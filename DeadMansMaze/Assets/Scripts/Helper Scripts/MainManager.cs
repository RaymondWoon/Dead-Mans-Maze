using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    // values to use in the game
    public int MazeWidth;
    public int MazeDepth;
    public int MazeScale;

    // constrains
    public int MinMazeWidth;
    public int MaxMazeWidth;
    public int MinMazeDepth;
    public int MaxMazeDepth;

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
        NewMazeWidthSelected(MazeWidth);
        NewMazeDepthSelected(MazeDepth);
    }

    public void NewMazeWidthSelected(int width)
    {
        // Update the MazeWidth
        Instance.MazeWidth = width;
    }

    public void NewMazeDepthSelected(int depth)
    {
        // Update the MazeDepth
        Instance.MazeDepth = depth;
    }

}
