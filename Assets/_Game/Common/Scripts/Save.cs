[System.Serializable]
public class Save
{
    private int totalTapes;
    private int levelsComplete;
    private int lives;

    public int TotalTapes
    {
        get
        {
            return totalTapes;
        }
        set
        {
            totalTapes = value;
        }
    }

    public int LevelsComplete
    {
        get
        {
            return levelsComplete;
        }
        set
        {
            levelsComplete = value;
        }
    }

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
        }
    }
}
