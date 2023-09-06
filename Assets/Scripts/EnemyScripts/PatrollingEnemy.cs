using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [Header("Horizontal Movement")]
    //How much damage the enemy does
    [SerializeField] private float damage;

    //How far the enemy can move
    [SerializeField] private float movementDistance;

    //How fast the enemy moves at rest
    [SerializeField] private float idleSpeed;

    //How fast the enemy moves while attacking
    [SerializeField] private float attackSpeed;

    //How fast the enemy is going right now
    private float currSpeed;

    //If the enemy is moving left
    private bool movingLeft;

    //The left edge of the enemy range
    private float leftEdge;

    //The right edge of the enemy range
    private float rightEdge;

    [Header("Player Sensing")]
    //How far the deerguard is able to detect the player
    [SerializeField] private float range;

    //How long between attacks the deerguard has to wait
    [SerializeField] private float cooldown;

    //The layer that the player is on
    [SerializeField] private LayerMask playerLayer;

    //The timer keeping track of the cooldown
    private float cooldownTimer;

    //Stores if the deerguard is attacking or not
    private bool attacking;

    private Vector3[] directions = new Vector3[2];

    // Start is called before the first frame update
    void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        currSpeed = idleSpeed;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            currSpeed = attackSpeed;
        }
        else
        {
            currSpeed = idleSpeed;
        }

        if (movingLeft)
        {
            //check to see if player is on the left
            checkForPlayer(-transform.right * range);

            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector2(transform.position.x - currSpeed * Time.deltaTime, transform.position.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                movingLeft = false;
            }

        }
        else //Moving right
        {
            //check to see if player is on the right
            checkForPlayer(transform.right * range);

            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector2(transform.position.x + currSpeed * Time.deltaTime, transform.position.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                movingLeft = true;
            }

        }
    }

    private void checkForPlayer(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector3(1f, 1f, 1f), 0, direction, range, playerLayer);
        if (hit.collider != null && !attacking)
        {
            attacking = true;
            cooldownTimer = 0;
        }
        else
        {
            attacking = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Health>().takeDamage(damage);
        }
    }
}