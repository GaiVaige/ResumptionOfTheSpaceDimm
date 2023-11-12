using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.Progress;

public class ScriptableObjectContainer : MonoBehaviour
{
    public DialogueScriptableObject dso;
    public bool noMore;
    public DialogueScriptableObject dsoToAdd;



    public bool doCounter;
    public int counter;
    public int max;

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

                if (noMore)
                {

                    if (dsoToAdd != null)
                    {

                        dso = dsoToAdd;
                    }
                    else
                    {

                        Destroy(this);
                    }

                }
            }
        }




    }
}
