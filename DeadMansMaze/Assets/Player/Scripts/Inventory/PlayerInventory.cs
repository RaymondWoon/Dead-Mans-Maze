using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// This class is not a MonoBehavior as it will live as a property of the player.
// Done by Alwin. Replaced by Raymond
//public class Inventory
//{
//    // Declare class variables
//    private List<string> keyItems =  new List<string>();


//    public bool HasItem(string k)
//    {
//        return keyItems.Contains(k.ToLower());
//    }

//    public void AddItem(string k)
//    {
//        keyItems.Add(k.ToLower());
//    }

//    public int GetNumberOfKeyItem(string k)
//    {
//        int count = 0;
//        foreach (string item in keyItems) {
//            if (item.ToLower() == k.ToLower()) count++;
//        }
//        return count;
//    }
//}


public class PlayerInventory : MonoBehaviour
{
    public InventoryItemDatabase _itemDatabase;

    public List<InventoryItem> _playerItems; // = new List<InventoryItem>();
    public UIInventory _inventoryUI;

    private void Awake()
    {
        _playerItems = new List<InventoryItem>();
    }

    private void Start()
    {
        // Initial player assets
        AddItem("pistol", new Dictionary<string, int> { { "ammo", 30 } });
        AddItem("rifle", new Dictionary<string, int> { { "ammo", 90 } });
        //AddItem("key");
        //AddItem("athelas");
    }

    public void AddItem(int id)
    {
        InventoryItem itemToAdd = _itemDatabase.GetItem(id);
        _playerItems.Add(itemToAdd);
        _inventoryUI.AddNewItem(itemToAdd);
        //Debug.Log("Added item: " + itemToAdd.title);
    }

    public void AddItem(string itemName)
    {
        InventoryItem itemToAdd = _itemDatabase.GetItem(itemName);
        _playerItems.Add(itemToAdd);
        _inventoryUI.AddNewItem(itemToAdd);
        //Debug.Log("Added item: " + itemToAdd.title);
    }

    public void AddItem(int id, Dictionary<string, int> context)
    {
        InventoryItem itemToAdd = _itemDatabase.GetItem(id);

        foreach (var kvp in context)
        {
            if (!itemToAdd.stats.ContainsKey(kvp.Key))
            {
                itemToAdd.stats.Add(kvp.Key, kvp.Value);
                _playerItems.Add(itemToAdd);
                _inventoryUI.AddNewItem(itemToAdd);
            }
            else
            {
                itemToAdd.stats[kvp.Key] += kvp.Value;

                var result = from item in _playerItems where item.id == id select item;
                result.First().stats = itemToAdd.stats;
                _inventoryUI.UpdateSlot(id, itemToAdd);

                foreach (InventoryItem item in _playerItems)
                {
                    Debug.Log(item.title);
                    Debug.Log(item.stats);
                }
            }
        }

    }

    public void AddItem(string itemName, Dictionary<string, int> context)
    {
        InventoryItem itemToAdd = _itemDatabase.GetItem(itemName);

        foreach (var kvp in context)
        {
            if (!itemToAdd.stats.ContainsKey(kvp.Key))
            {
                itemToAdd.stats.Add(kvp.Key, kvp.Value);
                _playerItems.Add(itemToAdd);
                _inventoryUI.AddNewItem(itemToAdd);
            }
            else
            {
                itemToAdd.stats[kvp.Key] += kvp.Value;

                var result = from item in _playerItems where item.title == itemName select item;
                result.First().stats = itemToAdd.stats;
                _inventoryUI.UpdateSlot(itemName, itemToAdd);
            }
        }


        //Debug.Log("Added item: " + itemToAdd.title);
    }

    public InventoryItem CheckForItem(int id)
    {
        return _playerItems.Find(item => item.id == id);
    }

    public InventoryItem CheckForItem(string itemName)
    {
        return _playerItems.Find(item => item.title == itemName);
    }

    public void RemoveItem(int id)
    {
        InventoryItem itemToRemove = CheckForItem(id);

        if (itemToRemove != null)
        {
            _playerItems.Remove(itemToRemove);
            _inventoryUI.RemoveItem(itemToRemove);
            //Debug.Log("Item removed: " + itemToRemove.title);
        }
    }


}