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


    private Queue<string> sentences;
    string sentence;

    void Start()
    {




        

    }

    // Update is called once per frame
    void Update()
    {





        if ((Input.GetKeyDown(KeyCode.Space) && nextDialogue != null))
        {
            LoadNextDialogue();
        }
        else if ((Input.GetKeyDown(KeyCode.Space) && nextDialogue == null))
        {
            this.gameObject.SetActive(false);
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
            loadedDialogue.sc.PlayOneShotToAudioSource();
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
                loadedDialogue.sc.PlayOneShotToAudioSource();
            }


        }
    }
}
