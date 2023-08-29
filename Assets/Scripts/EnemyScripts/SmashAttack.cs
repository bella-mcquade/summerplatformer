using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttack : EnemyDamage
{
    [Header("SmashAttack")]

    //The speed of the enemy
    [SerializeField] protected float attackSpeed;

    //How far the enemy is able to detect the player
    [SerializeField] protected float range;

    //How long between attacks the deerguard has to wait
    [SerializeField] protected float cooldown;

    //The layer that the player is on
    [SerializeField] protected LayerMask playerLayer;

    //The timer keeping track of the cooldown
    protected float cooldownTimer;

    //Stores the coordinates of the detected player
    private Vector3 dest;

    //Stores if the enemy is attacking or not
    protected bool attacking;

    private Vector3[] directions = new Vector3[2];//new Vector3[4];


    // Start is called before the first frame update
    private void Awake()
    {
        attacking = false;
    }

    private void OnEnable()
    {
        stop();
    }

    // Update is called once per frame
    private void Update()
    {
        if (attacking)
        {
            transform.Translate(dest * Time.deltaTime * attackSpeed);
        } else
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > cooldown)
            {
                //attacking = true;
                checkForPlayer();
            }
        }
    }

    protected void checkForPlayer()
    {
        calculateDir();

        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            if (hit.collider != null && hit.collider.tag == "Player" && !attacking)
            {
                attacking = true;
                /*if ((i == 1 && !canMoveL) || (i == 0 && !canMoveR))
                {
                    stop();

                } else {*/
                    dest = directions[i];
                    cooldownTimer = 0;
                //}
            }
        }
    }

    private void calculateDir()
    {
        directions[0] = transform.right * range; //right
        directions[1] = -transform.right * range; //left
        //directions[2] = transform.up * range; //up
        //directions[3] = -transform.up * range; //down
    }

    private void stop()
    {
        dest = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.transform.position.x <= transform.position.x)
            canMoveL = false;
        else
            canMoveR = false;*/
        

        base.OnTriggerEnter2D(collision);

        stop();


        if (collision.gameObject.tag == "Breakable")
        {
            collision.GetComponent<BreakableObject>().breakObject();
        }
    }
}

//OTHER OPTIONS:
//Make it so that the boar checks to see if there is anything not tagged as the player in check for player in front of the player.
