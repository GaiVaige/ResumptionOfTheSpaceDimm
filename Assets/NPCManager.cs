using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCManager : MonoBehaviour
{
    PlayerInventory pi;
    public int currentCapacity;

    public ItemData[] items;


    public void Start()
    {
        pi = FindAnyObjectByType<PlayerInventory>();
        currentCapacity = pi.playerItems.Count;
    }


    public void Update()
    {
        if(pi.playerItems.Count != currentCapacity)
        {
            foreach(ItemData pitem in items)
            {
                if (pi.playerItems.Contains(pitem.itemToCheckFor.gameObject))
                {

                    if (this.gameObject.GetComponent<ScriptableObjectContainer>())
                    {
                        Destroy(this.gameObject.GetComponent<ScriptableObjectContainer>());

                        
                    }

                    if (!this.gameObject.GetComponent<ScriptableObjectContainer>() && !pitem.dialogueShown)
                    {
                        System.Type type = pitem.soc.GetType();
                        Component copy = this.gameObject.AddComponent(type);

                        System.Reflection.FieldInfo[] fields = type.GetFields();

                        foreach (System.Reflection.FieldInfo field in fields)
                        {
                            field.SetValue(copy, field.GetValue(pitem.soc));
                        }
                    }

                    pitem.dialogueShown = true;
                    pitem.hasItem = true;
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
    public bool dialogueShown;
    public PlayerItem itemToCheckFor;
    public ScriptableObjectContainer soc;



}
