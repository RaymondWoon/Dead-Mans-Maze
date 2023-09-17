using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    [Header("Pickups")]
    [SerializeField] private GameObject _health;
    [SerializeField] private GameObject _pistolAmmo;
    [SerializeField] private GameObject _rifleAmmo;
    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _athelas;

    private MapCoordinate _keyPt;
    private bool _keyLoaded;
    private bool _athelasLoaded;

    // Start is called before the first frame update
    void Start()
    {
        // Default
        _keyLoaded = false;
        _athelasLoaded = false;

        // pistol ammo pickup
        LoadPistolAmmo(Extensions.RandomEnumValue<MAZE_PIECE>());

        // rifle ammo pickup
        LoadRifleAmmo(Extensions.RandomEnumValue<MAZE_PIECE>());

        // health pickup
        LoadHealth(Extensions.RandomEnumValue<MAZE_PIECE>());

        // key pickup
        // no parameter required. Will always load in a deadend.
        LoadKey();

        // Athelas pickup
        // no parameter required. Will always load in a deadend.
        LoadAthelas();
    }

    private void LoadPistolAmmo(MAZE_PIECE mp)
    {
        MapCoordinate mapPt = new MapCoordinate(0, 0);

        switch (mp)
        {
            case MAZE_PIECE.CORNER:
                mapPt = MazeGenerator.cornerPieces[Random.Range(0, MazeGenerator.cornerPieces.Count - 1)];
                break;

            case MAZE_PIECE.CROSS:
                if (MazeGenerator.crossPieces.Count == 0)
                {
                    LoadPistolAmmo(MAZE_PIECE.CORNER);
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

        Vector3 pos = new Vector3((mapPt.x - MainManager.Instance.MazeWidth / 2) * MainManager.Instance.MazeScale, 1f, mapPt.z * MainManager.Instance.MazeScale + 3.0f);

        Instantiate(_pistolAmmo, pos, Quaternion.identity);
    }


    private void LoadRifleAmmo(MAZE_PIECE mp)
    {
        MapCoordinate mapPt = new MapCoordinate(0, 0);

        switch (mp)
        {
            case MAZE_PIECE.CORNER:
                mapPt = MazeGenerator.cornerPieces[Random.Range(0, MazeGenerator.cornerPieces.Count - 1)];
                break;

            case MAZE_PIECE.CROSS:
                if (MazeGenerator.crossPieces.Count == 0)
                {
                    LoadPistolAmmo(MAZE_PIECE.CORNER);
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

        Vector3 pos = new Vector3((mapPt.x - MainManager.Instance.MazeWidth / 2) * MainManager.Instance.MazeScale, 1f, mapPt.z * MainManager.Instance.MazeScale + 3.0f);

        Instantiate(_rifleAmmo, pos, Quaternion.identity);
    }

    private void LoadHealth(MAZE_PIECE mp)
    {
        MapCoordinate mapPt = new MapCoordinate(0, 0);

        switch (mp)
        {
            case MAZE_PIECE.CORNER:
                mapPt = MazeGenerator.cornerPieces[Random.Range(0, MazeGenerator.cornerPieces.Count - 1)];
                break;

            case MAZE_PIECE.CROSS:
                if (MazeGenerator.crossPieces.Count == 0)
                {
                    LoadPistolAmmo(MAZE_PIECE.CORNER);
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

        Vector3 pos = new Vector3((mapPt.x - MainManager.Instance.MazeWidth / 2) * MainManager.Instance.MazeScale, 1f, mapPt.z * MainManager.Instance.MazeScale + 3.0f);

        Instantiate(_health, pos, Quaternion.identity);
    }

    private void LoadKey()
    {
        if (_keyLoaded)
            return;

        _keyPt = new MapCoordinate(0, 0);
        _keyPt = MazeGenerator.deadendPieces[Random.Range(0, MazeGenerator.deadendPieces.Count - 1)];

        //if (_keyPt.z < MainManager.Instance.MazeDepth * 0.4)
        //    LoadKey();

        Vector3 pos = new Vector3((_keyPt.x - MainManager.Instance.MazeWidth / 2) * MainManager.Instance.MazeScale, 1f, _keyPt.z * MainManager.Instance.MazeScale + 3.0f);

        Instantiate(_key, pos, Quaternion.identity);

        _keyLoaded = true;
    }

    private void XLoadAthelas()
    {
        if (_athelasLoaded)
            return;
        
        _ = new MapCoordinate(0, 0);
        MapCoordinate mapPt = MazeGenerator.deadendPieces[Random.Range(0, MazeGenerator.deadendPieces.Count - 1)];

        Vector3 pos = new Vector3(0, 0, 0);

        if (mapPt.x != _keyPt.x && mapPt.z != _keyPt.z)
        {
            pos = new Vector3((mapPt.x - MainManager.Instance.MazeWidth / 2) * MainManager.Instance.MazeScale, 0.25f, _keyPt.z * MainManager.Instance.MazeScale + 3.0f);
        }
        else
        {
            LoadAthelas();
        }

        if (mapPt.z != 0)
        {
            Instantiate(_athelas, pos, Quaternion.identity);
            _athelasLoaded = true;
        }
            
    }

    private void LoadAthelas()
    {
        MapCoordinate mapPt = new MapCoordinate(0, 0);

        mapPt = MazeGenerator.deadendPieces[Random.Range(0, MazeGenerator.deadendPieces.Count - 1)];

        Vector3 pos = new Vector3((mapPt.x - MainManager.Instance.MazeWidth / 2) * MainManager.Instance.MazeScale, 1f, mapPt.z * MainManager.Instance.MazeScale + 3.0f);

        Instantiate(_athelas, pos, Quaternion.identity);

        _athelasLoaded = true;
    }

}
