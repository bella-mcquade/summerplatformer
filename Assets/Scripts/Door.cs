using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform lastRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                cam.moveToNewRoom(nextRoom, true);
            else
                cam.moveToNewRoom(lastRoom, false);
        }
    }
}
