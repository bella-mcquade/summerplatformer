using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //The game object corresponding to the game over screen.
    [SerializeField] GameObject gameOverScreen;

    public void gameOver()
    {
        gameOverScreen.SetActive(true);

    }
}
