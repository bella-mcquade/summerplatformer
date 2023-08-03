using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrandDoor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer key;

    [SerializeField] private int nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If hit player and this door key has been picked up
        if (collision.gameObject.tag == "Player" && key.enabled == false)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
