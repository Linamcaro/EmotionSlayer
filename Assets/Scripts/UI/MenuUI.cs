using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject menu;
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu") 
        {
            return;
        }else if (PlayerControls.Instance.PlayerMenu())
        {
            menu.SetActive(true);
        }
    }

}
