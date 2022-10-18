using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    public static int TotalTapesAmount { get; private set; }

    public static int LevelsComplete { get; private set; }

    private static string savePath = "/gamesave.save";

    private static Save CreateSaveObject(int tapesCollectedAmount, int levelCompleteAmount) 
    {
        //PlayerPrefs.SetInt(StringConsts.TOTAL_TAPES_AMOUNT, tapesAmount);
        //TotalTapesAmount += tapesAmount;
        //PlayerPrefs.SetInt(StringConsts.LEVELS_COMPLETE, currentLevelComplete);
        //LevelsComplete = currentLevelComplete;

        Save save = new Save();

        save.TotalTapes = TotalTapesAmount + tapesCollectedAmount;
        save.LevelsComplete = LevelsComplete + levelCompleteAmount;

        return save;
    }


    public static void SaveGame(int tapesCollectedAmount, int levelCompleteAmount)
    {
        Save save = CreateSaveObject(tapesCollectedAmount, levelCompleteAmount);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + savePath);

        bf.Serialize(file, save);

        file.Close();

        //Debug.Log("Game Saved");

    }


    public static void LoadProgress()
    {
        //if (PlayerPrefs.HasKey(StringConsts.TOTAL_TAPES_AMOUNT))
        //{
        //    TotalTapesAmount = PlayerPrefs.GetInt(StringConsts.TOTAL_TAPES_AMOUNT);
        //}

        //if (PlayerPrefs.HasKey(StringConsts.LEVELS_COMPLETE))
        //{
        //    LevelsComplete = PlayerPrefs.GetInt(StringConsts.LEVELS_COMPLETE);
        //}

        if(File.Exists(Application.persistentDataPath + savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + savePath, FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            TotalTapesAmount = save.TotalTapes;
            LevelsComplete = save.LevelsComplete;
            //Debug.Log("Progress Loaded");
        }
    }

    public static void ResetProgress()
    {
        TotalTapesAmount = 0;
        LevelsComplete = 0;
        SaveGame(TotalTapesAmount, LevelsComplete);
    }
}
