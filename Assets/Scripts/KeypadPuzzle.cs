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
    public TextMeshProUGUI codeText;
    public PlayerController pc;
    public AudioSource beepSound;
    public AudioSource correctSound;

    GameObject panel;
    public Button[] buttons;


    public int digitToAdd;
    public string stringDig;

    public bool panelIsOpen;


    [Header("Things to touch")]
    [Tooltip("Literally the ontly thing to edit, this sets the correct answer. It is always 4 digits.")]
    public string safeCode;



    [Header("Settings for puzzle completion")]
    [Header("If you want an item to spawn")]
    public bool spawnItem;
    public PlayerItem itemToSpawn;

    [Header("If you want a safe to open")]
    public bool open;
    public Animator anim;


    [Header("Click if you want to delete the collider for the keypad")]
    public bool destroyKeypadEntirely;




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
                correctSound.enabled = true;

                if (spawnItem)
                {
                    pc.gameObject.GetComponent<PlayerInventory>().playerItems.Add(itemToSpawn.gameObject);
                }

                if (open)
                {

                   anim.SetTrigger("Open");
                }



                pc.enabled = true;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
                codeTextValue = "";
                Destroy(panel);
                panelIsOpen = false;


                if (destroyKeypadEntirely)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    Destroy(this);
                }
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
        beepSound.enabled = false;
        beepSound.enabled = true;
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
        beepSound.enabled = false;
        beepSound.enabled = true;
        codeTextValue += digit;
        Debug.Log(digit);
    }

}
