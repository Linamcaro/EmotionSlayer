using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class OrbCollected : MonoBehaviour
{
    private static OrbCollected _instance;
    public static OrbCollected Instance
    {
        get
        {
            return _instance;
        }
    }

    private int orbsCollected;
    private bool gameIsRunning;

    public event EventHandler OnScoreChanged;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        ResetScore();
    }

    private void setScore()
    {
        orbsCollected = 0;
    }

    public void IncreaseScore()
    {
        if (gameIsRunning)
        {
            orbsCollected++;
            OnScoreChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetScore()
    {
        return orbsCollected;
    }

    public void EndGame()
    {
        gameIsRunning = false;
    }

    public void ResetScore()
    {
        gameIsRunning = true;
        setScore();
    }

}
