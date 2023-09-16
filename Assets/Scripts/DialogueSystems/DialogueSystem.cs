using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystems
{
    public class DialogueSystem : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(diaSequence());    
        }

        //Activates one dialogue line at a time.
        private IEnumerator diaSequence()
        {
            for (int i = 0; i < transform.childCount; i++) {
                deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).gameObject.GetComponent<DialogueLines>().finished);
            }
            gameObject.SetActive(false);
        }

        private void deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}