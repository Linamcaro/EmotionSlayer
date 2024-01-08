using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image healthBar;
    private float playerCurentHealth;
    private float playerMaxHealth;

    private void Start()
    {
     
      playerCurentHealth = PlayerHealth.Instance.GetPlayerCurrentHealth();
      playerMaxHealth = PlayerHealth.Instance.GetPlayerMaxHealth();

      healthBar.fillAmount = playerCurentHealth / playerMaxHealth;

      PlayerHealth.Instance.OnPlayerHealthChanged += HealthUI_OnPlayerHealthChanged;
    }
   
    private void HealthUI_OnPlayerHealthChanged(object sender, EventArgs e)
    {
        playerCurentHealth = PlayerHealth.Instance.GetPlayerCurrentHealth();
        playerMaxHealth = PlayerHealth.Instance.GetPlayerMaxHealth();
        healthBar.fillAmount = playerCurentHealth / playerMaxHealth;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event or any relevant callbacks
        PlayerHealth.Instance.OnPlayerHealthChanged -= HealthUI_OnPlayerHealthChanged;
    }

}
