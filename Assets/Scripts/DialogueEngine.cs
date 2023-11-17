using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEngine : MonoBehaviour
{
    public DialogueScriptableObject loadedDialogue;
    public DialogueScriptableObject nextDialogue;
    public List<DialogueScriptableObject> choiceDialogues;
    public GameObject choiceUI;
    public List<Button> buttons;
    public GameObject cui;
    public string thisSentence;
    public Transform canvasParent;
    public TextMeshProUGUI _namespace;
    public TextMeshProUGUI _dialogueText;
    public Sprite image;
    public GameObject imageOverride;
    public int imageInt;
    UIManager uim;
    public bool dialogueFinished;
    public GameObject player;
    public Transform respawn;
    bool hasntAddedItem = true;

    public bool goNextHour;

    public AudioSource dialogueAudioEngine;

    private Queue<string> sentences;
    string sentence;

    public int choiceStep;
    public bool waitingForResponse;
    public bool generatedButtons;

    void Start()
    {
        choiceStep = 0;
        uim = FindObjectOfType<UIManager>();
        goNextHour = false;


        if(loadedDialogue.art.Length == 0)
        {
            imageOverride.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {




        if (loadedDialogue.goNextHour)
        {
            goNextHour = true;
        }





        if(loadedDialogue.dialogueChoice.Count != 0)
        {
            waitingForResponse = true;
        }
        else
        {
            waitingForResponse = false;
        }


        if (Input.GetKeyDown(KeyCode.Space) && !waitingForResponse && loadedDialogue.dialogueChoice.Count == 0)
        {
            if (nextDialogue != null)
            {
                LoadNextDialogue(nextDialogue);
            }
            else if (dialogueFinished)
            {
                Debug.Log("I am in the first check");
                if (goNextHour)
                {
                    Debug.Log("I am in the function");
                    LoadNextHour();

                }

                if (nextDialogue == null)
                {
                    this.gameObject.SetActive(false);
                }
            }
            else if (nextDialogue == null)
            {
                this.gameObject.SetActive(false);
            }

        }








    }

    public void LoadNextDialogue(DialogueScriptableObject dso)
    {
        hasntAddedItem = true;
        dialogueFinished = false;
        generatedButtons = false;
        _dialogueText.text = "";
        imageInt = 0;
        loadedDialogue = dso;
        choiceDialogues = loadedDialogue.dialogueChoice;
        sentences.Dequeue();
        StopAllCoroutines();
        sentence = loadedDialogue.dialogue.ToString();
        sentences.Enqueue(sentence);


        if(loadedDialogue.goNextHour == true)
        {
            FindAnyObjectByType<PlayerController>().canProgressToNextHour = true;
        }


        if (loadedDialogue.itemToAdd != null)
        {
            AddItem();
        }


        if (loadedDialogue.nextDialogue != null)
        {
            nextDialogue = loadedDialogue.nextDialogue;
        }
        else
        {
            nextDialogue = null;
        }

        if (loadedDialogue.nameSpace != null)
        {
            _namespace.text = loadedDialogue.nameSpace;
        }

        if (loadedDialogue.dialogue != null)
        {

            StartCoroutine(TypeSentence(sentence));
        }

        if (loadedDialogue.art.Length != 0)
        {

            image = loadedDialogue.art[imageInt];
            imageOverride.SetActive(true);
        }
        else
        {
            imageOverride.SetActive(false);
        }

        if (loadedDialogue.sc != null)
        {
            dialogueAudioEngine.clip = loadedDialogue.sc;
            dialogueAudioEngine.Play();
        }


    }


    IEnumerator TypeSentence (string sentence)
    {



        foreach (char letter in sentence.ToCharArray())
        {

            if (_dialogueText.text != thisSentence)
            {

                if(loadedDialogue.art.Length != 0)
                {
                    if (imageInt == loadedDialogue.art.Length - 1)
                    {
                        imageInt = 0;
                    }
                    else
                    {
                        imageInt++;
                    }

                    image = loadedDialogue.art[imageInt];
                }





            }


            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);

        }

        dialogueFinished = true;
        GenerateChoices();
        Debug.Log(dialogueFinished);

            
    }

    public void LoadNewDialogue()
    {


        if (loadedDialogue != null)
        {

            hasntAddedItem = true;
            _dialogueText.text = "";
            sentences = new Queue<string>();
            sentence = loadedDialogue.dialogue.ToString();
            sentences.Enqueue(sentence);


            imageInt = 0;

            if(loadedDialogue.itemToAdd != null)
            {
                AddItem();
            }

            if (loadedDialogue.goNextHour == true)
            {
                FindAnyObjectByType<PlayerController>().canProgressToNextHour = true;
            }

            if (loadedDialogue.nextDialogue != null)
            {
                nextDialogue = loadedDialogue.nextDialogue;
            }



            if (loadedDialogue.nameSpace != null)
            {
                _namespace.text = loadedDialogue.nameSpace;
            }

            if (loadedDialogue.dialogue != null)
            {
                StartCoroutine(TypeSentence(sentence));
            }

            if (loadedDialogue.art.Length != 0)
            {

                image = loadedDialogue.art[imageInt];
                imageOverride.SetActive(true);
            }
            else
            {
                imageOverride.SetActive(false);
            }


            if (loadedDialogue.sc != null)
            {
                dialogueAudioEngine.clip = loadedDialogue.sc;
                dialogueAudioEngine.Play();
            }


        }


    }


    public void LoadNextHour()
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        pc.canProgressToNextHour = true;
        goNextHour = false;
        


    }

    public void MakeChoice(DialogueScriptableObject dso)
    {
        LoadNextDialogue(dso);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Destroy(cui);
    }

    public void GenerateChoices()
    {

        if (dialogueFinished && waitingForResponse && !generatedButtons)
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            choiceDialogues = loadedDialogue.dialogueChoice;
            choiceUI.SetActive(true);
            waitingForResponse = true;


            cui = Instantiate(choiceUI, canvasParent);
            buttons = cui.GetComponentsInChildren<Button>().ToList();




            foreach (Button button in buttons)
            {


                if (choiceStep == choiceDialogues.Count)
                {
                    Destroy(button.gameObject);
                    buttons.Remove(button);

                    if(buttons.Count > choiceDialogues.Count)
                    {
                        Destroy(buttons[choiceStep].gameObject);
                        buttons.Remove(buttons[choiceStep]);
                    }


                    break;



                }

                Debug.Log(buttons.Count);


                DialogueScriptableObject discrio = choiceDialogues[choiceStep];
                button.onClick.AddListener(() => MakeChoice(discrio));
                Check(choiceDialogues[choiceStep]);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choiceDialogues[choiceStep].dialogueChoicePreview;
                choiceStep++;
            }
            generatedButtons = true;

        }
        choiceStep = 0;
    }


    public void Check(DialogueScriptableObject dso)
    {
        Debug.Log(dso);


    }


    public void AddItem()
    {
        GameObject.FindObjectOfType<PlayerInventory>().playerItems.Add(loadedDialogue.itemToAdd.gameObject);
        hasntAddedItem = false;



    }
}
