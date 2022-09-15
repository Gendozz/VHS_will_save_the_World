using System;
using UnityEngine;
using JSAM;

public class TapeCollectible : MonoBehaviour
{
    [SerializeField] private LerpLight[] _lerpLights;

    [SerializeField] private Sounds _tapePiece;
    
    public static Action onTapeCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            AudioManager.PlaySound(_tapePiece);
            SwitchLights();
            SayTapeCollected();
            gameObject.SetActive(false);
        }
    }

    private void SwitchLights()
    {
        if (_lerpLights.Length > 0)
        {
            for (int i = 0; i < _lerpLights.Length; i++)
            {
                _lerpLights[i].gameObject.SetActive(true);
                _lerpLights[i].StartLerp();
            } 
        }
    }

    private void SayTapeCollected()
    {
        Debug.Log("Tape collected");
        onTapeCollected?.Invoke();
    }
}
