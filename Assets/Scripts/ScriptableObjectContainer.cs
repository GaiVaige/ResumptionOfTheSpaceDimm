using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptableObjectContainer : MonoBehaviour
{
    PlayerController de;

    [Tooltip("Set what dialogue to load when the character raycast returns this object, as a DialogueScriptableObject (RightClick in Assets Section > ScriptableObject)")]
    public DialogueScriptableObject dso;

    [Tooltip("Set a DialogueScriptableObject to replace the current one when dialogue is exhausted.")]
    public DialogueScriptableObject dsoToAdd;

    [Tooltip("Do not bother to set this true, it will auto delete.")]
    public bool noMore;

    [Tooltip("This increments whenver a player sees dialogue, set it to one to delete it after the first time, and so on. Ensure you check doCounter to have this logic run.")]
    public bool doCounter;
    public int counter;
    public int max;


    public bool doModelSwap;
    public GameObject modelToSwapTO;
    public GameObject modelToDisable;

    public bool destroyObjectToo;
    public AudioSource soound;

    public bool doSearchByName;
    public string soundName;
    public void Start()
    {
        de = FindObjectOfType<PlayerController>();

        if (doSearchByName)
        {
            soound = GameObject.Find(soundName).GetComponent<AudioSource>();
        }
    }


    public void Update()
    {

        if(dso != dsoToAdd)
        {
            if (doCounter)
            {
                if (counter >= max)
                {
                    noMore = true;
                }


            }
        }



        if (noMore && !doModelSwap)
        {








            if (dso.dsoToAddOveride != null)
            {
                dsoToAdd = dso.dsoToAddOveride;
            }

            if (dsoToAdd != null)
            {

                dso = dsoToAdd;


            }
            else
            {

                if (destroyObjectToo)
                {
                    Destroy(this.gameObject);
                }


                Destroy(this);
            }

        }


        if(noMore)
        {
            if (doModelSwap)
            {
                if (!de.isInDialogue)
                {
                    modelToSwapTO.SetActive(true);
                    modelToDisable.SetActive(false);
                    doModelSwap = false;
                    if (destroyObjectToo)
                    {
                        Destroy(this.gameObject);
                    }
                    Destroy(this);
                }



            }
        }
    }

    public void OnDestroy()
    {

        if(soound != null)
        {
            soound.enabled = false;
            soound.enabled = true;
            soound.Play();
        }



    }
}
