using UnityEngine;

public class TapeCollectionInfluencer : MonoBehaviour
{
    [SerializeField] private LerpLight[] _lerpLights;

    public void SwitchLights()
    {
        for (int i = 0; i < _lerpLights.Length; i++)
        {
            _lerpLights[i].gameObject.SetActive(true);
            _lerpLights[i].StartLerp();
        }
    }
}
