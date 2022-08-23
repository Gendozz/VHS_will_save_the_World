using UnityEngine;

// ��������� �������
// TODO: ���������� ��� ����� ����
public class CameraFollowPlayer : MonoBehaviour
{
    [Header("�� ��� �������� ������")]
    [SerializeField] private Transform target;

    [Header("����� �� ��������� ������������ ����")]
    [SerializeField] private float yOffset;
    
    [Header("��������� �� ���� ��")]
    [SerializeField] private float zOffset;

    [Header("�������� ��������")]
    [SerializeField] private float followSpeed;

    private Vector3 _currentTargetPosition;

    void Update()
    {
        _currentTargetPosition = target.transform.position;

        _currentTargetPosition.z += zOffset;

        _currentTargetPosition.y += yOffset;

        transform.position = Vector3.Lerp(transform.position, _currentTargetPosition, Time.deltaTime * followSpeed);
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
