using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SamsonEndOfHourManager : MonoBehaviour
{
    public SamsonDatas itemsToHave;
    PlayerInventory pi;
    int currentCount;
    [Header("MAKE THIS THE SAME LENGHT AS THE SAMSON ITEMS")]

    public bool[] hasItems;
    public bool allItems;
    public bool hasntGivenItem = true;


    private void Start()
    {
        pi = FindObjectOfType<PlayerInventory>();
        currentCount = pi.playerItems.Count;
    }


    // Update is called once per frame
    void Update()
    {
        if(pi.playerItems.Count != currentCount)
        {

            for(int i = 0; i < itemsToHave.itemsToHave.Length; i++)
            {
                if (pi.playerItems.Contains(itemsToHave.itemsToHave[i].gameObject))
                {
                    hasItems[i] = true;




                    }
                }
            }







        if (IsAllTrue(hasItems) && hasntGivenItem)
        {

                pi.playerItems.Add(itemsToHave.itemToGive.gameObject);
                hasntGivenItem = false;


        }

    }

    public bool IsAllTrue(bool[] collection)
    {
        for (int i = 0; i < collection.Length - 1; i++)
        {
            if (!hasItems[i])
            {
                return false;
            }


        }
        return true;

    }

}



[System.Serializable]
public class SamsonDatas
{
    public bool hasAllItems;
    public PlayerItem[] itemsToHave;
    public PlayerItem itemToGive;
}



