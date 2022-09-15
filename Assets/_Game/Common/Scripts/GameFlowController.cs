using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour
{
    private int _totalTapesCollected;

    private int _maxLevelNumber = 2;

    private void Start()
    {
        if (PlayerPrefs.HasKey(StringConsts.TOTAL_TAPES_AMOUNT))
        {
            _totalTapesCollected = PlayerPrefs.GetInt(StringConsts.TOTAL_TAPES_AMOUNT);
        }

        DontDestroyOnLoad(gameObject);

        //Debug.Log("Current total tapes on Start: " + _totalTapesCollected);
    }

    private void OnEnable()
    {
        TapeCollectibleHandler.onLevelEndCoinsCollected += AddTapes;
        LevelEnd.onPlayerGotToLevelEnd += SetCurrentLevelComplete;
    }

    private void OnDisable()
    {
        TapeCollectibleHandler.onLevelEndCoinsCollected -= AddTapes;
        LevelEnd.onPlayerGotToLevelEnd -= SetCurrentLevelComplete;
    }
    
    private void SetCurrentLevelComplete()
    {
        // врн-рн гдеяэ

        PlayerPrefs.SetInt(StringConsts.LEVELS_COMPLETE, _maxLevelNumber == SceneManager.GetActiveScene().buildIndex ? 0 : SceneManager.GetActiveScene().buildIndex);
    }

    private void AddTapes(int tapesToAdd)
    {
        _totalTapesCollected += tapesToAdd;
        PlayerPrefs.SetInt(StringConsts.TOTAL_TAPES_AMOUNT, _totalTapesCollected);
        //Debug.Log("Total score in playerprefs " + PlayerPrefs.GetInt(StringConsts.TOTAL_TAPES_AMOUNT));
    }

    // FOR DEBUG
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
