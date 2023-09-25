using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _slotPanel;

    private List<UIItem> _uiItems = new List<UIItem>();
    private int noOfSlots = 4;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < noOfSlots; i++)
        {
            GameObject instance = Instantiate(_slotPrefab);
            instance.transform.SetParent(_slotPanel);
            _uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    public void UpdateSlot(int slot, InventoryItem item)
    {
        _uiItems[slot].UpdateItem(item);
    }

    public void UpdateSlot(string name, InventoryItem item)
    {
        UpdateSlot(_uiItems.FindIndex(i => i.item.title == name), item);
    }

    public void AddNewItem(InventoryItem item)
    {
        UpdateSlot(_uiItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(InventoryItem item)
    {
        UpdateSlot(_uiItems.FindIndex(i => i.item == item), null);
    }
}
