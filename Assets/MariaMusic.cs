using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaMusic : MonoBehaviour
{
    public DialogueEngine de;
    public AudioSource aus;
    public DialogueScriptableObject dso;

    void Start()
    {
        aus.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(de.loadedDialogue != null)
        {
            if (de.loadedDialogue == dso)
            {
                aus.enabled = true;
            }
        }


    }
}
