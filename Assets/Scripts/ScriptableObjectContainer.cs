using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectContainer : MonoBehaviour
{


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

    public bool destroyColliderToo;

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

        if (noMore)
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

                if (destroyColliderToo)
                {
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
                }


                Destroy(this);
            }

        }



    }
}
