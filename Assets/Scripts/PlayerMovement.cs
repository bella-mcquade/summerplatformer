using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jump;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body; //Creates the body object
    private BoxCollider2D boxCol;
    float horizontalInput;
    //private bool onGround;

    //Called when script is loaded. Basically constructor/initlializer?
    private void Awake() 
    {
        Debug.Log("Loading up the game");
        body = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        body.freezeRotation = true;

        //onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        /*Input.GetAxis("Horizontal") basically checks whether you've clicked
         * a or d and returns -1 or 1. */
        //body.velocity.y keeps it the same
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flips user based on if they are going left or right.
        if (horizontalInput > 0.1f) {
            transform.localScale =  Vector3.one; //resets it back to 1 1 1
        } else if (horizontalInput < -0.1f){
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Handles jumping
        if(Input.GetAxisRaw("Vertical") > 0.5 && onGround()){
            body.velocity = new Vector2(body.velocity.x, jump);
            //body.AddForce(new Vector2(body.velocity.x, jump ));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //onGround = true;
        }
    }

    /*Determines whether the player is on the ground or not. Returns true or
     * false depending on if the player is touching the ground from the top.*/
    private bool onGround()
    {
        //Boxcast: Basically sends out a laser(box) in a direction + gets back info about stuff it hits.
        RaycastHit2D rCast = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rCast.collider != null;
    }

    /*Determines whether the player can attack or not. Public so that the attack 
     * script can access it.*/
    public bool canAttack()
    {
        return horizontalInput == 0 && onGround();
    }
}
