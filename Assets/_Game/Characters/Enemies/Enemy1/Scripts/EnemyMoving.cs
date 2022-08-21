using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMoving : MonoBehaviour
{
    [Header("-----      ���������      -----")]
    [Header("�����, ����� �������� �������� ���� �1")]
    [SerializeField] private Transform _leftPointTransform;
    [SerializeField] private Transform _rightPointTransform;

    [Header("�������� ����������� � ���������")]
    [SerializeField] private float _speedPosition;
    [SerializeField] private float _speedRotation;

    [Header("���� ���������, ��� ������� ���� ������ �����������")]
    [SerializeField] private float angleWhichEnemyChangesDirection = 70;

    [Space]
    [Header("-----      ���������� � ���������      -----")]
    [Header("���������� ���� �������")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("����������� ���������� ����� ������� � ������")]
    [SerializeField] private float _minDistanseBetweenPlayerAndEnemy = 1f;

    [Header("��������� ������")]
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private bool _isSees;                                       //����� � ��������� ��� ������.
    private Vector3 _directionMoving = Vector3.left;
    private float _leftPointTransformPositionX;
    private float _rightPointTransformPositoinX;

    private void Start()
    {
        _leftPointTransformPositionX = _leftPointTransform.position.x;
        _rightPointTransformPositoinX = _rightPointTransform.position.x;
    }

    private void Update()
    {
        if (_isSees)
        {
            GetDirectionToPlayer();
            GetStay();
        }
        else
        {
            KeepMoving();
            ChangeDirection();
            //�������� idle.
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _directionMoving * _speedPosition;
    } 

    private void GetDirectionToPlayer()
    {
        if (_playerTransform.position.x > _rigidbody.position.x)
        {
            _directionMoving = Vector3.right;
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, 0.1f, 0), _speedRotation);
        }
        if (_playerTransform.position.x < _rigidbody.position.x)
        {
            _directionMoving = Vector3.left;
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, 179.9f, 0), _speedRotation);
        }
        //�������� hunting.
    }

    private void GetStay()
    {
        float _distToPlayer = _rigidbody.position.x - _playerTransform.position.x;

        if (_rigidbody.position.x <= _leftPointTransformPositionX && (transform.eulerAngles.y > 180 - angleWhichEnemyChangesDirection) ||
            _rigidbody.position.x >= _rightPointTransformPositoinX && (transform.eulerAngles.y < 0 + angleWhichEnemyChangesDirection) ||
            _distToPlayer * _distToPlayer < _minDistanseBetweenPlayerAndEnemy * _minDistanseBetweenPlayerAndEnemy)
        {
            _directionMoving = Vector3.zero;
            //�������� stay.
        }
    }

    private void KeepMoving()
    {
        if (_directionMoving == Vector3.zero)
        {
            if (transform.eulerAngles.y > 180 - angleWhichEnemyChangesDirection)
            {
                _directionMoving = Vector3.left;
            }
            else
            {
                _directionMoving = Vector3.right;
            }
        }
    }

    private void ChangeDirection()
    {
        if (_leftPointTransformPositionX > _rigidbody.position.x)
        {
            _directionMoving = Vector3.zero;
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, 0.1f, 0), _speedRotation);
            if (transform.eulerAngles.y < 180 - angleWhichEnemyChangesDirection)
            {
                _directionMoving = Vector3.right;
            }
        }
        else if (_rightPointTransformPositoinX < _rigidbody.position.x)
        {
            _directionMoving = Vector3.zero;
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, 179.9f, 0), _speedRotation);
            if (transform.eulerAngles.y > 0 + angleWhichEnemyChangesDirection)
            {
                _directionMoving = Vector3.left;
            }
        }

        if (_directionMoving == Vector3.right)
        {
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, 0.1f, 0), _speedRotation);
        }
        else if (_directionMoving == Vector3.left)
        {
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0, 179.9f, 0), _speedRotation);
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
