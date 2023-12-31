using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCManager : MonoBehaviour
{
    PlayerInventory pi;
    public int currentCapacity;
    public ScriptableObjectContainer soc;

    public GameObject modeltoswapto;
    public GameObject modeltodelete;

    [Tooltip("Here you can set what items to check for, drag and drop the prefab you would give the player into the slot. Every time the player gets an item, all NPCs in the scene will check.")]
    public ItemData[] items;


    public void Start()
    {
        pi = FindAnyObjectByType<PlayerInventory>();
        currentCapacity = pi.playerItems.Count;
        soc = GetComponent<ScriptableObjectContainer>();

        if (soc != null)
        {
            if (soc.doModelSwap)
            {
                soc.modelToDisable = modeltodelete;
                soc.modelToSwapTO = modeltoswapto;
            }
        }
    }


    public void Update()
    {







        if(pi.playerItems.Count != currentCapacity)
        {
            foreach(ItemData pitem in items)
            {
                if (pi.playerItems.Contains(pitem.itemToCheckFor.gameObject) && pi.playerItems[pi.playerItems.Count - 1] == pitem.itemToCheckFor.gameObject)
                {

                    if (this.gameObject.GetComponent<ScriptableObjectContainer>())
                    {
                        Destroy(this.gameObject.GetComponent<ScriptableObjectContainer>());
                        Debug.Log("Itemdestroyed");
                    }

                    Debug.Log("nextcheck");


                    if (!pitem.dialogueUpdated)
                    {
                        System.Type type = pitem.soc.GetType();
                        Component copy = this.gameObject.AddComponent(type);

                        System.Reflection.FieldInfo[] fields = type.GetFields();
                        Debug.Log("I am copying");
                        foreach (System.Reflection.FieldInfo field in fields)
                        {
                            field.SetValue(copy, field.GetValue(pitem.soc));
                        }

                        pitem.hasItem = true;
                        soc = GetComponent<ScriptableObjectContainer>();

                        if(modeltodelete && modeltoswapto != null)
                        {
                            soc.modelToDisable = modeltodelete;
                            soc.modelToSwapTO = modeltoswapto;
                        }

                        pitem.dialogueUpdated = true;

                    }





                }
            }

            currentCapacity = pi.playerItems.Count;
        }
    }
}


[System.Serializable]
public class ItemData
{
    public bool hasItem;
    public bool dialogueUpdated;
    public PlayerItem itemToCheckFor;
    public ScriptableObjectContainer soc;



}
