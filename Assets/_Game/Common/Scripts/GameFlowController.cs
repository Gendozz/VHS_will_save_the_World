using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    private int _totalTapesCollected;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(StringConsts.TOTAL_TAPES_AMOUNT))
        {
            _totalTapesCollected = PlayerPrefs.GetInt(StringConsts.MUSIC_VOLUME);
        }

        DontDestroyOnLoad(gameObject);

        Debug.Log("Current total tapes: " + _totalTapesCollected);
    }

    private void OnEnable()
    {
        TapeCollectibleHandler.onLevelEndCoinsCollected += AddTape;
    }

    private void OnDisable()
    {
        TapeCollectibleHandler.onLevelEndCoinsCollected -= AddTape;
    }
    
    private void AddTape(int tapesToAdd)
    {
        _totalTapesCollected += tapesToAdd;
        PlayerPrefs.SetInt(StringConsts.TOTAL_TAPES_AMOUNT, _totalTapesCollected);
    }
}
