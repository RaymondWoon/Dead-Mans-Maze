using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is not a MonoBehavior as it will live as a property of the player.
public class InventoryY
{
    // Declare class variables
    private List<string> keyItems =  new List<string>();


    public bool HasItem(string k)
    {
        return keyItems.Contains(k.ToLower());
    }

    public void AddItem(string k)
    {
        keyItems.Add(k.ToLower());
    }

    public int GetNumberOfKeyItem(string k)
    {
        int count = 0;
        foreach (string item in keyItems) {
            if (item.ToLower() == k.ToLower()) count++;
        }
        return count;
    }
}
