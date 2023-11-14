using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGetter : MonoBehaviour
{

    DialogueEngine de;
    Image thisArt;

    void Start()
    {
        de = GetComponentInParent<DialogueEngine>();
        thisArt = GetComponent<Image>();
        thisArt.sprite = de.image;
    }

    // Update is called once per frame
    void Update()
    {
        thisArt.sprite = de.image;
    }
}
