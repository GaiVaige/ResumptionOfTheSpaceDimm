using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEngine : MonoBehaviour
{
    public DialogueScriptableObject loadedDialogue;
    public DialogueScriptableObject nextDialogue;
    public string thisSentence;
    public TextMeshProUGUI _namespace;
    public TextMeshProUGUI _dialogueText;
    public Sprite image;
    public int imageInt;
    UIManager uim;
    public bool dialogueFinished;
    public GameObject player;
    public Transform respawn;

    public bool goNextHour;
    public GameObject currentHour;
    public GameObject nextHour;
    public List<GameObject> nextHours;

    public AudioSource dialogueAudioEngine;

    private Queue<string> sentences;
    string sentence;

    void Start()
    {

        uim = FindObjectOfType<UIManager>();
        nextHour = nextHours[0];
        goNextHour = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (loadedDialogue.goNextHour)
        {
            goNextHour = true;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (nextDialogue != null)
            {
                LoadNextDialogue();
            }
            else if (dialogueFinished)
            {
                Debug.Log("I am in the first check");
                if (goNextHour)
                {
                    Debug.Log("I am in the function");
                    LoadNextHour();

                    goNextHour = false;
                    this.gameObject.SetActive(false);
                }
            }
            else if (nextDialogue == null)
            {
                this.gameObject.SetActive(false);
            }

        }








    }

    public void LoadNextDialogue()
    {
        _dialogueText.text = "";
        
        imageInt = 0;
        loadedDialogue = nextDialogue;
        sentences.Dequeue();
        StopAllCoroutines();
        sentence = loadedDialogue.dialogue.ToString();
        sentences.Enqueue(sentence);


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

        if (loadedDialogue.art != null)
        {
            image = loadedDialogue.art[imageInt];
        }

        if(loadedDialogue.sc != null)
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


            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);

        }

        dialogueFinished = true;
        Debug.Log(dialogueFinished);

            
    }

    public void LoadNewDialogue()
    {
        if (loadedDialogue != null)
        {
            _dialogueText.text = "";
            sentences = new Queue<string>();
            sentence = loadedDialogue.dialogue.ToString();
            sentences.Enqueue(sentence);

            imageInt = 0;

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

            if (loadedDialogue.art != null)
            {
                image = loadedDialogue.art[imageInt];
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


        player.transform.position = respawn.position;

        GameObject nextHourDestroy = Instantiate(nextHour);
        nextHours.Remove(nextHour);
        Destroy(currentHour);
        currentHour = nextHourDestroy;

        if(nextHours.Count != 0)
        {
            nextHour = nextHours[0];
        }


    }
}
