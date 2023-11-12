using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

    public AudioSource aus;


    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        aus.PlayOneShot(clip);
    }


}


