using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

  

    private static PlayerHealth _instance;
    public static PlayerHealth Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] private int maxHealth;

    [SerializeField] private int currentHealth;

    public event EventHandler OnPlayerDied;


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
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            OnPlayerDied?.Invoke(this, EventArgs.Empty);
        }
    }
}
