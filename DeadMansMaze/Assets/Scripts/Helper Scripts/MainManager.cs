using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    // values to use in the game
    public int MazeWidth;
    public int MazeDepth;
    public int MazeScale;
    public int NumberOfEnemies;
    public bool MazeKeyFound;
    public bool AthelasFound;
    public int TimeToComplete;
    public float MouseSensitivity;

    // constrains
    public int MinMazeWidth;
    public int MaxMazeWidth;
    public int MinMazeDepth;
    public int MaxMazeDepth;
    public int MinNumEnemies;
    public int MaxNumEnemies;
    public int MinTimeToComplete;
    public int MaxTimeToComplete;


    private void Awake()
    {
        // destroy any existing MainManager
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitMazeWidth(MazeWidth);
        InitMazeDepth(MazeDepth);
        InitNumOfEnemies(NumberOfEnemies);
        InitTimeToComplete(TimeToComplete);
        InitMouseSensitivity(MouseSensitivity);
    }

    public void InitMazeWidth(int width)
    {
        // Update the MazeWidth
        Instance.MazeWidth = width;
    }

    public void InitMazeDepth(int depth)
    {
        // Update the MazeDepth
        Instance.MazeDepth = depth;
    }

    public void InitNumOfEnemies(int num)
    {
        // Update the number of enemies
        Instance.NumberOfEnemies = num;
    }

    public void InitTimeToComplete(int num)
    {
        // Update the time variable
        Instance.TimeToComplete = num;
    }

    public void InitMouseSensitivity(float num)
    {
        // update the mouse sensitivity variable
        Instance.MouseSensitivity = num;
    }

}
