using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : VerticalEnemy
{
    private Transform originalCheckpoint;

    //CHANGE
    /*Do an array of checkpoints in the order that the player would unlock them.
     With each item, serialize what number the nezt checkpoint is?? Or next
    checkpoint itself*/

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void onRespawn(Transform currCheckpoint)
    {
        if (currCheckpoint == originalCheckpoint)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
