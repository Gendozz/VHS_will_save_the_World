using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Count collected coins and show its amount
/// </summary>
public class TapeCollectibleHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _tapesText;

    [SerializeField] private string _textBeforeTapes;

    public int GotTapesAmountOnLevel { get; private set; } = 0;

    public static Action<int> onLevelEndCoinsCollected;

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
        _tapesText.text = _textBeforeTapes + " " + GotTapesAmountOnLevel;
    }

    private void TellHowMuchTapesCollectedOnLevelEnd()
    {
        onLevelEndCoinsCollected?.Invoke(GotTapesAmountOnLevel);
        Debug.Log("TapeCollectibleHandler says that, total tapes collected one level is " + GotTapesAmountOnLevel);
    }
}
