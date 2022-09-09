using System;
using UnityEngine;

public class TapeCollectible : MonoBehaviour
{
    public static Action onTapeCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            SayTapeCollected();
            gameObject.SetActive(false);
        }
    }

    private void SayTapeCollected()
    {
        Debug.Log("Tape collected");
        onTapeCollected?.Invoke();
    }
}
