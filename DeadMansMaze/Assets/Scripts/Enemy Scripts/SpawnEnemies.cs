﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [Header("Enemies")]
    //[SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private GameObject _boss;

    //public List<MapCoordinate> straightPieces = new List<MapCoordinate>();
    //public List<MapCoordinate> cornerPieces = new List<MapCoordinate>();
    //public List<MapCoordinate> crossPieces = new List<MapCoordinate>();
    //public List<MapCoordinate> deadendPieces = new List<MapCoordinate>();
    //public List<MapCoordinate> tPieces = new List<MapCoordinate>();

    // Variables
    private bool _isBossActive;
    private int _numOfEnemies = MainManager.Instance.NumberOfEnemies;

    private enum MAZE_PIECE
    {
        STRAIGHT,
        CORNER,
        CROSS,
        DEADEND,
        T
    }

    // Start is called before the first frame update
    void Start()
    {
        _isBossActive = false;

        for (int i = 0; i < _numOfEnemies; i++)
        {
            MAZE_PIECE mp = Extensions.RandomEnumValue<MAZE_PIECE>();

            InstantiateEnemy(mp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateEnemy(MAZE_PIECE mp)
    {
        MapCoordinate mapPt = new MapCoordinate(0, 0);

        //Debug.Log("Corner Pieces -> " + MazeGenerator.cornerPieces.Count);

        switch (mp)
        {
            case MAZE_PIECE.CORNER:
                mapPt = MazeGenerator.cornerPieces[Random.Range(0, MazeGenerator.cornerPieces.Count - 1)];
                break;

            case MAZE_PIECE.CROSS:
                if (MazeGenerator.crossPieces.Count == 0)
                {
                    InstantiateEnemy(MAZE_PIECE.CORNER);
                    return;
                }

                mapPt = MazeGenerator.crossPieces[Random.Range(0, MazeGenerator.crossPieces.Count - 1)];
                break;

            case MAZE_PIECE.DEADEND:
                mapPt = MazeGenerator.deadendPieces[Random.Range(0, MazeGenerator.deadendPieces.Count - 1)];
                break;

            case MAZE_PIECE.STRAIGHT:
                mapPt = MazeGenerator.straightPieces[Random.Range(0, MazeGenerator.straightPieces.Count - 1)];
                break;

            case MAZE_PIECE.T:
                mapPt = MazeGenerator.tPieces[Random.Range(0, MazeGenerator.tPieces.Count - 1)];
                break;
        }

        GameObject enemy = _enemy[Random.Range(0, _enemy.Length)];

        Vector3 pos = new Vector3((mapPt.x - MainManager.Instance.MazeWidth / 2) * MainManager.Instance.MazeScale, 0.1f, mapPt.z * MainManager.Instance.MazeScale + 3.0f);

        Instantiate(enemy, pos, Quaternion.identity);
        //Debug.Log(mapPt.x);
        //Debug.Log(mapPt.z);
    }
}