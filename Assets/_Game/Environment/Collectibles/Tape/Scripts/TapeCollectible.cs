using System;
using UnityEngine;

public class TapeCollectible : MonoBehaviour
{
    [SerializeField] private LerpLight[] _lerpLights;

    public static Action onTapeCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            SwitchLights();
            SayTapeCollected();
            gameObject.SetActive(false);
        }
    }

    private void SwitchLights()
    {
        for (int i = 0; i < _lerpLights.Length; i++)
        {
            _lerpLights[i].gameObject.SetActive(true);
            _lerpLights[i].StartLerp();
        }
    }

    private void SayTapeCollected()
    {
        Debug.Log("Tape collected");
        onTapeCollected?.Invoke();
    }
}
