using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    public string nameSpace;
    public string dialogue;
    public Sprite[] art;
    public DialogueScriptableObject nextDialogue;
    public PlayerItem itemToAdd;
}