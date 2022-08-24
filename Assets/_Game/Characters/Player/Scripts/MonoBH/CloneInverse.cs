using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CloneInverse : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _clone;

    [SerializeField] private CameraFollowPlayer _camera;

    [SerializeField] private float _cooldownDelay;

    private bool _isAbleToActivateClone = false;

    // For cooldown
    private bool _isAbilityReady = true;

    void Start()
    {
        _clone.SetActive(false);    // TODO: Maybe change after
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isAbleToActivateClone && _isAbilityReady)
        {
            SwapPlayerClone(); // TODO: Temp. Make public method in Player to visualise disactivness
            _isAbilityReady = false;
            StartCoroutine(RestoreAbility());
        }
    }
    
    // isTrigger collider which denotes the zone where inverse ability can be activated
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _isAbleToActivateClone = true;
        }
    }

    // isTrigger collider which denotes the zone where inverse ability can be activated
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _isAbleToActivateClone = false;
        }
    }

    // Hard Collider represented a lever to unblock everything
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            SwapPlayerClone();
            Invoke(nameof(UnblockEverythingInZone), 0.1f);
        }
    }

    private void UnblockEverythingInZone()
    {
        print("Everything is unblocked");

        gameObject.SetActive(false);
    }

    private void SwapPlayerClone()
    {
        _player.SetActive(!_player.activeSelf);
        _clone.SetActive(!_clone.activeSelf);
        _camera.ChangeTarget(_player.activeSelf ? _player.transform : _clone.transform);
    }

    private IEnumerator RestoreAbility()
    {
        yield return new WaitForSeconds(_cooldownDelay);
        _isAbilityReady = true;
    }


}
