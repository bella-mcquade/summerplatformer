using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float damage;

    private bool hit;

    private float direction;

    private Animator anim;

    //The lifetime of the acorn if it does not hit something.
    private float lifetime;

    //The boxcollider connected to this projectile.
    private BoxCollider2D boxCol;

    //Basically constructor
    private void Awake()
    {
        hit = false;
        lifetime = 0;
        boxCol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 10)
        {
            Deactivate();
        }
    }

    //Checks to see if it has hit something. If it has, make sure it stops moving.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCol.enabled = false;
        anim.SetTrigger("explode");
        //Deactivate();

        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Health>()?.takeDamage(damage);
        }
    }

    //Creates a projectile and sets the direction to the given direction.
    public void setDirection(float dir)
    {
        direction = dir;
        hit = false;
        gameObject.SetActive(true); //I assume this summons an active object.
        boxCol.enabled = true;
        lifetime = 0;
        anim.ResetTrigger("explode");

        float scale = transform.localScale.x;
        if (Mathf.Sign(scale) != dir)
        {
            scale = -scale;
        }

        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }

    //Deactivates the projectile. Use this if you want to add animation.
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
