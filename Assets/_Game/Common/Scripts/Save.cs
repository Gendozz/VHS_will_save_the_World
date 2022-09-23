[System.Serializable]
public class Save
{
    private int totalTapes;
    private int levelsComplete;

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
}
