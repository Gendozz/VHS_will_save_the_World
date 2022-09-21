using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SwapZone : MonoBehaviour, IActivatable
{
    [Header("Объект игрока на сцене")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Объект клона на сцене")]
    [SerializeField] private PlayerInput _cloneInput;

    [Header("Главная камера")]
    [SerializeField] private CinemachineMainCamera _camera;

    [Header("Задержка для перемены местами игрока и клона")]
    [SerializeField] private float _cooldownDelay;

    [Header("Объект, показывающий здоровье игрока")]
    [SerializeField] private GameObject _playerHealth;

    [Header("Канвас, отображающий возможность нажатия специальной кнопки")]
    [SerializeField] private GameObject _specialButtonLabelCanvas;

    [Header("Объект с объёмом постпроцессинга, удаляемый после прохождения зоны клона")]
    [SerializeField] private GameObject _postProcessingVolumeToDelete;

    [Header("Задержка перед возвращением управления игроку после прохождения зоны")]
    [SerializeField] private float _delayBeforeDeactivation;

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
            _specialButtonLabelCanvas.SetActive(true);
        }
    }

    // isTrigger collider which denotes the zone where inverse ability can be activated
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _isAbleToActivateClone = false;
            _specialButtonLabelCanvas.SetActive(false);

        }
    }

    private void SwapPlayerClone()
    {
        _isPlayerActive = !_isPlayerActive;
        _playerInput.SwitchInput(_isPlayerActive);
        _cloneInput.SwitchInput(!_isPlayerActive);
        _camera.ChangeTarget(_isPlayerActive ? _playerInput.transform : _cloneInput.transform);
        _playerHealth.SetActive(!_playerHealth.activeSelf);
    }

    private IEnumerator RestoreAbility()
    {
        yield return new WaitForSeconds(_cooldownDelay);
        _isAbilityReady = true;
    }

    public void Activate()
    {
        _isAbleToActivateClone = false;
        Invoke(nameof(Deactivate), _delayBeforeDeactivation);
    }

    private void Deactivate()
    {
        SwapPlayerClone();
        _cloneInput.gameObject.SetActive(false);
        _postProcessingVolumeToDelete.SetActive(false);
        _specialButtonLabelCanvas.SetActive(false);
        gameObject.SetActive(false);
    }
}
