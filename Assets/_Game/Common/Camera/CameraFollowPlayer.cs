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

    [SerializeField] private float minX;

    [SerializeField] private float maxX;

    [SerializeField] private float minY;

    [SerializeField] private float maxY;

    private Vector3 _currentTargetPosition;

    void Update()
    {
        _currentTargetPosition = target.transform.position;

        _currentTargetPosition.z += zOffset;

        _currentTargetPosition.y += yOffset;

        Vector3 clampedPosition = new Vector3(Mathf.Clamp(_currentTargetPosition.x, minX, maxX),
                                              Mathf.Clamp(_currentTargetPosition.y, minY, maxY), 
                                              transform.position.z);

        transform.position = Vector3.Lerp(transform.position, clampedPosition, Time.deltaTime * followSpeed);        
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
