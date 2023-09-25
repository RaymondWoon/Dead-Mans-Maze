using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemDatabase : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();

    // Build inventory database on game start
    private void Awake()
    {
        BuildDatabase();
    }

    // Database of inventory items for game with defaults
    private void BuildDatabase()
    {
        items = new List<InventoryItem> {
            new InventoryItem(0, "pistol", new Dictionary<string, int>{}),
            new InventoryItem(1, "rifle", new Dictionary<string, int>{}),
            new InventoryItem(2, "key", new Dictionary<string, int>{}),
            new InventoryItem(3, "athelas", new Dictionary<string, int>{})
        };
    }

    // find inventory item by id
    public InventoryItem GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    // find inventory item by name
    public InventoryItem GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }
}
