using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject menu;
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
        HideCursor();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ShowCursor();
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
            ShowCursor();
        }
    }

    public void ShowCursor()
    {
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }

    public void HideCursor()
    {
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

}
