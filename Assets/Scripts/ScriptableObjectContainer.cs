using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectContainer : MonoBehaviour
{
    public DialogueScriptableObject dso;
    public bool noMore;
    public DialogueScriptableObject dsoToAdd;



    public bool doCounter;
    public int counter;
    public int max;


    public void Start()
    {
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
