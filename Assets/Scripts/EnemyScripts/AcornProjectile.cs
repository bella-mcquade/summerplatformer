using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornProjectile : MonoBehaviour
{
    //The speed of the projectile
    [SerializeField] private float speed;

    //The damage of the projectile
    [SerializeField] private float damage;

    //If it has hit something or not. For now, probably don't need but can use to implement acorn rolling later.
    private bool hit;

    //The direction that the projectile will be moving.
    [SerializeField] private float direction; //ONLY SERIALIZABLE FOR NOW. LATER LINK TO ACORNGUNNER SCALE

    //The lifetime of the acorn if it does not hit something.
    private float lifetime;

    //The boxcollider connected to this projectile.
    private BoxCollider2D boxCol;

    private Animator anim;

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
        //transform.Rotate(transform.rotation.x, transform.rotation.y, -1);

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
        Deactivate();

        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Health>()?.takeDamage(damage);
        }
    }

    //Creates a projectile and sets the direction to the given direction.
    public void setDirection(float dir)
    {
        direction = dir;
        hit = false;
        boxCol.enabled = true;
        lifetime = 0;
        gameObject.SetActive(true); //I assume this summons an active object.

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
