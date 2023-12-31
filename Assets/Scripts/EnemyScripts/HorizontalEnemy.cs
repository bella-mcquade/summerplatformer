using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemy : MonoBehaviour
{
    [Header("Horizontal Movement")]
    //How much damage the enemy does
    [SerializeField] private float damage;

    //How far the enemy can move
    [SerializeField] private float movementDistance;

    //How fast the enemy moves at rest
    [SerializeField] private float idleSpeed;

    //If the enemy is moving left
    private bool movingLeft;

    //The left edge of the enemy range
    private float leftEdge;

    //The right edge of the enemy range
    private float rightEdge;

    //The layer that the player is on
    [SerializeField] private LayerMask playerLayer;

    // Start is called before the first frame update
    void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {

            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector2(transform.position.x - idleSpeed * Time.deltaTime, transform.position.y);
                transform.localScale = new Vector3(1f, 1f, 1f); ;
            }
            else
            {
                movingLeft = false;
            }

        }
        else //Moving right
        {

            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector2(transform.position.x + idleSpeed * Time.deltaTime, transform.position.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                movingLeft = true;
            }

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