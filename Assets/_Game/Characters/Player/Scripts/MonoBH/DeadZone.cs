using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DeadZone : MonoBehaviour
{   
    [Header("����� ��������������")]
    [SerializeField] private Transform _pointOfRestore;

    [Header("�������� ����� ������������ � ����� ��������������")]
    [SerializeField] private float _delayBeforeMoveToPoint;

    [Header("������ �� Health ������")]
    [SerializeField] private Health _playerHealth;

    [Header("������ �� PlayerMovement")]
    [SerializeField] private PlayerMovement _playerMovement;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _playerHealth.onTakeDamage += PullOutFromDeadZoneDelayed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _playerHealth.onTakeDamage -= PullOutFromDeadZoneDelayed;
        }
    }

    private void OnDisable()
    {
        _playerHealth.onTakeDamage -= PullOutFromDeadZoneDelayed;
    }

    private void PullOutFromDeadZoneDelayed()
    {
        Invoke(nameof(PullOutFromDeadZone), _delayBeforeMoveToPoint);
    }

    private void PullOutFromDeadZone()
    {
        _playerMovement.MoveToCoords(_pointOfRestore.position);
    }


}
