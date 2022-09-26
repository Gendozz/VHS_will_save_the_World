using UnityEngine;

public class SwitchBackground : MonoBehaviour, IActivatable
{
    [SerializeField] private LerpAlpha[] _lerpAlphas;

    public void Activate()
    {
        foreach (var lerpAlphas in _lerpAlphas)
        {
            lerpAlphas.StartChangingAlpha();
        }
    }
}
