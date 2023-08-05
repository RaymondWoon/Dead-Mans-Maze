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

    public GameObject straight_piece;
    public GameObject crossroad_piece;
    public GameObject deadend_piece;

    // Start is called before the first frame update
    void Start()
    {
        _width = UIManager._mazeWidth;
        _depth = UIManager._mazeDepth;

        InitializeMaze();
        GenerateMaze(5, 5);
        DrawMaze();
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

    private void GenerateMaze(int x, int z)
    {
        if (CountOrthogonalNeighbours(x, z) >= 2)
            return;

        _map[x, z] = 0;

        directions.Shuffle();

        GenerateMaze(x + directions[0].x, z + directions[0].z);
        GenerateMaze(x + directions[1].x, z + directions[1].z);
        GenerateMaze(x + directions[2].x, z + directions[2].z);
        GenerateMaze(x + directions[3].x, z + directions[3].z);
    }

    private void DrawMaze()
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
                else if (Search2D(x, z, new int[] { 8, 0, 8, 1, 0, 1, 8, 0, 8 }))
                {
                    Vector3 pos = new Vector3(x * _scale, 0, z * _scale);
                    GameObject maze_piece = Instantiate(straight_piece, pos, Quaternion.identity);
                    maze_piece.transform.parent = _mazeContainer.transform;
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 0, 0, 0, 8, 1, 8 }))
                {
                    Vector3 pos = new Vector3(x * _scale, 0, z * _scale);
                    GameObject maze_piece = Instantiate(straight_piece, pos, Quaternion.identity);
                    maze_piece.transform.parent = _mazeContainer.transform;
                    maze_piece.transform.Rotate(0, 90, 0);
                }
                else if (Search2D(x, z, new int[] { 1, 0, 1, 0, 0, 0, 1, 0, 1}))
                {
                    Vector3 pos = new Vector3(x * _scale, 0, z * _scale);
                    GameObject maze_piece = Instantiate(crossroad_piece, pos, Quaternion.identity);
                    maze_piece.transform.parent = _mazeContainer.transform;
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 1, 0, 1, 8, 0, 8 }))
                {
                    Vector3 pos = new Vector3(x * _scale, 0, z * _scale);
                    GameObject maze_piece = Instantiate(deadend_piece, pos, Quaternion.identity);
                    maze_piece.transform.parent = _mazeContainer.transform;
                }
            }
        }
    }

    private bool Search2D(int c, int r, int[] pattern)
    {
        int count = 0;
        int pos = 0;

        for (int z = 1; z > -2; z--)
        {
            for (int x = -1; x < 2; x++)
            {
                if (pattern[pos] == _map[c + x, r + z] || pattern[pos] == 8)
                {
                    count++;
                }

                pos++;
            }
        }

        return (count == 9);
    }

    public void UpdateMazeWidth(int newWidth)
    {
        _width = newWidth;
    }

    private int CountOrthogonalNeighbours(int x, int z)
    {
        //  initiate counter
        int count = 0;

        // Check if outside of limits and return an unexpected value
        if (x <= 0 || x >= _width - 1 || z <= 0 || z >= _depth - 1)
            return 5;

        // @ 180 degree
        if (_map[x - 1, z] == 0) 
            count++;

        // @ 0 degree
        if (_map[x + 1, z] == 0)
            count++;

        // @ 90 degree
        if (_map[x, z + 1] == 0)
            count++;

        // @ -90 degree
        if (_map[x, z - 1] == 0)
            count++;

        return count;
    }


}


