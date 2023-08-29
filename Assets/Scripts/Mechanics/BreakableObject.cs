using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void breakObject(){
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    /*public void onRespawn(Transform currCheckpoint)
    {
        if (currCheckpoint == originalCheckpoint)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }*/
}
