using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(AudioSource))]
public class SoundClip : MonoBehaviour
{

    public AudioSource aus;
    public UIManager uim;
    public bool playConstant;



    public void Awake()
    {
        uim = FindObjectOfType<UIManager>();
        aus = GetComponent<AudioSource>();


        if (!playConstant)
        {
            aus.enabled = false;
        }

    }


    public void Update()
    {


        if (playConstant)
        {
            aus.enabled = true;
        }
        else if (uim.pauseUI.activeInHierarchy)
        {
            aus.enabled = false;
        }



    }
}

