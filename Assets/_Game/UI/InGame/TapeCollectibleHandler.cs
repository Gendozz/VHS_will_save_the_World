using System;
using JSAM;
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

    private Sounds[] _tapesPieces = new Sounds[12]; 

    private int _currentTotalTapes;

    public int GotTapesAmountOnLevel { get; private set; } = 0;

    public static Action<int> onLevelEndCoinsCollected;

    private void Start()
    {
        _currentTotalTapes = GameFlowController.TotalTapesAmount;
        _tapesBar.fillAmount = (float)(GotTapesAmountOnLevel + _currentTotalTapes) / 12;

        _tapesPieces[0] = Sounds.TapePiece_01;
        _tapesPieces[1] = Sounds.TapePiece_02;
        _tapesPieces[2] = Sounds.TapePiece_03;
        _tapesPieces[3] = Sounds.TapePiece_04;
        _tapesPieces[4] = Sounds.TapePiece_05;
        _tapesPieces[5] = Sounds.TapePiece_06;
        _tapesPieces[6] = Sounds.TapePiece_07;
        _tapesPieces[7] = Sounds.TapePiece_08;
        _tapesPieces[8] = Sounds.TapePiece_09;
        _tapesPieces[9] = Sounds.TapePiece_10;
        _tapesPieces[10] = Sounds.TapePiece_11;
        _tapesPieces[11] = Sounds.TapePiece_12;

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

        AudioManager.PlaySound(_tapesPieces[_currentTotalTapes + GotTapesAmountOnLevel - 1]);
        Debug.Log($"Just played {_tapesPieces[_currentTotalTapes + GotTapesAmountOnLevel - 1]}");
    }

    private void TellHowMuchTapesCollectedOnLevelEnd()
    {
        onLevelEndCoinsCollected?.Invoke(GotTapesAmountOnLevel + _currentTotalTapes);
        Debug.Log("TapeCollectibleHandler says that, total tapes collected one level is " + GotTapesAmountOnLevel);
    }
}
