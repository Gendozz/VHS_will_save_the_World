using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelEnd : MonoBehaviour
{
    public static Action onPlayerGotToLevelEnd;

    private bool _wasOneTriggerWithPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement) && !_wasOneTriggerWithPlayer)
        {
            onPlayerGotToLevelEnd?.Invoke();
            _wasOneTriggerWithPlayer = true;
        }
    }
}
