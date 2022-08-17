using UnityEngine;

// ��������� �������
// TODO: ���������� ��� ����� ����
public class CameraFollowPlayer : MonoBehaviour
{
    [Header("�� ��� �������� ������")]
    [SerializeField] private Transform target;

    [Header("����� �� ��������� ������������ ����")]
    [SerializeField] private float yOffset;

    private Vector3 _currentTargetPosition;

    void Update()
    {
        _currentTargetPosition = target.transform.position;

        _currentTargetPosition.z = -10f;

        _currentTargetPosition.y += yOffset;

        transform.position = Vector3.Lerp(transform.position, _currentTargetPosition, Time.deltaTime);
    }
}
