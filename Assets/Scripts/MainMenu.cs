using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer aus;
    public string gameSceneName;


    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    public void AdjustVolume(float soundLevel)
    {
        aus.SetFloat("volume", soundLevel);
    }
}
