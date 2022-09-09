using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SwapZone : MonoBehaviour, IActivatable
{
    [Header("Объект игрока на сцене")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Объект клона на сцене")]
    [SerializeField] private PlayerInput _cloneInput;

    [Header("Главная камера")]
    [SerializeField] private CameraFollowPlayer _camera;

    [Header("Задержка для перемены местами игрока и клона")]
    [SerializeField] private float _cooldownDelay;

    private bool _isAbleToActivateClone = false;

    // For cooldown
    private bool _isAbilityReady = true;

    private bool _isPlayerActive = true;

    void Start()
    {
        _cloneInput.SwitchInput(false);    
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
        _isPlayerActive = !_isPlayerActive;
        _playerInput.SwitchInput(_isPlayerActive);
        _cloneInput.SwitchInput(!_isPlayerActive);
        _camera.ChangeTarget(_isPlayerActive ? _playerInput.transform : _cloneInput.transform);
    }

    private IEnumerator RestoreAbility()
    {
        yield return new WaitForSeconds(_cooldownDelay);
        _isAbilityReady = true;
    }

    public void Activate()
    {
        SwapPlayerClone();
        _cloneInput.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
