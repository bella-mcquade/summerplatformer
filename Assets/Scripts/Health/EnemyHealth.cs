using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public void death()
    {
        //player dead animation
        //anim.SetTrigger("death");

        gameObject.SetActive(false);
        //Do this for now but delete later once you do the animations.
        //GetComponent<PlayerRespawn>().respawn();
    }
}
