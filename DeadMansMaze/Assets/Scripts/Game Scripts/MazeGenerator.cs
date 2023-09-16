using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class MapCoordinate
{
    public int x, z;

    public MapCoordinate(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}

public enum MAZE_PIECE
{
    STRAIGHT,
    CORNER,
    CROSS,
    DEADEND,
    T
}

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Elements")]
    [SerializeField] private GameObject _mazeContainer;
    [SerializeField] private GameObject _mazeWall;
    [SerializeField] private GameObject _straightPiece;
    [SerializeField] private GameObject _cornerPiece;
    [SerializeField] private GameObject _crossRoadPiece;
    [SerializeField] private GameObject _deadendPiece;
    [SerializeField] private GameObject _tPiece;

    [Header("Map")]
    [SerializeField] private TMP_Text _uiMap;
    //[SerializeField] private Text _uiMap;

    [Header("Player")]
    [SerializeField] private GameObject _player;

    [HideInInspector]
    public byte[,] _map;

    [HideInInspector]
    public static List<MapCoordinate> straightPieces = new List<MapCoordinate>();
    public static List<MapCoordinate> cornerPieces; // = new List<MapCoordinate>();
    public static List<MapCoordinate> crossPieces = new List<MapCoordinate>();
    public static List<MapCoordinate> deadendPieces = new List<MapCoordinate>();
    public static List<MapCoordinate> tPieces = new List<MapCoordinate>();

    private readonly List<MapCoordinate> directions = new List<MapCoordinate>()
    {
        new MapCoordinate(1, 0),
        new MapCoordinate(0, 1),
        new MapCoordinate(-1, 0),
        new MapCoordinate(0, -1)
    };

    private int _width;     // x-axis
    private int _depth;     // z-axis
    private int _scale;

    //private GameObject _player;

    private void Awake()
    {
        cornerPieces = new List<MapCoordinate>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (MainManager.Instance != null)
        {
            _width = MainManager.Instance.MazeWidth;
            _depth = MainManager.Instance.MazeDepth;
            _scale = MainManager.Instance.MazeScale;
        }

        //_player = GameObject.FindWithTag("Player");

        InitializeMaze();
        GenerateMaze((int)(_width / 2), 1);
        DrawMaze();
    }

    private void Update()
    {
        UpdateMap();
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
                if (x == (int)(_width / 2) && z == 0)
                {
                    _map[x, z] = 8;
                }
                else
                {
                    _map[x, z] = 1;
                }
            }
        }
    }

    // Generate maze using a recursive DFS algorithm
    private void GenerateMaze(int x, int z)
    {
        // Determine neighbours and backtrack if necessary 
        if (CountOrthogonalNeighbours(x, z) >= 2)
            return;

        _map[x, z] = 0;

        // in-place shuffle using Fisher-Yates algorithm
        directions.Shuffle();

        // Iterate thru each orthogonal direction
        GenerateMaze(x + directions[0].x, z + directions[0].z);
        GenerateMaze(x + directions[1].x, z + directions[1].z);
        GenerateMaze(x + directions[2].x, z + directions[2].z);
        GenerateMaze(x + directions[3].x, z + directions[3].z);
    }

    // Insert the maze pieces
    private void DrawMaze()
    {
        for (int z = 0; z < _depth; z++)
        {
            for (int x = 0; x < _width; x++)
            {
                Vector3 pos;

                if (_map[x, z] == 1)
                {
                    pos = new Vector3((x - _width / 2) * _scale, 3.0f, z * _scale + 3.0f);
                }
                else
                {
                    pos = new Vector3((x - _width / 2) * _scale, 0.1f, z * _scale + 3.0f);
                }

                if (_map[x, z] == 1)
                {
                    // Walls
                    GameObject wall = Instantiate(_mazeWall, pos, Quaternion.identity);
                    wall.transform.localScale = new Vector3(_scale, _scale, _scale);
                    wall.transform.parent = _mazeContainer.transform;
                }
                else if (x == (int)(_width / 2) && z == 0)
                {
                    // Entrance
                    GameObject maze_piece = Instantiate(_straightPiece, pos, Quaternion.identity);
                    maze_piece.transform.parent = _mazeContainer.transform;
                }
                else if (x == (int)(_width / 2) && z == 1)
                {
                    // Maze piece after entrance
                    if (_map[x, z + 1] == 1 && _map[x + 1, z] == 1)
                    {
                        // Top right corner piece
                        DrawCornerPiece(pos, "top_right");

                        // add to 'cornerPieces' list
                        //cornerPieces.Add(new MapCoordinate(x, z));
                    }
                    else if (_map[x, z + 1] == 1 && _map[x - 1, z] == 1)
                    {
                        // Top left corner piece
                        DrawCornerPiece(pos, "top_left");

                        // add to 'cornerPieces' list
                        //cornerPieces.Add(new MapCoordinate(x, z));
                    }
                    else if (_map[x, z + 1] == 1 && _map[x + 1, z] == 0 && _map[x - 1, z] == 0)
                    {
                        // Upright T
                        DrawTPiece(pos, "upright_t");

                        // add to 'tPieces' list
                        //tPieces.Add(new MapCoordinate(x, z));
                    }
                    else if (_map[x - 1, z] == 1 && _map[x, z + 1] == 0 && _map[x + 1, z] == 0)
                    {
                        // Right T
                        DrawTPiece(pos, "right_t");

                        // add to 'tPieces' list
                        //tPieces.Add(new MapCoordinate(x, z));
                    }
                    else if (_map[x + 1, z] == 1 && _map[x, z + 1] == 0 && _map[x - 1, z] == 0)
                    {
                        // Left T
                        DrawTPiece(pos, "left_t");

                        // add to 'tPieces' list
                        //tPieces.Add(new MapCoordinate(x, z));
                    }
                    else if (_map[x - 1, z] == 1 && _map[x + 1, z] == 1)
                    {
                        // Vertical
                        GameObject maze_piece = Instantiate(_straightPiece, pos, Quaternion.identity);
                        maze_piece.transform.parent = _mazeContainer.transform;

                        // add to 'straightPieces' list
                        //straightPieces.Add(new MapCoordinate(x, z));
                    }
                    else if (_map[x - 1, z] == 0 && _map[x, z + 1] == 0 && _map[x + 1, z] == 0)
                    {
                        // Crossroad
                        GameObject maze_piece = Instantiate(_crossRoadPiece, pos, Quaternion.identity);
                        maze_piece.transform.parent = _mazeContainer.transform;

                        // add to 'crossPieces' list
                        //crossPieces.Add(new MapCoordinate(x, z));
                    }
                }
                else if (Search2D(x, z, new int[] { 8, 0, 8, 1, 0, 1, 8, 0, 8 }))
                {
                    // Vertical
                    GameObject maze_piece = Instantiate(_straightPiece, pos, Quaternion.identity);
                    maze_piece.transform.parent = _mazeContainer.transform;

                    // add to 'straightPieces' list
                    straightPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 0, 0, 0, 8, 1, 8 }))
                {
                    // Horizontal
                    GameObject maze_piece = Instantiate(_straightPiece, pos, Quaternion.identity);
                    maze_piece.transform.parent = _mazeContainer.transform;
                    maze_piece.transform.Rotate(0, 90, 0);

                    // add to 'straightPieces' list
                    straightPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 1, 0, 1, 0, 0, 0, 1, 0, 1 }))
                {
                    // Crossroad
                    GameObject maze_piece = Instantiate(_crossRoadPiece, pos, Quaternion.identity);
                    maze_piece.transform.parent = _mazeContainer.transform;

                    // add to 'crossPieces' list
                    crossPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 1, 0, 1, 8, 0, 8 }))
                {
                    // Vertical up deadend
                    DrawDeadendPiece(pos, "up");

                    // add to 'deadendPieces' list
                    deadendPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 1, 0, 0, 8, 1, 8 }))
                {
                    // Horizontal left deadend
                    DrawDeadendPiece(pos, "left");

                    // add to 'deadendPieces' list
                    deadendPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 0, 8, 1, 0, 1, 8, 1, 8 }))
                {
                    // Vertical down deadend
                    DrawDeadendPiece(pos, "down");

                    // add to 'deadendPieces' list
                    deadendPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 0, 0, 1, 8, 1, 8 }))
                {
                    // Horizontal right deadend
                    DrawDeadendPiece(pos, "right");

                    // add to 'deadendPieces' list
                    deadendPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 0, 0, 1, 1, 0, 8 }))
                {
                    // Top right
                    DrawCornerPiece(pos, "top_right");

                    // add to 'cornerPieces' list
                    cornerPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 1, 0, 0, 8, 0, 1 }))
                {
                    // Top left
                    DrawCornerPiece(pos, "top_left");

                    // add to 'cornerPieces' list
                    cornerPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 0, 1, 1, 0, 0, 8, 1, 8 }))
                {
                    // Bottom left
                    DrawCornerPiece(pos, "bottom_left");

                    // add to 'cornerPieces' list
                    cornerPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 1, 0, 8, 0, 0, 1, 8, 1, 8 }))
                {
                    // Bottom right
                    DrawCornerPiece(pos, "bottom_right");

                    // add to 'cornerPieces' list
                    cornerPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 1, 0, 1, 0, 0, 0, 8, 1, 8 }))
                {
                    // Inverted T
                    DrawTPiece(pos, "inverted_t");

                    // add to 'tPieces' list
                    tPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 0, 1, 1, 0, 0, 8, 0, 1 }))
                {
                    // Right T
                    DrawTPiece(pos, "right_t");

                    // add to 'tPieces' list
                    tPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 0, 0, 0, 1, 0, 1 }))
                {
                    // Upright T
                    DrawTPiece(pos, "upright_t");

                    // add to 'tPieces' list
                    tPieces.Add(new MapCoordinate(x, z));
                }
                else if (Search2D(x, z, new int[] { 1, 0, 8, 0, 0, 1, 1, 0, 8 }))
                {
                    // Left T
                    DrawTPiece(pos, "left_t");

                    // add to 'tPieces' list
                    tPieces.Add(new MapCoordinate(x, z));
                }
            }
        }
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

    private void DrawCornerPiece(Vector3 pos, string orientation)
    {
        GameObject maze_piece = Instantiate(_cornerPiece, pos, Quaternion.identity);
        maze_piece.transform.parent = _mazeContainer.transform;

        if (orientation == "top_right")
        {
            maze_piece.transform.Rotate(0, 90, 0);
        }
        else if (orientation == "top_left")
        {
            maze_piece.transform.Rotate(0, 0, 0);
        }
        else if (orientation == "bottom_left")
        {
            maze_piece.transform.Rotate(0, -90, 0);
        }
        else if (orientation == "bottom_right")
        {
            maze_piece.transform.Rotate(0, 180, 0);
        }
    }

    private void DrawDeadendPiece(Vector3 pos, string orientation)
    {
        GameObject maze_piece = Instantiate(_deadendPiece, pos, Quaternion.identity);
        maze_piece.transform.parent = _mazeContainer.transform;

        if (orientation == "up")
        {
            maze_piece.transform.Rotate(0, 0, 0);
        }
        else if (orientation == "left")
        {
            maze_piece.transform.Rotate(0, -90, 0);
        }
        else if (orientation == "down")
        {
            maze_piece.transform.Rotate(0, 180, 0);
        }
        else if (orientation == "right")
        {
            maze_piece.transform.Rotate(0, 90, 0);
        }
    }

    private void DrawTPiece(Vector3 pos, string orientation)
    {
        GameObject maze_piece = Instantiate(_tPiece, pos, Quaternion.identity);
        maze_piece.transform.parent = _mazeContainer.transform;

        if (orientation == "inverted_t")
        {
            maze_piece.transform.Rotate(0, 180, 0);
        }
        else if (orientation == "right_t")
        {
            maze_piece.transform.Rotate(0, -90, 0);
        }
        else if (orientation == "upright_t")
        {
            maze_piece.transform.Rotate(0, 0, 0);
        }
        else if (orientation == "left_t")
        {
            maze_piece.transform.Rotate(0, 90, 0);
        }
    }

    // Draw utf-8 box symbols to represent map of maze
    private void OnGUI_UTF8()
    {
        string msg = "";

        Vector3 pos = _player.transform.position;
        int posInMazeX = (int)(_width / 2) + (int)(pos.x / _scale);
        int posInMazeZ = (int)(pos.z / _scale);

        //Debug.Log("Player X -> " + posInMazeX);
        //Debug.Log("Player Z -> " + posInMazeZ);

        for (int z = _depth - 1; z >= 0; z--)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_map[x, z] == 1)
                {
                    // wall
                    msg += "  ";
                }
                else if (x == (int)(_width / 2) && z == 0)
                {
                    // entrance
                    msg += "\u2551";
                }
                else if (pos.z >= 0 && x == posInMazeX && z == posInMazeZ)
                {
                    // player
                    //msg += "\u25CB";
                    msg += "\u25EF";
                }
                else if (x == (int)(_width / 2) && z == 1)
                {
                    // Maze piece after entrance
                    if (_map[x, z + 1] == 1 && _map[x + 1, z] == 1)
                    {
                        // Top right corner piece
                        msg += "\u2557";
                    }
                    else if (_map[x, z + 1] == 1 && _map[x - 1, z] == 1)
                    {
                        // Top left corner piece
                        msg += "\u2554";
                    }
                    else if (_map[x, z + 1] == 1 && _map[x + 1, z] == 0 && _map[x - 1, z] == 0)
                    {
                        // Upright T
                        msg += "\u2566";
                    }
                    else if (_map[x - 1, z] == 1 && _map[x, z + 1] == 0 && _map[x + 1, z] == 0)
                    {
                        // Right T
                        msg += "\u2560";
                    }
                    else if (_map[x + 1, z] == 1 && _map[x, z + 1] == 0 && _map[x - 1, z] == 0)
                    {
                        // Left T
                        msg += "\u2563";
                    }
                    else if (_map[x - 1, z] == 1 && _map[x + 1, z] == 1)
                    {
                        // Vertical
                        msg += "\u2551";
                    }
                    else if (_map[x - 1, z] == 0 && _map[x, z + 1] == 0 && _map[x + 1, z] == 0)
                    {
                        // Crossroad
                        msg += "\u256C";
                    }
                }
                else if (Search2D(x, z, new int[] { 8, 0, 8, 1, 0, 1, 8, 0, 8 }))
                {
                    // Vertical
                    msg += "\u2551";
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 0, 0, 0, 8, 1, 8 }))
                {
                    // Horizontal
                    msg += "\u2550";
                }
                else if (Search2D(x, z, new int[] { 1, 0, 1, 0, 0, 0, 1, 0, 1 }))
                {
                    // Crossroad
                    msg += "\u256C";
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 1, 0, 1, 8, 0, 8 }))
                {
                    // Vertical up deadend
                    //msg += "\u2553";
                    msg += "\u2551";
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 1, 0, 0, 8, 1, 8 }))
                {
                    // Horizontal left deadend
                    //msg += "\u2552";
                    msg += "\u2550";
                }
                else if (Search2D(x, z, new int[] { 8, 0, 8, 1, 0, 1, 8, 1, 8 }))
                {
                    // Vertical down deadend
                    //msg += "\u2559";
                    msg += "\u2551";
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 0, 0, 1, 8, 1, 8 }))
                {
                    // Horizontal right deadend
                    //msg += "\u255B";
                    msg += "\u2550";
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 0, 0, 1, 1, 0, 8 }))
                {
                    // Top right
                    msg += "\u2557";
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 1, 0, 0, 8, 0, 1 }))
                {
                    // Top left
                    msg += "\u2554";

                    // Top right
                    //msg += "\u2557";
                }
                else if (Search2D(x, z, new int[] { 8, 0, 1, 1, 0, 0, 8, 1, 8 }))
                {
                    // Bottom left
                    msg += "\u255A";
                }
                else if (Search2D(x, z, new int[] { 1, 0, 8, 0, 0, 1, 8, 1, 8 }))
                {
                    // Bottom right
                    msg += "\u255D";
                }
                else if (Search2D(x, z, new int[] { 1, 0, 1, 0, 0, 0, 8, 1, 8 }))
                {
                    // Inverted T
                    msg += "\u2569";
                }
                else if (Search2D(x, z, new int[] { 8, 0, 1, 1, 0, 0, 8, 0, 1 }))
                {
                    // Right T
                    msg += "\u2560";
                }
                else if (Search2D(x, z, new int[] { 8, 1, 8, 0, 0, 0, 1, 0, 1 }))
                {
                    // Upright T
                    msg += "\u2566";
                }
                else if (Search2D(x, z, new int[] { 1, 0, 8, 0, 0, 1, 1, 0, 8 }))
                {
                    // Left T
                    msg += "\u2563";
                }
                else
                {
                    msg += "?";
                }
            }

            msg += "\n";
        }

        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    // Draw ASCII characters to represent map of maze
    private void OnGUI_ASCII()
    {
        string msg = "";

        Vector3 pos = _player.transform.position;
        int posInMazeX = (int)(_width / 2) + (int)(pos.x / _scale);
        int posInMazeZ = (int)(pos.z / _scale);

        for (int z = _depth - 1; z >= 0; z--)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_map[x, z] == 1)
                {
                    // wall
                    msg += "||||";
                }
                else if (pos.z >= 0 && x == posInMazeX && z == posInMazeZ)
                {
                    msg += " o ";
                }
                else
                {
                    // Maze
                    msg += "   ";
                }
            }

            msg += "\n";
        }

        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    private void UpdateMap()
    {
        string msg = "";

        Vector3 pos = _player.transform.position;
        int posInMazeX = (int)(_width / 2) + (int)(pos.x / _scale);
        int posInMazeZ = (int)(pos.z / _scale);
        
        for (int z = _depth - 1; z >= 0; z--)
        {
            if (z == _depth - 1)
                msg = "<mspace=0.22em>";

            for (int x = 0; x < _width; x++)
            {
                if (_map[x, z] == 1)
                {
                    // wall
                    msg += "|||";
                }
                else if (pos.z >= 0 && x == posInMazeX && z == posInMazeZ)
                {
                    msg += " o ";
                }
                else
                {
                    // Maze
                    msg += "   ";
                }
            }

            if (z == 0)
                msg += "</mspace>";

            msg += "\n";
        }

        _uiMap.text = msg;
    }
}
