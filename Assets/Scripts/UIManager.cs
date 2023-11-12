using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerController pc;
    public bool paused;

    public GameObject pauseUI;


    void Start()
    {

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
        Time.timeScale = 0;
        pc.enabled = false;


        pauseUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        pc.enabled = true;
        pauseUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Debug.Log("quit");
    }

    public void QuitToMenu()
    {
        Debug.Log("back to menu");
    }


}
