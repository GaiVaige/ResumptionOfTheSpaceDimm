using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public string itemName;

    public void Start()
    {
        this.name = itemName;
    }
}
