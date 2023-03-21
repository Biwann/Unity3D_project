using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _musicClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // сохраняем объект при переходе на новую сцену
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _audioSource.clip = _musicClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }
    }
}
