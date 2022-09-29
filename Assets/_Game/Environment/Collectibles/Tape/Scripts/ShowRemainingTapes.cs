using System;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(BoxCollider))]
public class ShowRemainingTapes : MonoBehaviour
{
    [SerializeField] private AnimationClip _flybyCamAnimation;

    private float _flybyCamDuration;

    public static Action<bool> onShowingRemainingTapes;

    private void Start()
    {
        _flybyCamDuration = _flybyCamAnimation.averageDuration;
        Debug.Log($"_flybyCamDuration duration is {_flybyCamDuration}");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                onShowingRemainingTapes?.Invoke(true);
                Invoke(nameof(StopShowingRemainingTapes), _flybyCamDuration);
            }        
        }
    }

    private void StopShowingRemainingTapes()
    {
        onShowingRemainingTapes?.Invoke(false);
    }    
}
