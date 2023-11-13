using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/DialogueManagerScriptableObject", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{

    [Header("Settings")]
    [Tooltip("Punch everything in here, just drag and drop, the dialogue engine is fully automated.")]


    public string nameSpace;
    public string dialogue;
    public Sprite[] art;
    public DialogueScriptableObject nextDialogue;

    [Tooltip("DO NOT MAKE THIS GREATER THAN 3 IT WILL CRASH.")]
    public List<DialogueScriptableObject> dialogueChoice;
    public string dialogueChoicePreview;
    public DialogueScriptableObject dsoToAddOveride;
    public PlayerItem itemToAdd;
    public AudioClip sc;

    public bool goNextHour;

}
