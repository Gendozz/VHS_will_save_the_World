using System.Collections;
using UnityEngine;

public class PlayerModelRotation : MonoBehaviour
{
    [Header("Продолжительность разворота")]
    [SerializeField] private float _rotationDuration;

    [Header("Скорость по Х, когда разворот происходит без инпута")]
    [SerializeField] private float _minVelocityToStartRotate;

    [Space]
    [Header("-----     Компоненты     -----")]
    [Header("Ссылка на пользовательский ввод")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Физическое тело объекта")]
    [SerializeField ]private Rigidbody _rigidbody;

    private Vector3 _rightRotation = new Vector3(0, 0.1f, 0);

    private Vector3 _leftRotation = new Vector3(0, 179.9f, 0);

    private float _currentDirection = 1; 

    private Coroutine _rotationRoutine;

    void Update()
    {
        ApplyChangingDirection();
    }

    private void ApplyChangingDirection()
    {
        if (Mathf.Sign(_rigidbody.velocity.x) != Mathf.Sign(_currentDirection))
        {
            if ((_playerInput.isHorizontalInput && _rigidbody.velocity.x > 0.05f) || Mathf.Abs(_rigidbody.velocity.x) > _minVelocityToStartRotate)
            {
                _currentDirection *= -1;
                if (_rotationRoutine != null)
                {
                    StopCoroutine(_rotationRoutine);
                }
                _rotationRoutine = StartCoroutine(RotateModel(_currentDirection)); 
            }
        }
    }

    private IEnumerator RotateModel(float direction)
    {
        float elapsedTime = 0;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = direction > 0 ? Quaternion.Euler(_rightRotation) : Quaternion.Euler(_leftRotation);

        while (elapsedTime < _rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            _rigidbody.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / _rotationDuration);
            yield return null;
        }
    }
}
