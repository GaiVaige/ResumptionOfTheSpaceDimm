using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeypadPuzzle : MonoBehaviour
{
    public GameObject kpui;
    public GameObject canvasParent;
    string codeTextValue;
    public string safeCode;
    public TextMeshProUGUI codeText;
    public PlayerController pc;
    public AudioSource beepSound;
    public AudioSource correctSound;
    Animator anim;
    GameObject panel;
    public Button[] buttons;


    public int digitToAdd;
    public string stringDig;

    public bool panelIsOpen;

    public void Start()
    {
        anim = GetComponent<Animator>();
        pc = FindAnyObjectByType<PlayerController>();
        codeTextValue = "";
    }



    // Update is called once per frame
    void Update()
    {


        if (codeText != null)
        {
            codeText.text = codeTextValue;
        }



        if (panelIsOpen)
        {
            Debug.Log(codeTextValue);
            if (codeTextValue == safeCode)
            {
                //correctSound.enabled = true;
                //anim.SetTrigger("Open");
                pc.enabled = true;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
                codeTextValue = "";
                Destroy(panel);
                Destroy(this);
                panelIsOpen = false;
            }


            if ((codeTextValue.Length == 4) && !(codeTextValue == safeCode))
            {
                codeTextValue = "";
            }

        }




    }

    public void ActivatePanel()
    {
        panel = Instantiate(kpui, canvasParent.transform);
        codeText = panel.GetComponentInChildren<TextMeshProUGUI>();
        codeText.text = codeTextValue;
        panelIsOpen = true;
        pc.canMove = false;
        pc.walking.enabled = false;
        pc.enabled = false;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        kpui.SetActive(true);


        buttons = panel.GetComponentsInChildren<Button>();
        digitToAdd = 1;

        foreach (Button button in buttons)
        {
            Debug.Log(buttons.Length);


            if(digitToAdd == 10)
            {
                button.onClick.AddListener (delegate {ClosePanel(); });
                break;
            }



            string digit = digitToAdd.ToString();
            button.onClick.AddListener (() => AddDigit(digit));
            digitToAdd++;
            


        }
        digitToAdd = 0;


    }

    public void ClosePanel()
    {
        Destroy(panel);

        panelIsOpen = false;
        pc.enabled = true;
        pc.canMove = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    public void ClearCode()
    {
        codeTextValue = "";
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
        Debug.Log(digit);
    }

}
