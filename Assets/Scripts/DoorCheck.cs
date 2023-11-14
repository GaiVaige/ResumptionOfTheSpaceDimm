using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class DoorCheck : MonoBehaviour
{

    public Animator anim;
    public PlayerItem piToHave;
    public AudioSource doorLockedSound;
    public DialogueScriptableObject doorLockedText;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        anim.SetTrigger("OpenDoor");
    }

    public void DoorLock()
    {
        doorLockedSound.enabled = false;
        doorLockedSound.enabled = true;

    }
}
