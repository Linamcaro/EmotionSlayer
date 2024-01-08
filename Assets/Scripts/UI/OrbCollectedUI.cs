using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class OrbCollectedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orbCollectedText;


    private void Start()
    {
        OrbCollected.Instance.OnScoreChanged += OrbCollectedUI_OnScoreChanged;
    }

    private void OrbCollectedUI_OnScoreChanged(object sender, EventArgs e)
    {
        orbCollectedText.text = $"Orbs Collected: {OrbCollected.Instance.GetScore()}";
    }



    private void OnDestroy()
    {
        // Unsubscribe from the event or any relevant callbacks
        OrbCollected.Instance.OnScoreChanged += OrbCollectedUI_OnScoreChanged;
    }
}
