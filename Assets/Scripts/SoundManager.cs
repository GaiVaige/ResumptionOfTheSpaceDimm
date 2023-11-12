using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioMixer))]
public class SoundManager : MonoBehaviour
{

    public AudioMixer aus;


    // Start is called before the first frame update
    void Start()
    {

    }



    public void SetValue(float soundLevel)
    {
        aus.SetFloat("volume", soundLevel);
    }

}


