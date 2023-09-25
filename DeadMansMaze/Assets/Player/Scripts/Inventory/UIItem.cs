using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public InventoryItem item;
    private Image _spriteImage;
    private Text _text;

    private void Awake()
    {
        _spriteImage = GetComponent<Image>();
        _text = GetComponentInChildren<Text>();
        UpdateItem(null);
    }

    public void UpdateItem(InventoryItem item)
    {
        this.item = item;

        if (this.item != null)
        {
            _spriteImage.color = Color.white;
            _spriteImage.sprite = this.item.icon;

            if (item.stats.Count > 0)
            {
                if (item.stats.ContainsKey("ammo"))
                {
                    _text.text = item.stats["ammo"].ToString();
                }
            }
            else
            {
                _text.text = "";
            }
        }
        else
        {
            _text.text = "";
            _spriteImage.color = Color.clear;
        }
    }
}
