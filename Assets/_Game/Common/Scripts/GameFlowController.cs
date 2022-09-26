using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour
{
    public static int TotalTapesAmount { get; private set; } = 0;

    public static int LevelsComplete { get; private set; } = 0;

    private string savePath = "/gamesave.save";

    public static Action onProgessLoaded;

    private int _sceneBuildNumberToResetProgress = 2;

    private void OnEnable()
    {
        TapeCollectibleHandler.onLevelEndCoinsCollected += SaveGame;
        LevelEnd.onPlayerGotToLevelEnd += DoPlayerWinActions;
    }

    private void OnDisable()
    {
        TapeCollectibleHandler.onLevelEndCoinsCollected -= SaveGame;
        LevelEnd.onPlayerGotToLevelEnd -= DoPlayerWinActions;

    }

    private void Awake()
    {
        LoadProgress();
    }

    private void Start()
    {
        onProgessLoaded?.Invoke();

        Debug.Log("onProgessLoaded fired");
    }
    private void DoPlayerWinActions()
    {
        //_isGamePaused = true;
        //_menuController.ShowWinCanvas();
        //SwitchMotions(!_isGamePaused);
        Invoke(nameof(LoadNextLevel), 1f);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private Save CreateSaveObject(int tapesCollectedAmount, int levelCompleteAmount)
    {

        Save save = new Save();

        save.TotalTapes = tapesCollectedAmount;
        save.LevelsComplete = levelCompleteAmount;

        return save;
    }


    public void SaveGame(int tapesCollectedAmount)
    {
        int sceneNumberToSave = SceneManager.GetActiveScene().buildIndex + 1;
        int tapesNumberToSave = tapesCollectedAmount;

        Save save = CreateSaveObject(tapesNumberToSave, sceneNumberToSave);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + savePath);

        bf.Serialize(file, save);

        file.Close();

        Debug.Log("Game Saved");
    }


    public void LoadProgress()
    {
        if (File.Exists(Application.persistentDataPath + savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + savePath, FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            if (SceneManager.GetActiveScene().buildIndex == 0 && save.LevelsComplete > _sceneBuildNumberToResetProgress)
            {
                ResetProgress();
            }
            else
            {
                TotalTapesAmount = save.TotalTapes;
                LevelsComplete = save.LevelsComplete;
            }
            Debug.Log($"Progress Loaded. Total taped collected at start {TotalTapesAmount}. Current build index saved {LevelsComplete}");
        }
    }

    public void ResetProgress()
    {
        TotalTapesAmount = 0;
        LevelsComplete = 0;
        SaveGame(TotalTapesAmount);
    }




    // FOR DEBUG

    private void OnGUI()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (GUI.Button(new Rect(10, 10, 150, 50), "Clear Progress"))
                ResetProgress();
        }
    }


    //private void OnLevelWasLoaded(int level)
    //{
    //    //if (PlayerPrefs.HasKey(StringConsts.TOTAL_TAPES_AMOUNT))
    //    //{
    //    //    _totalTapesCollected = PlayerPrefs.GetInt(StringConsts.TOTAL_TAPES_AMOUNT);
    //    //}

    //    //Debug.Log("On Level was Loaded GameFlowController"); 
    //    Debug.Log("Current total tapes: " + _totalTapesCollected);


    //}
}
