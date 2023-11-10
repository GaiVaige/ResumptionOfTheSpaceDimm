using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueEngine : MonoBehaviour
{
    public DialogueScriptableObject loadedDialogue;
    public TextMeshProUGUI _namespace;
    public TextMeshProUGUI _dialogueText;
    public SpriteRenderer image;
    public AudioSource sound;
    public AudioClip dialogueSound;

    void Start()
    {
        if (loadedDialogue.nameSpace != null)
        {
            _namespace.text = loadedDialogue.nameSpace;
        }

        if (loadedDialogue.dialogue != null)
        {
            _dialogueText.text = loadedDialogue.dialogue;
        }

        if (loadedDialogue.art != null)
        {
            image.sprite = loadedDialogue.art;
        }

        if (loadedDialogue.voiceLine != null)
        {
            sound.PlayOneShot(loadedDialogue.voiceLine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) && loadedDialogue.nextDialogue != null))
        {
            LoadNextDialogue();

        }
        else if ((Input.GetKeyDown(KeyCode.Space) && loadedDialogue.nextDialogue == null))
        {
            Debug.Log("I would turn off the UI here but LAYZ");
        }
    }

    public void LoadNextDialogue()
    {
        loadedDialogue = loadedDialogue.nextDialogue;

        if (loadedDialogue.nameSpace != null)
        {
            _namespace.text = loadedDialogue.nameSpace;
        }

        if (loadedDialogue.dialogue != null)
        {
            _dialogueText.text = loadedDialogue.dialogue;
        }

        if (loadedDialogue.art != null)
        {
            image.sprite = loadedDialogue.art;
        }

        if (loadedDialogue.voiceLine != null)
        {
            sound.PlayOneShot(loadedDialogue.voiceLine);
        }
    }
}
