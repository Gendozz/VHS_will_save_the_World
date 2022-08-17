using UnityEngine;

// Временное решение
// TODO: Переделать под нужды игры
public class CameraFollowPlayer : MonoBehaviour
{
    [Header("За кем движется камера")]
    [SerializeField] private Transform target;

    [Header("Офсет по вертикали относительно цели")]
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
