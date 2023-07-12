using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingEnemy : MonoBehaviour
{
    [Header("Horizontal Movement")]
    //How much damage the enemy does
    [SerializeField] private float damage;

    //How far the enemy can move
    [SerializeField] private float movementDistance;

    //How fast the enemy moves at rest
    [SerializeField] private float idleSpeed;

    //How shallow the arc is
    [SerializeField] private float shallowness;

    //If the enemy is moving left
    private bool movingLeft;

    //The left edge of the enemy range
    private float leftEdge;

    //The right edge of the enemy range
    private float rightEdge;

    //The center (and starting point) of the enemy range.
    private float center;

    //The starting yPosition of the enemy.
    private float topY;

    private float initY;

    // Start is called before the first frame update
    void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        center = transform.position.x;
        initY = transform.position.y;
        topY = transform.position.y + (rightEdge - center) * (rightEdge - center);
        movingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distFromCenter = transform.position.x - center;

        if(movingLeft)
        {

            if (transform.position.x > leftEdge)
            {
                float xPos = transform.position.x - idleSpeed * Time.deltaTime;
                if (transform.position.x < center + 0.5 && transform.position.x > center - 0.5)
                {
                    transform.position = new Vector2(xPos, initY); //Resets y position

                } else if (transform.position.x > center) //y going down
                {
                    transform.position = new Vector2(xPos, transform.position.y -
                        (distFromCenter * distFromCenter * Time.deltaTime) / shallowness);

                    transform.localScale = new Vector3(1f, 1f, 1f);

                } else //y going up
                {
                    transform.position = new Vector2(xPos, transform.position.y +
                        (distFromCenter * distFromCenter * Time.deltaTime) / shallowness);

                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
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
                float xPos = transform.position.x + idleSpeed * Time.deltaTime;
                if (transform.position.x < center + 0.5 && transform.position.x > center - 0.5)
                {
                    transform.position = new Vector2(xPos, initY); //Resets y position
                }
                else if (transform.position.x > center) //y going up
                {
                    transform.position = new Vector2(xPos, transform.position.y +
                        (distFromCenter * distFromCenter * Time.deltaTime) / shallowness);

                    transform.localScale = new Vector3(-1f, 1f, 1f);

                }
                else //y going down
                {
                    transform.position = new Vector2(xPos, transform.position.y -
                        (distFromCenter * distFromCenter * Time.deltaTime) / shallowness);

                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
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
