using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    //Stores coordinates of the last checkpoint
    private Transform currCheckpoint;

    //The player's health
    private Health playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<Health>();
        currCheckpoint = transform; //Set the first "checkpoint" to be current pos
    }

    public void respawn()
    {
        transform.position = currCheckpoint.position;
        playerHealth.healthRespawn();

        //Move camera to checkpoint as well (Checkpoint needs to be child of room object)
        //Camera.main.GetComponent<CameraController>().moveToNewRoom(currCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currCheckpoint = collision.transform;

            //Once activated can't be activated again. Disable collider.
            collision.GetComponent<Collider2D>().enabled = false; //Collider disables any type of collider. Could also use BoxCollider2D

            //Do the animation
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }

}
