using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //The game object corresponding to the game over screen.
    [SerializeField] GameObject gameOverScreen;

    public void Awake()
    {
        gameOverScreen.SetActive(false);    
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);

    }

    public void Restart() {
        //Reloads the scene that we are currently using
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        //Loads the intro scene
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
 