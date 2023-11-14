using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer aus;
    public AudioSource clickSound;
    public string gameSceneName;


    // Start is called before the first frame update
    void Start()
    {
        clickSound.enabled = false;
    }

    public void StartGame()
    {
        clickSound.enabled = true;
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        clickSound.enabled = true;
        Application.Quit();
    }

    public void AdjustVolume(float soundLevel)
    {
        aus.SetFloat("volume", soundLevel);
    }
}
