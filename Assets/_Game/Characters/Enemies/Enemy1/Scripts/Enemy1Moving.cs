using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Enemy1Moving : MonoBehaviour
{
    [HideInInspector] public bool IsSees;

    [Header("-----      Настройки      -----")]
    [SerializeField] private Transform _leftPointTransform;
    [SerializeField] private Transform _rightPointTransform;
    [SerializeField] private float _speedPosition;
    [SerializeField] private float _timeIdleRotation;
    [SerializeField] private float _timeAgroRotation;
    [SerializeField] private float _additionalVisibilityHeight;
    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _minDistanseBetweenPlayerAndEnemy;
    [SerializeField] private Transform _firePoint;

    private Coroutine _changingFirePoint;
    private Vector3 _directionMoving;
    private float _leftPointTransformPositionX;
    private float _rightPointTransformPositoinX;
    private float _lowerLimitFieldView;
    private float _upperLimitFieldView;
    private bool _onScreen;
    private bool _playerInZone;
    private bool _isFirePointchanging;

    private void Start()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

        _directionMoving = Vector3.left;

        _leftPointTransformPositionX = _leftPointTransform.position.x;
        _rightPointTransformPositoinX = _rightPointTransform.position.x;

        _lowerLimitFieldView = transform.position.y - transform.localScale.y / 2;
        _upperLimitFieldView = transform.position.y + transform.localScale.y / 2 + _additionalVisibilityHeight;

        _isFirePointchanging = true;
    }

    private void Update()
    {
        if (_isFirePointchanging)
        {
            GetInfAboutPlayerInZone();

            if (_onScreen && _playerInZone)
            {
                FollowPlayer();
            }
            else
            {
                SetMoveIdle();
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _directionMoving * _speedPosition;
    }

    private void GetInfAboutPlayerInZone()
    {
        if (_playerTransform.position.y < _upperLimitFieldView &&
            _playerTransform.position.y > _lowerLimitFieldView)
        {
            _playerInZone = true;
        }
        else
        {
            _playerInZone = false;
        }
    }

    private void FollowPlayer()
    {
        if (_playerTransform.position.x > transform.position.x)
        {
            if (_firePoint.localPosition.z < 0)
            {
                IsSees = true;

                float _distToPlayer = _rigidbody.position.x - _playerTransform.position.x;
                if (_distToPlayer * _distToPlayer < _minDistanseBetweenPlayerAndEnemy * _minDistanseBetweenPlayerAndEnemy ||
                    transform.position.x < _leftPointTransformPositionX || transform.position.x > _rightPointTransformPositoinX)
                {
                    _directionMoving = Vector3.zero;
                    _animator.SetBool("Walk", false);
                }
                else
                {
                    _directionMoving = Vector3.right;
                    _animator.SetBool("Walk", true);
                }
            }
            else
            {
                IsSees = false;

                if (_changingFirePoint != null)
                {
                    StopCoroutine(_changingFirePoint);
                }
                _changingFirePoint = StartCoroutine(СhangingFirePoint(_timeAgroRotation));
                _isFirePointchanging = false;
            }
        }
        else
        {
            if (_firePoint.localPosition.z < 0)
            {
                IsSees = false;

                if (_changingFirePoint != null)
                {
                    StopCoroutine(_changingFirePoint);
                }
                _changingFirePoint = StartCoroutine(СhangingFirePoint(_timeAgroRotation));
                _isFirePointchanging = false;
            }
            else
            {
                IsSees = true;

                float _distToPlayer = _rigidbody.position.x - _playerTransform.position.x;
                if (_distToPlayer * _distToPlayer < _minDistanseBetweenPlayerAndEnemy * _minDistanseBetweenPlayerAndEnemy ||
                    transform.position.x < _leftPointTransformPositionX || transform.position.x > _rightPointTransformPositoinX)
                {
                    _directionMoving = Vector3.zero;
                    _animator.SetBool("Walk", false);
                }
                else
                {
                    _directionMoving = Vector3.left;
                    _animator.SetBool("Walk", true);
                }
            }
        }
    }

    private IEnumerator СhangingFirePoint(float _speedRotation)
    {
        _directionMoving = Vector3.zero;
        yield return new WaitForSeconds(_speedRotation);
        _firePoint.localPosition = new Vector3(_firePoint.localPosition.x, _firePoint.localPosition.y, _firePoint.localPosition.z * -1);
        if (_firePoint.localPosition.z > 0)
        {
            _directionMoving = Vector3.left;
        }
        else
        {
            _directionMoving = Vector3.right;
        }
        _animator.SetBool("Walk", true);
        yield return new WaitForFixedUpdate();
        _isFirePointchanging = true;
    }

    private void SetMoveIdle()
    {
        IsSees = false;

        if (transform.position.x < _leftPointTransformPositionX || transform.position.x > _rightPointTransformPositoinX)
        {
            _directionMoving = Vector3.zero;
            _animator.SetBool("Walk", false);
            if (_changingFirePoint != null)
            {
                StopCoroutine(_changingFirePoint);
            }
            _changingFirePoint = StartCoroutine(СhangingFirePoint(_timeIdleRotation));
            _isFirePointchanging = false;
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
