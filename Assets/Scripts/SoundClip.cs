using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClip : MonoBehaviour
{
    public SoundManager sm;
    [SerializeField] private AudioClip thisSound;

    public bool playLooping;

    void Start()
    {
        sm = FindObjectOfType<SoundManager>();





    }


    public void PlayOneShotToAudioSource()
    {
        sm.PlaySound(thisSound);
    }

    public void PlayLooping()
    {

            sm.aus.loop = true;
            sm.PlaySound(thisSound);

    }
}
