using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerController pc;
    public bool paused;

    public GameObject pauseUI;
    public AudioSource clickSound;


    void Awake()
    {
        pauseUI = GameObject.Find("PauseUI");
        pauseUI.SetActive(false);
        pc = FindObjectOfType<PlayerController>();
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!pc.isInDialogue)
            {
                if (!paused)
                {
                    PauseGame();
                    Debug.Log("Paused");
                }
                else if (paused)
                {
                    ResumeGame();
                    Debug.Log("Not Paused");
                }
            }



        }

    }



    public void PauseGame()
    {
        paused = true;
        clickSound.enabled = true;
        Time.timeScale = 0;
        pc.enabled = false;


        pauseUI.SetActive(true);


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void ResumeGame()
    {

        Time.timeScale = 1;
        pc.enabled = true;
        pauseUI.SetActive(false);
        clickSound.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
    }

    public void QuitGame()
    {
        clickSound.enabled = true;
        Debug.Log("quit");
    }

    public void QuitToMenu()
    {
        clickSound.enabled = true;
        Debug.Log("back to menu");
    }


}
