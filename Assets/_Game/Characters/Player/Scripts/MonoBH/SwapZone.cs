using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SwapZone : MonoBehaviour, IActivatable
{
    [Header("������ ������ �� �����")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("������ ����� �� �����")]
    [SerializeField] private PlayerInput _cloneInput;

    [Header("������� ������")]
    [SerializeField] private CinemachineMainCamera _camera;

    [Header("�������� ��� �������� ������� ������ � �����")]
    [SerializeField] private float _cooldownDelay;

    [Header("������, ������������ �������� ������")]
    [SerializeField] private TMP_Text _playerHealth;

    [Header("������, ������������ ����������� ������� ����������� ������")]
    [SerializeField] private GameObject _specialButtonLabelCanvas;

    [Header("������ � ������� ���������������, ��������� ����� ����������� ���� �����")]
    [SerializeField] private GameObject _postProcessingVolumeToDelete;

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
        _playerHealth.gameObject.SetActive(!_playerHealth.gameObject.activeSelf);
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
        _postProcessingVolumeToDelete.SetActive(false);
        _specialButtonLabelCanvas.SetActive(false);
        gameObject.SetActive(false);
    }
}
