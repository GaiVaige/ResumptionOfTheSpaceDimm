using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectContainer : MonoBehaviour
{
    public DialogueScriptableObject dso;
    public bool noMore;

    public bool doCounter;
    public int counter;
    public int max;

    public void Update()
    {

        if (doCounter)
        {
            if (counter >= max)
            {
                noMore = true;
            }

            if (noMore)
            {
                Destroy(this);
            }
        }

    }
}
