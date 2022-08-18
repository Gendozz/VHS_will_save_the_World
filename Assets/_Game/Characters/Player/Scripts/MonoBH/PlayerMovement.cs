using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("-----      Настройки перемещения и прыжка      -----")]

    [Header("Динамика изменения горизонтальной скорости от времени нажатия клавиши")]
    [SerializeField] private AnimationCurve _speedCurve;

    [Header("Сила прыжка")]
    [Range(5, 10)]
    [SerializeField] private float _jumpForce;

    [Header("Сила отскока от стены")]
    [SerializeField] private float _wallJumpForce;

    [Header("Продолжительность блокировки после рыжка от стены")]
    [SerializeField] private float _stopMoveAfterWallJumpDelay;

    [Header("Модификатор гравитации при падении")]
    [Range(1, 8)]
    [SerializeField] private float _fallGravityMultilier;

    [Header("Слой, который считать землёй")]
    [SerializeField] private LayerMask _groundLayer;

    [Space]
    [Header("-----      Компоненты и системные     -----")]

    [Header("Физическое тело объекта")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Скрипт получения пользовательского ввода")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Радиус от пивота персонажа, в котором детектится земля")]
    [SerializeField] private float _groundCheckRadius;

    [Header("Расстояние вбок от пивота персонажа, в которой детектятся стены")]
    [SerializeField] private Vector3 _wallCheckBoxSize;

    // Ground relative
    public bool IsGrounded { get; private set; } = false;

    private Collider[] _groundCollider = new Collider[1];

    // Horizontal movement relative

    private float _horizontalInputTreshold = 0.1f;

    private bool _isHorizontalMove = false;

    // Wall jump relative

    public bool IsOnWall { get; private set; } = false;

    private Collider[] _wallCollider = new Collider[1];

    private int _offTheWallDirection = 0;

    // Common
    private bool _canMove = true;


    private void Awake()
    {
        _rigidbody.sleepThreshold = 0f;
    }


    private void Update()
    {
        if (_canMove)
        {
            WaitWhenShouldMove();

            WaitWhenShouldJump();
        }

        ChangeGravityIfFalling();
    }
    private void WaitWhenShouldMove()
    {
        if (Mathf.Abs(_playerInput.HorizontalDirection) > _horizontalInputTreshold)
        {
            _isHorizontalMove = true;
        }
        else
        {
            _isHorizontalMove = false;
        }
    }

    private void WaitWhenShouldJump()
    {
        if (_playerInput.IsJumpButtonPressed)
        {
            if (IsGrounded)
            {
                Debug.Log("Jump");
                Jump();
            }
            else if (IsOnWall && !IsGrounded)
            {
                Debug.Log("Wall jump in direction " + _offTheWallDirection);
                WallJump();
            }
        }
    }

    private void ChangeGravityIfFalling()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += transform.up * Physics.gravity.y * (_fallGravityMultilier - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        CheckGround();

        CheckWall();

        if (_isHorizontalMove && _canMove)
        {
            MoveToDirection(_playerInput.HorizontalDirection);
        }
    }

    private void MoveToDirection(float direction)
    {
        _rigidbody.velocity = new Vector3(_speedCurve.Evaluate(direction), _rigidbody.velocity.y, _rigidbody.velocity.z);
    }

    private void Jump()
    {
        _rigidbody.velocity = transform.up * _jumpForce;
    }

    private void WallJump()
    {
        _rigidbody.velocity = Vector3.zero;
        StartCoroutine(BlockMovementOnSeconds(_stopMoveAfterWallJumpDelay));
        _rigidbody.velocity = new Vector3(_wallJumpForce * _offTheWallDirection, _jumpForce, _rigidbody.velocity.z);
    }

    private void CheckGround()
    {
        IsGrounded = Physics.OverlapSphereNonAlloc(transform.position, _groundCheckRadius, _groundCollider, _groundLayer) > 0;
    }

    private void CheckWall()
    {
        IsOnWall = Physics.OverlapBoxNonAlloc(transform.position + transform.up, _wallCheckBoxSize, _wallCollider, Quaternion.identity, _groundLayer) > 0;

        if (IsOnWall)
        {
            _offTheWallDirection = _wallCollider[0].transform.position.x - transform.position.x > 0 ? -1 : 1;
        }
    }

    private IEnumerator BlockMovementOnSeconds(float seconds)
    {
        _canMove = false;
        yield return new WaitForSeconds(seconds);
        _canMove = true;
    }

    private void OnDrawGizmos()
    {
        // Sphere that detects ground
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);

        // Box that detects walls
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + transform.up, _wallCheckBoxSize * 2);
    }
}


