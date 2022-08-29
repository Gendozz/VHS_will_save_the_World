using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [Header("-----      Настройки      -----")]
    [Header("Скорость разворота в градус в сек")]
    [SerializeField] private float _speedRotation;

    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [Header("Трансформ игрока")]
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private bool _isSees = true;                           // для теста

    private void Update()
    {
        if (_playerTransform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -89.9f, 0), _speedRotation);
        }
        if (_playerTransform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 89.9f, 0), _speedRotation);
        }
    }

    /*private void OnBecameVisible()
    {
        _isSees = true;
    }

    private void OnBecameInvisible()
    {
        _isSees = false;
    }*/
}
