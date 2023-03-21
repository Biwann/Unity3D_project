using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GamePaused;
    private bool _winLoose = false;
    [SerializeField] GameObject _pauseGameMenu;
    [SerializeField] GameObject _winGameMenu;
    [SerializeField] GameObject _looseGameMenu;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _winSound;
    [SerializeField] AudioClip _looseSound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!_winLoose)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        _pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        _pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().buildIndex == 5)
            LoadMenu();
        else
        {
            var newScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(newScene);
        }
        
    }
    public void Win()
    {
        _winGameMenu.SetActive(true);
        _winLoose = true;
        GamePaused = true;

        Time.timeScale = 1f;
        _audioSource.PlayOneShot(_winSound);
    }

    public void Loose()
    {
        _looseGameMenu.SetActive(true);
        _winLoose = true;
        GamePaused = true;
        _audioSource.PlayOneShot(_looseSound);
    }
}
