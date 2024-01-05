using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image healthBar;

    private void Start()
    {
        PlayerHealth.Instance.OnPlayerHealthChanged += HealthUI_OnPlayerHealthChanged;

    }
   
    private void HealthUI_OnPlayerHealthChanged(object sender, EventArgs e)
    {
        float playerCurentHealth = PlayerHealth.Instance.GetPlayerCurrentHealth();
        float playerMaxHealth = PlayerHealth.Instance.GetPlayerMaxHealth();

        healthBar.fillAmount = playerCurentHealth / playerMaxHealth;
    }
}
