using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelEnd : MonoBehaviour
{
    public static Action onPlyerGotToLevelEnd;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            onPlyerGotToLevelEnd?.Invoke();
        }
    }
}
