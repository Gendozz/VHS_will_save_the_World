using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy1Moving : MonoBehaviour
{
    [HideInInspector] public bool IsSees;

    [Header("-----      Настройки      -----")]
    [SerializeField] private Transform _leftPointTransform;
    [SerializeField] private Transform _rightPointTransform;
    [SerializeField] private float _speedPosition;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float angleWhichEnemyChangesDirection;
    [SerializeField] private float _additionalVisibilityHeight;
    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _minDistanseBetweenPlayerAndEnemy;

    private Vector3 _directionMoving = Vector3.left;
    private float _leftPointTransformPositionX;
    private float _rightPointTransformPositoinX;
    private float _lowerLimitFieldView;
    private float _upperLimitFieldView;
    private float _angleError = 0.1f;
    private bool _onScreen;
    private bool _playerInZone;

    private void Start()
    {
        _rigidbody.useGravity = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _leftPointTransformPositionX = _leftPointTransform.position.x;
        _rightPointTransformPositoinX = _rightPointTransform.position.x;

        _lowerLimitFieldView = transform.position.y - transform.localScale.y;
        _upperLimitFieldView = transform.position.y + transform.localScale.y + _additionalVisibilityHeight;
    }

    private void Update()
    {
        PlayerInZone();

        if (_onScreen && _playerInZone)
        {
            ChangeDirection(_playerTransform.position.x, _playerTransform.position.x);
            GetStay();

            if (_rigidbody.position.x < _playerTransform.position.x && (transform.eulerAngles.y < 90 - angleWhichEnemyChangesDirection) ||
            _rigidbody.position.x > _playerTransform.position.x && (transform.eulerAngles.y > 90 + angleWhichEnemyChangesDirection))
            {
                IsSees = true;
            }
        }
        else
        {
            IsSees = false;
            KeepMoving();
            ChangeDirection(_leftPointTransformPositionX, _rightPointTransformPositoinX);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _directionMoving * _speedPosition;
    }

    private void PlayerInZone()
    {
        if (_playerTransform.position.y - _playerTransform.localScale.y < _upperLimitFieldView &&
            _playerTransform.position.y + _playerTransform.localScale.y > _lowerLimitFieldView)
        {
            _playerInZone = true;
        }
        else
        {
            _playerInZone = false;
        }
    }

    private void GetStay()
    {
        float _distToPlayer = _rigidbody.position.x - _playerTransform.position.x;

        if (_rigidbody.position.x <= _leftPointTransformPositionX && (transform.eulerAngles.y > 180 - angleWhichEnemyChangesDirection) ||
            _rigidbody.position.x >= _rightPointTransformPositoinX && (transform.eulerAngles.y < angleWhichEnemyChangesDirection) ||
            _distToPlayer * _distToPlayer < _minDistanseBetweenPlayerAndEnemy * _minDistanseBetweenPlayerAndEnemy)
        {
            _directionMoving = Vector3.zero;
        }
    }

    private void KeepMoving()
    {
        if (_directionMoving == Vector3.zero)
        {
            if (transform.eulerAngles.y > angleWhichEnemyChangesDirection)
            {
                _directionMoving = Vector3.left;
            }
            else
            {
                _directionMoving = Vector3.right;
            }
        }
    }

    private void ChangeDirection(float leftPositon, float rightPosition)
    {
        if (leftPositon > _rigidbody.position.x)
        {
            _directionMoving = Vector3.zero;
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, _angleError, 0), _speedRotation);
            if (transform.eulerAngles.y < angleWhichEnemyChangesDirection)
            {
                _directionMoving = Vector3.right;
            }
        }
        else if (rightPosition < _rigidbody.position.x)
        {
            _directionMoving = Vector3.zero;
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, 180 - _angleError, 0), _speedRotation);
            if (transform.eulerAngles.y > 180 - angleWhichEnemyChangesDirection)
            {
                _directionMoving = Vector3.left;
            }
        }

        if (_directionMoving == Vector3.right)
        {
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, _angleError, 0), _speedRotation);
        }
        else if (_directionMoving == Vector3.left)
        {
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, 180 - _angleError, 0), _speedRotation);
        }
    }

    private void OnBecameVisible()
    {
        _onScreen = true;
    }

    private void OnBecameInvisible()
    {
        _onScreen = false;
    }
}
