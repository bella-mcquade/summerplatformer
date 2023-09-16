using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBase : MonoBehaviour
{
    public bool finished { get; private set; }

    protected IEnumerator writeText(string input, Text textHolder,
            Color textColor,  Font textFont, float delay, float btwnLines){
        textHolder.color = textColor;
        textHolder.font = textFont;
        

        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitUntil(()=> Input.GetMouseButton(0));
        finished = true;
    }
}
