using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    public string nameSpace;
    public string dialogue;
    public AudioClip voiceLine;
    public Sprite art;
    public DialogueScriptableObject nextDialogue;
}
