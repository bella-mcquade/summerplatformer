using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;

    //Basically the image/UI version of the transform object
    private RectTransform rect;

    private int currPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        currPos = 0;
    }

    private void Update()
    {
        //Moves arrow up and down
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            changePos(-1); //Needs to DECREASE pos to go up

        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            changePos( 1); //INCREASE pos to go down
        }

        //Interacts with selection
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Space has been clicked");
            //Play sound
            //Access button component
            options[currPos].GetComponent<Button>().onClick.Invoke();
        }
    }

    private void changePos(int change)
    {
        currPos += change;

        if (currPos < 0) 
        {
            //Loops current position to the end
            currPos = options.Length - 1;
        } else if(currPos >= options.Length)
        {
            //Loops current position to the start
            currPos = 0;
        }

        rect.position = new Vector3(rect.position.x, options[currPos].position.y, 0);
    }
}
