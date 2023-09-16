using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystems
{
    public class DialogueLines : DialogueBase
    {
        [Header("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header("Text Delay")]
        [SerializeField] private float delay;
        [SerializeField] private float btwnLines;

        [Header("CharacterImage")]
        [SerializeField] private Sprite charImage;
        [SerializeField] private Image imageHolder;

        private Text textholder;

        public void Awake()
        {
            textholder = GetComponent<Text>();
            textholder.text = ""; //make sure its empty

            imageHolder.sprite = charImage;
            imageHolder.preserveAspect = true;
        }

        public void Start()
        {
            StartCoroutine(writeText(input, textholder, textColor, textFont, delay, btwnLines));
        }
    }
}
