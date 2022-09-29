using UnityEngine;

public class HighlightTapeOnShowingRemainingTapes : MonoBehaviour
{
    [SerializeField] private VLight _tapeHighlight;

    [SerializeField] private float _lightMultiplierOnHighliting;

    private void OnEnable()
    {
        ShowRemainingTapes.onShowingRemainingTapes += SwitchHighlight;
    }

    private void OnDisable()
    {
        ShowRemainingTapes.onShowingRemainingTapes -= SwitchHighlight;
    }

    private void SwitchHighlight(bool needHighLight)
    {
        if (needHighLight)
        {
            _tapeHighlight.lightMultiplier = _lightMultiplierOnHighliting;
        }
        else
        {
            _tapeHighlight.lightMultiplier = 0;
        }
    }
}
