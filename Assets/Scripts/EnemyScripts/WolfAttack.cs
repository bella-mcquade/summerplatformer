using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : EnemyDamage
{
    //The layer the ground is on. Used to tell when to jump
    [SerializeField] private LayerMask groundLayer;

    //How far away the enemy can sense player
    [SerializeField] private float range;

    //The layer the player is on. Used to sense if the player is within range.
    [SerializeField] private LayerMask playerLayer;

    //The speed of the enemy
    [SerializeField] private float speed;

    [SerializeField] private float idleCooldown;

    //How high the wolf can jump
    [SerializeField] private float jump;

    private bool attacking;

    //Keeps track of how long since the cooldown has reset.
    private float cooldownTimer;

    private Rigidbody2D body;

    private BoxCollider2D boxCol;

    // Start is called before the first frame update
    void Awake()
    {
        attacking = false;
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        boxCol = GetComponent<BoxCollider2D>();
        cooldownTimer = Mathf.Infinity;
    }

    // Update is called once per frame
    void Update()
    {
        //Randomly turns enemy around after cooldown. Idle movement.
        /*if (!attacking && cooldownTimer > idleCooldown) {
            cooldownTimer = 0;
            if (Random.Range(0, 2) == 0)
            {
                speed = -speed;
                //transform.localScale = transform.localScale * (new Vector3(-1f, 1f, 1f));
            }
        }*/

        cooldownTimer += Time.deltaTime;

        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        checkForPlayer(-transform.right);
        checkForPlayer(transform.right);

        if (onGround())
        {
            //body.velocity = new Vector2(body.velocity.x, jump);
            if (speed > 0) {
                checkForGround(-transform.right);
            } else
            {
                checkForGround(transform.right);
            }
        }
    }

    private void checkForPlayer(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector3(1f, 1f, 1f), 0, direction, range, playerLayer);
        if (hit.collider != null && !attacking)
        {
            attacking = true;
            speed = speed * -Mathf.Sign(direction.x);
            //cooldownTimer = 0;
        }
        else
        {
            attacking = false;
        }
    }

    private bool onGround()
    {
        //Boxcast: Basically sends out a laser(box) in a direction + gets back info about stuff it hits.
        RaycastHit2D rCast = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rCast.collider != null;
    }

    private void checkForGround(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector3(1f, 1f, 1f), 0, direction, 1.5f, groundLayer);
        if (hit.collider != null)
        {
            body.velocity = new Vector2(body.velocity.x, jump);
        }
    }
}
