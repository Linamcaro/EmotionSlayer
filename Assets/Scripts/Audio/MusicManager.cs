using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private MusicSO musicSO;
    private AudioSource audioSource;

    private static MusicManager _instance;
    public static MusicManager Instance
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
                PlayMusic(musicSO.mainMenuMusic);
            }
        }

    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            PlayMusic(musicSO.mainMenuMusic);
        }
        else
        {
            PlayMusic(musicSO.gameMusic);
        }
    }

    public void PlayMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.volume = 0.3f;
        audioSource.Play();

    }
}
