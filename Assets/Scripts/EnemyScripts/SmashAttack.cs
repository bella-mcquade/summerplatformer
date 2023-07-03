using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttack : HorizontalEnemy
{
    [Header("SmashAttack")]

    //The speed of the deerguard
    [SerializeField] private float attackSpeed;

    //How far the deerguard is able to detect the player
    [SerializeField] private float range;

    //How long between attacks the deerguard has to wait
    [SerializeField] private float cooldown;

    //The layer that the player is on
    [SerializeField] private LayerMask playerLayer;

    //The timer keeping track of the cooldown
    private float cooldownTimer;

    //Stores the coordinates of the detected player
    private Vector3 dest;

    //Stores if the deerguard is attacking or not
    private bool attacking;

    private Vector3[] directions = new Vector3[4];

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

    private void checkForPlayer()
    {
        calculateDir();

        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            if (hit.collider != null && !attacking)
            {
                attacking = true;
                dest = directions[i];
                cooldownTimer = 0;
            }
        }
    }

    private void calculateDir()
    {
        directions[0] = transform.right * range; //right
        directions[1] = -transform.right * range; //left
        directions[2] = transform.up * range; //up
        directions[3] = -transform.up * range; //down
    }

    private void stop()
    {
        dest = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        stop();
    }
}
