using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCoordinate
{
    public int x, z;

    public MapCoordinate(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}

public class MazeGenerator : MonoBehaviour
{
    private List<MapCoordinate> directions = new List<MapCoordinate>()
    {
        new MapCoordinate(1, 0),
        new MapCoordinate(0, 1),
        new MapCoordinate(-1, 0),
        new MapCoordinate(0, -1)
    };

    private int _width;     // x-axis
    private int _depth;     // z axis

    [SerializeField]
    private GameObject _mazeContainer;

    private byte[,] _map;
    private int _scale = 6;

    // Start is called before the first frame update
    void Start()
    {
        _width = UIManager._mazeWidth;
        _depth = UIManager._mazeDepth;

        InitializeMaze();
        GenerateMaze();
    }

    private void InitializeMaze()
    {
        // define size of map array
        _map = new byte[_width, _depth];

        // 1 = wall, 0 = tunnel
        // Initialize maze to all walls and no tunnels
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {
                _map[x, z] = 1;
            }
        }
    }

    private void GenerateMaze()
    {
        for (int z = 0; z < _depth; z++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x * _scale, 0, z * _scale);
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.localScale = new Vector3(_scale, _scale, _scale);
                    wall.transform.parent = _mazeContainer.transform;
                    wall.transform.position = pos;
                }
            }
        }
    }

    public void UpdateMazeWidth(int newWidth)
    {
        _width = newWidth;
    }
}
