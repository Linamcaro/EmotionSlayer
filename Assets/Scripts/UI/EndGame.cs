using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI SubtitleText;

    private Boss boss;

    private void Awake()
    {
        HidePanel();
    }
    private void Start()
    {
        PlayerHealth.Instance.OnPlayerDied += EndGame_OnPlayerDied;
        LevelManager.Instance.OnPlayerWins += EndGame_OnPlayerWins;
    }

    private void EndGame_OnPlayerWins(object sender, EventArgs e)
    {
        ShowWinner();
    }

    private void EndGame_OnPlayerDied(object sender, EventArgs e)
    {
        ShowGameOver();
    }


    private void ShowGameOver()
    {
        TitleText.text = "YOU LOST!";
        SubtitleText.text = "Keep Trying";

        GameOverUI.SetActive(true);
        ShowCursor();
    }

    private void ShowWinner()
    {
        TitleText.text = "CONGRATULATIONS!!";
        SubtitleText.text = "You made it";

        GameOverUI.SetActive(true);
        ShowCursor();
    }


    private void HidePanel()
    {
        GameOverUI.SetActive(false);
    }


    public void CleanUP()
    {
        Destroy(SoundfxManager.Instance.gameObject);
        Destroy(OrbCollected.Instance.gameObject);
        Destroy(PlayerHealth.Instance.gameObject);
        Destroy(PlayerControls.Instance.gameObject);
        Destroy(LevelManager.Instance.gameObject);
    }

    public void ShowCursor()
    {
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }

    private void OnDestroy()
    {
        PlayerHealth.Instance.OnPlayerDied -= EndGame_OnPlayerDied;
        LevelManager.Instance.OnPlayerWins -= EndGame_OnPlayerWins;
    }

}
