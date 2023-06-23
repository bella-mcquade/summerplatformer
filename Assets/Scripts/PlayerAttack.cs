using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //A given variable that is how long between each attack
    [SerializeField] private float attackCool;

    //The start point of the projectile's movement.
    [SerializeField] private Transform startPoint;

    //Helps with object pooling.
    [SerializeField] private GameObject[] projectiles;

    //The cooldown counter. Increments every update.
    private float cooldown;

    //The player movement object
    private PlayerMovement playermovement;

    //Animation object
    private Animator anim;

    //Basically constructor
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playermovement = GetComponent<PlayerMovement>();
        cooldown = Mathf.Infinity; //Use big number for start to ensure attack is always ready
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldown > attackCool && playermovement.canAttack()) //If the mouse is clicked
        {
            Attack(); //Could just put this here?
        }

        cooldown += Time.deltaTime; //Incrememnt every frame
    }

    private void Attack()
    {
        cooldown = 0;

        //Use object pooling to keep performance good.
        //projectiles[0].transform.position = startPoint.position;
        //projectiles[0].GetComponent<AcornProjectile>().setDirection(Mathf.Sign(transform.localScale.x));

        //Use object pooling to keep performance good.
        projectiles[findProjectile()].transform.position = startPoint.position;
        projectiles[findProjectile()].GetComponent<AcornProjectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    //Returns the index of the first non active projectile
    private int findProjectile()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return i; //i is first index that is not active
            }
        }
        return 0;
    }
}
