using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCheck : MonoBehaviour
{
    Button button;
    DialogueEngine de;
    void Start()
    {
        button = GetComponent<Button>();
        de = GetComponentInParent<DialogueEngine>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(de.generatedButtons == true)
        {
            if (button.onClick == null)
            {
                Destroy(this.gameObject);
            }
        }


    }
}
