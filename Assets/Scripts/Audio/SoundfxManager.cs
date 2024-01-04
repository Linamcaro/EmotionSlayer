using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundfxManager : MonoBehaviour
{
    [SerializeField] private SoundFxSO soundfx;

    private AudioSource audioSource;

    private static SoundfxManager _instance;
    public static SoundfxManager Instance
    {
        get { return _instance; }
    }
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

            // Initialize the audio source
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                // If there's no AudioSource component, add one to the GameObject
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }



    //-----------------------------------------------------------------------------------------------------------


    private void PlaySound(AudioClip audioClip)
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        else
        {
            audioSource.volume = 1.0f;
            audioSource.PlayOneShot(audioClip);
        }
    }


    //-----------------------------------------------------------------------------------------------------------

    public void PlayFootstepsSound()
    {
        PlaySound(soundfx.footsteps);
    }

    //-----------------------------------------------------------------------------------------------------------

    public void PlayPlayerJumped()
    {
        PlaySound(soundfx.jump);
    }

    //-----------------------------------------------------------------------------------------------------------

    public void PlayMonsterDeath()
    {
        PlaySound(soundfx.monsterDeath);
    }

    //-----------------------------------------------------------------------------------------------------------

    public void PlayMonsterRoar()
    {
        PlaySound(soundfx.monsterRoar);
    }

    //-----------------------------------------------------------------------------------------------------------
    public void PlayOrbPickUp()
    {
        PlaySound(soundfx.orbPickup);
    }

    public void PlaySwordSlash()
    {
        PlaySound(soundfx.swordSlash);
    }

    //-----------------------------------------------------------------------------------------------------------
}
