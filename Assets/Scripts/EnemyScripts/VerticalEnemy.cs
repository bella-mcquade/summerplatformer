using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemy : MonoBehaviour
{
    [Header("Vertical Movement")]
    //How much damage the enemy does
    [SerializeField] private float damage;

    //How far the enemy can move
    [SerializeField] private float movementDistance;

    //How fast the enemy moves at rest
    [SerializeField] private float idleSpeed;

    //If the enemy is moving down
    private bool movingDown;

    //The left edge of the enemy range
    private float topEdge;

    //The right edge of the enemy range
    private float bottomEdge;

    // Start is called before the first frame update
    void Awake()
    {
        bottomEdge = transform.position.y - movementDistance;
        topEdge = transform.position.y + movementDistance;
        GetComponent<SpriteRenderer>().enabled = true; //helps make sure items always start active
    }

    // Update is called once per frame
    void Update()
    {
        if (movingDown)
        {

            if (transform.position.y > bottomEdge)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - idleSpeed * Time.deltaTime);
                //transform.localScale = new Vector3(1f, 1f, 1f); ;
            }
            else
            {
                movingDown = false;
            }

        }
        else //Moving Up
        {

            if (transform.position.y < topEdge)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + idleSpeed * Time.deltaTime);
                //transform.localScale = new Vector3(1f, -1f, 1f);
            }
            else
            {
                movingDown = true;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Health>().takeDamage(damage);
        }
    }
}
