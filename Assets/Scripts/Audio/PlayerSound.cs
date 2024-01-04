using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    private PlayerController playerController;
    private PlayerCombat playerCombat;

   
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    private void Start()
    {
        playerController.playerJumped += playerSound_OnPlayerJumped;
        playerCombat.playerFired += playerSound_OnPlayerAttacked;
        playerController.isRunning += playerSound_OnPlayerRunning;
    }

    private void playerSound_OnPlayerRunning(object sender, EventArgs e)
    {
        SoundfxManager.Instance.PlayFootstepsSound();
    }

    private void playerSound_OnPlayerAttacked(object sender, EventArgs e)
    { 
         SoundfxManager.Instance.PlaySwordSlash();
    }

    private void playerSound_OnPlayerJumped(object sender, EventArgs e)
    {
        SoundfxManager.Instance.PlayPlayerJumped();
    }


}
