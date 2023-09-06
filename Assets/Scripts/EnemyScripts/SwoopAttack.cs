using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwoopAttack : EnemyDamage
{
    //TWO STATES
    /*
     IF WE ARE OUTSIDE INITIAL RANGE, LEAVE AND GO BACK TO STARTING POINT.
     OTHERWISE CHECK IF PLAYER IS WITHIN BOX AND GO TO PLAYER.*/

    [Header("SwoopAttack")]

    //How far the enemy is able to detect the player
    [SerializeField] protected float range;

    //Player coordinates
    [SerializeField] private Transform playerCoords;

    //The start point of the swoop enemy's movement.
    [SerializeField] private Transform startPoint;

    //The speed of the enemy
    [SerializeField] protected float speed;

    //Stores the coordinates of the detected player
    private Vector3 dest;

    //Stores if the enemy is attacking or not
    protected bool attacking;

    //How long between attacks the deerguard has to wait
    [SerializeField] protected float cooldown;

    //The timer keeping track of the cooldown
    protected float cooldownTimer;

    //Constructor
    private void Awake()
    {
        attacking = false;
        dest = startPoint.position;
    }

    private void Update()
    {
        if (dest.x < transform.position.x)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }

        if (dest.y < transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }

        cooldownTimer += Time.deltaTime;

        if (cooldownTimer > cooldown)
        {
            //attacking = true;
            checkForPlayer();
        }
        //}
    }

    private void checkForPlayer()
    {
        if (playerCoords.position.x < startPoint.transform.position.x + range &&
                playerCoords.position.x > startPoint.transform.position.x - range &&
                playerCoords.position.y < startPoint.transform.position.y + range &&
                playerCoords.position.x > startPoint.transform.position.x - range)
        {
            attacking = true;
            dest = playerCoords.position;
        } else
        {
            attacking = false;
            dest = startPoint.position;
        }
    }

    private void stop()
    {
        dest = startPoint.position;
        attacking = false;
    }
}
