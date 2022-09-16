using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Count collected coins and show its amount
/// </summary>
public class TapeCollectibleHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _tapesText;

    [SerializeField] private string _textBeforeTapes;

    [SerializeField] private Image _tapesBar;

    private int _currentTotalTapes;

    public int GotTapesAmountOnLevel { get; private set; } = 0;

    public static Action<int> onLevelEndCoinsCollected;

    private void Start()
    {
        if (PlayerPrefs.HasKey(StringConsts.TOTAL_TAPES_AMOUNT))
        {
            _currentTotalTapes = PlayerPrefs.GetInt(StringConsts.TOTAL_TAPES_AMOUNT);
        }
        _tapesBar.fillAmount = (float)(GotTapesAmountOnLevel + _currentTotalTapes) / 12;

        //Debug.Log("Current total tapes on Start: " + _totalTapesCollected);
    }

    private void OnEnable()
    {
        TapeCollectible.onTapeCollected += DoAfterTapeCollectedThings;
        LevelEnd.onPlayerGotToLevelEnd += TellHowMuchTapesCollectedOnLevelEnd;
    }

    private void OnDisable()
    {
        TapeCollectible.onTapeCollected -= DoAfterTapeCollectedThings;
        LevelEnd.onPlayerGotToLevelEnd -= TellHowMuchTapesCollectedOnLevelEnd;
    }

    private void DoAfterTapeCollectedThings()
    {
        GotTapesAmountOnLevel += 1;

        _tapesBar.fillAmount = (float) (GotTapesAmountOnLevel + _currentTotalTapes) / 12;
        _tapesText.text = _textBeforeTapes + " " + GotTapesAmountOnLevel + _currentTotalTapes;
    }

    private void TellHowMuchTapesCollectedOnLevelEnd()
    {
        onLevelEndCoinsCollected?.Invoke(GotTapesAmountOnLevel + _currentTotalTapes);
        Debug.Log("TapeCollectibleHandler says that, total tapes collected one level is " + GotTapesAmountOnLevel);
    }
}
