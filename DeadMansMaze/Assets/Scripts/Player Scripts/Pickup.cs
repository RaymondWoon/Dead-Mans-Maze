﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Health,
    Pistol_Ammo,
    Rifle_Ammo,
    Key, 
    Athelas
}

public class Pickup : MonoBehaviour
{

    [SerializeField] private PickupType _type;
    [SerializeField] private int _value;

    [SerializeField] private AudioClip _pickupSFX;

    [Header("Movement")]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _bobSpeed;
    [SerializeField] private float _bobHeight;

    //[Header("Door")]
    //[SerializeField] private GameObject _door;

    //[Header("Inventory")]
    //[SerializeField] private PlayerInventory _inventory;

    private Vector3 _startPos;
    private bool _isBobbingUp;


    // Start is called before the first frame update
    void Start()
    {
        // the start position for the pickup item
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the pickup
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);

        // bobbing range
        Vector3 offset = _isBobbingUp ? new Vector3(0, _bobHeight / 2, 0) : new Vector3(0, -_bobHeight / 2, 0);

        // move in the bobbing direction
        transform.position = Vector3.MoveTowards(transform.position, _startPos + offset, _bobSpeed * Time.deltaTime);

        // limit bobbing to the range
        if (transform.position == _startPos + offset)
            _isBobbingUp = !_isBobbingUp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (_type)
            {
                case PickupType.Pistol_Ammo:
                    AddItemToInventory("pistol", new Dictionary<string, int> { { "ammo", _value } });
                    GunPlay.PistolBulletStock += _value;
                    break;

                case PickupType.Rifle_Ammo:
                    AddItemToInventory("rifle", new Dictionary<string, int> { { "ammo", _value } });
                    GunPlay.RifleBulletStock += _value;
                    break;

                case PickupType.Key:
                    MainManager.Instance.MazeKeyFound = true;
                    AddItemToInventory("key");
                    break;

                case PickupType.Athelas:
                    Debug.Log("Pickup athelas");
                    MainManager.Instance.AthelasFound = true;
                    AddItemToInventory("athelas");
                    break;

                case PickupType.Health:
                    Debug.Log("Pickup health");
                    PlayerAction.GetHealth(_value);
                    break;
            }

            // play soundeffect
            other.GetComponent<AudioSource>().PlayOneShot(_pickupSFX);

            // destroy the pickup
            Destroy(gameObject);
        }
    }

    private void AddItemToInventory(string itemToAdd)
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        PlayerInventory _inventory = _player.GetComponent<PlayerInventory>();
        _inventory.AddItem(itemToAdd);
    }
    //AddItem("pistol", new Dictionary<string, int> { { "ammo", 30 } });
    private void AddItemToInventory(string itemToAdd, Dictionary<string, int> context)
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        PlayerInventory _inventory = _player.GetComponent<PlayerInventory>();
        _inventory.AddItem(itemToAdd, context);
    }

    //private void AddItemToInventory(int id, Dictionary<string, int> context)
    //{
    //    GameObject _player = GameObject.FindGameObjectWithTag("Player");
    //    PlayerInventory _inventory = _player.GetComponent<PlayerInventory>();
    //    _inventory.AddItem(id, context);
    //}
}

