using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaMusic : MonoBehaviour
{
    DialogueEngine de;
    public AudioSource aus;
    public DialogueScriptableObject dso;

    void Start()
    {
        de = FindAnyObjectByType<DialogueEngine>();
        aus.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(de.loadedDialogue == dso)
        {
            aus.enabled=true;
        }
    }
}
