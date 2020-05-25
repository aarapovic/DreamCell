using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items= new List<Item>();
    public int numberOfKeys;
    public int numberOfCoins;
    public float maxArrows=10;
    public float currentArrows;

    public void OnEnable()
    {
        
    }
    public void ReduceArrows(float arrowCost)
    {
        currentArrows -= arrowCost;
    }
    public void AddItem(Item itemToAdd)
    {
        if(!itemToAdd.isKey)
        {
            items.Add(itemToAdd);
        }
        if(itemToAdd.isKey)
        {
            numberOfKeys++;
        }else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }
    public bool CheckForItem(Item item)
    {
        if(items.Contains(item))
            { return true;
        }
        return false;
    }
}
