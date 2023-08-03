using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Health
    //The health at the start of the level?
    [SerializeField] private float startHealth;

    //Tracks the current health of the object. Get == anyone can access, private set == only can change from here
    public float currHealth { get; private set; }

    //iFrames
    //Invicibility duration
    [SerializeField] private float iDuration;

    //Number of red flashes
    [SerializeField] private float numFlash;

    //The sprite that we are changing the color of
    private SpriteRenderer sprite;

    //The animator of the player
    private Animator anim;

    //Sets health to max upon start.
    private void Awake()
    {
        currHealth = startHealth;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    //Allows enemies to damage player.
    public void takeDamage(float damage)
    {
        //Basically caps currHealth - damage at 0 or startHealth
        currHealth = Mathf.Clamp(currHealth - damage, 0, startHealth);

        if (currHealth > 0)
        {
            //player hurt animation
            StartCoroutine(invulnerability());

        } else
        {
            //player dead animation
            anim.SetTrigger("death");
            GetComponent<PlayerMovement>().enabled = false;
            //Do this for now but delete later once you do the animations.
            //GetComponent<PlayerRespawn>().respawn();

        }
    }

    public void addHealth(float newHealth)
    {
        currHealth = Mathf.Clamp(currHealth + newHealth, 0, startHealth);
    }

    public void healthRespawn()
    {
        addHealth(startHealth);
        //reset death animation:
        //anim.ResetTrigger("die");

        //Play idle animation
        //anim.Play("Idle");

        //Reactivate deactivated components
        GetComponent<PlayerMovement>().enabled = true;
    }

    public void changeMaxHealth(float maxChange)
    {
        startHealth -= maxChange;

        if (startHealth < currHealth)
        {
            currHealth = startHealth;
        }
    }

    private IEnumerator invulnerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < numFlash; i++)
        {
            sprite.color = new Color(1, 0, 0, 0.5f); //Basically Color.red but with .5 transparency
            yield return new WaitForSeconds(iDuration / (numFlash * 2));
            sprite.color = Color.white;
            yield return new WaitForSeconds(iDuration / (numFlash * 2));
        }

        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
}
