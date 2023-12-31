﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Variable declaration.
    [SerializeField]
    private int numberOfKeys;
    private int numberOfEnemies;
    public GameObject keyPrefab; // This should have the key prefab.
    public GameObject[] enemiesPrefabs; // This should have the enemies prefabs.

    // Start is called before the first frame update
    void Start()
    {
        generateKeys();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumberOfKeys(int i)
    {
        numberOfKeys = i;
    }

    public int GetNumberOfKeys()
    { 
        return numberOfKeys;
    }

    public void SetNumberOfEnemies(int i)
    {
        numberOfEnemies = i;
    }

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }

    private Vector3 getRandomLocation(Vector3 loc)
    { 
        return new Vector3(loc.x + Random.Range(-4, 4),
                            1,
                            loc.z + Random.Range(-4, 4));
    }

    private void generateKeys()
    {
        for (int i = 0; i < numberOfKeys; i++)
        {
            Vector3 location = getRandomLocation(transform.position);
            Instantiate(keyPrefab, location, Quaternion.identity);
        }
    }

    private void generateEnemy()
    {
        // Instantiate();
    }
}
