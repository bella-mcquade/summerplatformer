using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room Camera
    [SerializeField] private float speed;
    private float currPosx;
    private Vector3 velocity = Vector3.zero;
    private bool bossRoom = false;

    //Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float ahead;
    [SerializeField] private float camSpeed;
    private float lookAhead = 0;

    // Update is called once per frame
    private void Update()
    {
        //Room Camera
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(currPosx, transform.position.y, transform.position.z),
            ref velocity, speed);

        if (!bossRoom) {
            //Follow Player
            transform.position = new Vector3(player.position.x + lookAhead, player.position.y, transform.position.z);
            //lookAhead = Mathf.Lerp(lookAhead, ahead * player.localScale.x, Time.deltaTime * camSpeed);
        }
    }

    public void moveToNewRoom(Transform newRoom, bool bossDoor)
    {
        if (bossDoor) {
            bossRoom = true;
        } else {
            bossRoom = false;
        }

        currPosx = newRoom.position.x;
    }
}
