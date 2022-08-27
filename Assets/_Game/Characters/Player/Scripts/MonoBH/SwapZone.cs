using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SwapZone : MonoBehaviour, IActivatable
{
    [Header("Объект игрока на сцене")]
    [SerializeField] private GameObject _player;

    [Header("Объект клона на сцене")]
    [SerializeField] private GameObject _clone;

    [Header("Главная камера")]
    [SerializeField] private CameraFollowPlayer _camera;

    [Header("Задержка для перемены местами игрока и клона")]
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

    public void Activate()
    {
        SwapPlayerClone();
        gameObject.SetActive(false);
    }
}
