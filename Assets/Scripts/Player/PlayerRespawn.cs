using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    //The sound it makes when you activate a checkpoint
    [SerializeField] private AudioClip sound;

    //Stores coordinates of the last checkpoint
    private Transform currCheckpoint;

    //The player's health
    private Health playerHealth;

    //Animation of the player
    private Animator anim;

    //The UI manager for the game over screen
    private UIManager UI;

    private ItemBehavior items;

    private void Start()
    {
        playerHealth = GetComponent<Health>();
        anim = GetComponent<Animator>();
        UI = FindObjectOfType<UIManager>(); //Looks through full hierarchy. Don't use if it has duplicates. Do not call in update - inefficient.
    }

    public void respawn()
    {
        if (currCheckpoint == null) //No current checkpoint
        {
            //show game over screen
            UI.gameOver();
        } 
        else
        {
            //items.onRespawn(currCheckpoint);
            transform.position = currCheckpoint.position;
            playerHealth.healthRespawn();

            anim.ResetTrigger("death");
            anim.Play("PlayerIdle");
            //Move camera to checkpoint as well (Checkpoint needs to be child of room object)
            Camera.main.GetComponent<CameraController>().moveToNewRoom(currCheckpoint, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            SoundManager.instance.playSound(sound);
            currCheckpoint = collision.transform;

            //Once activated can't be activated again. Disable collider.
            collision.GetComponent<Collider2D>().enabled = false; //Collider disables any type of collider. Could also use BoxCollider2D

            //Do the animation
            collision.GetComponent<Animator>().SetTrigger("appear");

            playerHealth.addHealth(100); //Over max health to make sure
        }
    }

}
