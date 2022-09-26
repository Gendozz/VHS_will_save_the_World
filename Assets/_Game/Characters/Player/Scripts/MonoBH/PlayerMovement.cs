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

    [Header("Слой, который считать землёй")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Модификатор гравитации при падении")]
    [SerializeField] private float _fallGravityMultiplier;

    [Header("С какой скорости по Y применять модификатор гравитации")]
    [SerializeField] private float _fallGravityVelocityYStart;

    [Header("Сила замедления смены направления в воздухе")]
    [SerializeField] private float _backForceOnJump;

    [Header("ДЛЯ ТЕСТА --- Есть ли способность ко второму прыжку?")]
    [SerializeField] private bool _haveDoubleJumpAbility = false;            // Temp imitation of switching-on/off double jump ability

    [Header("Сила прыжка вверх от земли")]
    [SerializeField] private float _jumpForce;

    [Header("Сила прыжка вверх на батуте")]
    [SerializeField] private float _trampolineJumpForce;

    [Header("Слой, который считать стеной")]
    [SerializeField] private LayerMask _wallLayer;

    [Header("Скорость скольжения по стене")]
    [SerializeField] private float _wallSlideGravityVelocity;

    [Header("Сила отскока от стены")]
    [SerializeField] private float _wallJumpSideForce;

    [Header("Сила прыжка вверх от стены")]
    [SerializeField] private float _wallJumpUpForce;

    [Header("Максимальный вертикальная скорость по инерции на стене")]
    [SerializeField] private float _limitOnWallUpInertion;

    [Header("Продолжительность блокировки после прыжка от стены")]
    [SerializeField] private float _afterWallJumpBlockMovementDuration;

    [Space]
    [Header("-----      Взаимодействия со скользкой платформой      -----")]
    [Header("Динамика изменения горизонтальной скорости от времени нажатия клавиши")]
    [SerializeField] private AnimationCurve _slipperySpeedCurve;

    [Header("Слой, который считать скользкой платформой")]
    [SerializeField] private LayerMask _slipperyLayer;

    [SerializeField] private LayerMask _trampolineLayer;

    [Space]
    [Header("-----      Взаимодействие с ловушками      -----")]
    [Header("Продолжительность блокировки после контакта с ловушкой")]
    [SerializeField] private float _afterGotTrappedBlockMovementDuration;

    [Header("Сила отталкивания вверх при контакте с ловушкой")]
    [SerializeField] private float _forceYOnTrapContact;

    [Header("Сила отталкивания вбок при контакте с ловушкой")]
    [SerializeField] private float _forceXOnTrapContact;

    [Header("Сила отталкивания вверх при контакте с ловушкой-пружиной")]
    [SerializeField] private float _springTrapForceY;

    [Header("Сила отталкивания вбок при контакте с ловушкой-пружиной")]
    [SerializeField] private float _springTrepForceX;

    [Space]
    [Header("-----      Компоненты и системные     -----")]

    [Header("Физическое тело объекта")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Скрипт получения пользовательского ввода")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Радиус от пивота персонажа, в котором детектится земля")]
    [SerializeField] private float _groundCheckRadius;

    [SerializeField] private CapsuleCollider _mainCollider;

    [Header("Расстояние вбок от пивота персонажа, в которой детектятся стены")]
    [SerializeField] private Vector3 _wallCheckBoxHalfSize;

    [Header("Оффсет центра детекта стен")]
    [SerializeField] private Vector3 _checkBoxCenterOffset;

    [Header("Длительность блокировки детекта стен после прыжка от стены")]
    [SerializeField] private float _blockWallDetectionDuration;

    // Ground relative
    public bool IsGrounded { get; private set; } = false;

    private Collider[] _groundCollider = new Collider[1];

    private Collider[] _slipperyCollider = new Collider[1];

    private Collider[] _trampolineCollider = new Collider[1];

    private bool _shouldDetectGround = true;

    // Horizontal movement relative

    private float _horizontalInputTreshold = 0.15f;

    // Jump relative

    public bool HaveDoubleJumpAbility { get { return _haveDoubleJumpAbility; } private set { } }        // TODO: To refactor after implement double jump ability

    private bool _isJumpInput;

    private bool _canDoubleJump;

    // Wall jump relative

    public bool IsOnWall { get; private set; } = false;

    private Collider[] _wallCollider = new Collider[1];

    private int _offTheWallDirection = 0;

    private bool _shoudDetectWall = true;

    public Action onWall;


    // Common
    private bool _canMove = true;

    private Coroutine _blockMovementRoutine = null;

    private bool _isBlockMovementRoutineStillWorking = false;

    private bool _isGrappling = false;

    private bool _isOnSlippery = false;

    private bool _isOnTrampoline = false;

    private bool _isInTrampolineTrigger = false;
    

    // For animation

    public float VelocityY => _rigidbody.velocity.y;

    // For pause

    private Vector3 onPauseVelocity;

    private Vector3 onPauseAngularVelocity;

    private void Awake()
    {
        _rigidbody.sleepThreshold = 0f;
    }


    private void Update()
    {
        ModifyGravityDependingOnPlayerStatus();

        SetIsJumpInput();

    }

    private void ModifyGravityDependingOnPlayerStatus()
    {
        if (_rigidbody.velocity.y < _fallGravityVelocityYStart && !_isGrappling)
        {
            if (IsOnWall)
            {

                //_rigidbody.velocity += transform.up * Physics.gravity.y * (_wallSlideGravityMultiplier - 1) * Time.deltaTime; // Uncomment this and comment next line If no need to decrease speed on slide start when fall velocity is big
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, -_wallSlideGravityVelocity, float.MaxValue), _rigidbody.velocity.y);

                return;
            }
            _rigidbody.velocity += transform.up * Physics.gravity.y * (_fallGravityMultiplier - 1) * Time.deltaTime;
        }
    }

    private void SetIsJumpInput()
    {
        if (_playerInput.IsJumpButtonPressed && _canMove)
        {
            _isJumpInput = true;
        }
    }

    private void FixedUpdate()
    {

        if (_shouldDetectGround)
        {
            CheckGround(); 
        }

        if (_shoudDetectWall)
        {
            CheckWall();
        }

        if (_canMove)
        {
            ApplyInputToHorizontalMovement();

            ApplyInputToJump();
        }

        if (_isOnTrampoline)
        {
            Jump(_jumpForce);
        }
    }


    private void ApplyInputToJump()
    {
        switch (_haveDoubleJumpAbility)
        {
            case false:
                if (_isJumpInput)
                {
                    if (_isInTrampolineTrigger)
                    {
                        Jump(_trampolineJumpForce);
                    }
                    else if (IsGrounded || _isOnSlippery)
                    {
                        Jump(_jumpForce);
                    }
                    else if (IsOnWall)
                    {
                        WallJump();
                    }
                }
                break;
            case true:
                if (_isJumpInput)
                {
                    if (_isInTrampolineTrigger)
                    {
                        Jump(_trampolineJumpForce);
                    }
                    else if (IsGrounded || _isOnSlippery)
                    {
                        Jump(_jumpForce);
                        _canDoubleJump = true;
                    }
                    else if (IsOnWall)
                    {
                        WallJump();
                        _canDoubleJump = true;
                    }
                    else if (_canDoubleJump)
                    {
                        Jump(_jumpForce);
                        _canDoubleJump = false;
                    }
                }
                break;
        }

        _isJumpInput = false;
    }

    private void ApplyInputToHorizontalMovement()
    {
        if (ShouldApplyHorizontalMovement())
        {
            if (IsGrounded)
            {
                if (!_isOnSlippery)
                {
                    _rigidbody.velocity = new Vector3(_speedCurve.Evaluate(_playerInput.HorizontalDirection), _rigidbody.velocity.y, _rigidbody.velocity.z);
                }
                else
                {
                    _rigidbody.velocity = new Vector3(_speedCurve.Evaluate(_playerInput.HorizontalDirection), _rigidbody.velocity.y, _rigidbody.velocity.z);
                }
            }
            else
            {
                _rigidbody.velocity += _backForceOnJump * Time.deltaTime * new Vector3(Mathf.Sign(_playerInput.HorizontalDirection), 0, 0);
                _rigidbody.velocity = new Vector3(Mathf.Clamp(_rigidbody.velocity.x, _speedCurve.Evaluate(-1), _speedCurve.Evaluate(1)), _rigidbody.velocity.y, _rigidbody.velocity.z);
            }
        }
    }

    public bool ShouldApplyHorizontalMovement()
    {
        return Mathf.Abs(_playerInput.HorizontalDirection) > _horizontalInputTreshold;
    }

    private void Jump(float jumpForce)
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpForce, 0);
        _shouldDetectGround = false;
        IsGrounded = false;
        StartCoroutine(RestoreGroundDetection(0.5f));
    }

    private void WallJump()
    {
        _rigidbody.velocity = Vector3.zero;
        _shoudDetectWall = false;
        StartCoroutine(RestoreWallDetection(_blockWallDetectionDuration));
        _blockMovementRoutine = StartCoroutine(BlockMovementOnSeconds(_afterWallJumpBlockMovementDuration));
        _rigidbody.velocity = new Vector3(_wallJumpSideForce * _offTheWallDirection, _wallJumpUpForce, _rigidbody.velocity.z);
    }

    private IEnumerator RestoreWallDetection(float delay)
    {
        yield return new WaitForSeconds(delay);
        _shoudDetectWall = true;
    }

    private IEnumerator RestoreGroundDetection(float delay)
    {
        yield return new WaitForSeconds(delay);
        _shouldDetectGround = true;
    }

    private void CheckGround()
    {
        IsGrounded = Physics.OverlapSphereNonAlloc(transform.position, _groundCheckRadius, _groundCollider, _groundLayer) > 0;
        _isOnSlippery = Physics.OverlapSphereNonAlloc(transform.position, _groundCheckRadius, _slipperyCollider, _slipperyLayer) > 0;
        _isOnTrampoline = Physics.OverlapSphereNonAlloc(transform.position, _groundCheckRadius, _trampolineCollider, _trampolineLayer) > 0;
    }

    private void CheckWall()
    {
        IsOnWall = Physics.OverlapBoxNonAlloc(transform.position + _checkBoxCenterOffset, _wallCheckBoxHalfSize, _wallCollider, Quaternion.identity, _wallLayer) > 0;

        if (IsOnWall)
        {
            _offTheWallDirection = MathF.Sign(transform.position.x - _wallCollider[0].transform.position.x);

            if (_rigidbody.velocity.y > _limitOnWallUpInertion)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _limitOnWallUpInertion, _rigidbody.velocity.z);
            }

            // For the case when jump from another wall and still blocking moving
            if (_isBlockMovementRoutineStillWorking)
            {
                InterruptBlockMovementRoutine();
            }
        }
    }

    private IEnumerator BlockMovementOnSeconds(float seconds)
    {
        _isBlockMovementRoutineStillWorking = true;

        _canMove = false;

        yield return new WaitForSeconds(seconds);

        _canMove = true;

        _isBlockMovementRoutineStillWorking = false;
    }

    private void InterruptBlockMovementRoutine()
    {
        StopCoroutine(_blockMovementRoutine);
        _isBlockMovementRoutineStillWorking = false;
        _canMove = true;
    }

    public void ChangeMovementParamsOnStartGrappling()
    {
        _isGrappling = true;
    }

    public void RestoreMovementParamsOnStartGrappling()
    {
        _isGrappling = false;
    }

    public void ApplyExternalForce(Vector3 fromPosition)
    {
        StartCoroutine(BlockMovementOnSeconds(_afterGotTrappedBlockMovementDuration));
        _rigidbody.velocity = Vector3.zero;
        float forceDirection = Mathf.Sign(transform.position.x - fromPosition.x);
        _rigidbody.velocity = new Vector3(forceDirection * _forceXOnTrapContact, _forceYOnTrapContact, _rigidbody.velocity.z);
    }

    public void SetDoubleJumpAbility(bool haveDoubleJumpAbilty)
    {
        _haveDoubleJumpAbility = haveDoubleJumpAbilty;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(StringConsts.TRAMPOLINE))
        {
            _isInTrampolineTrigger = true;
        }
        if (other.gameObject.CompareTag(StringConsts.SPRINGTRAP))
        {
            _rigidbody.velocity = Vector3.zero;
            StartCoroutine(BlockMovementOnSeconds(.8f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(StringConsts.TRAMPOLINE))
        {
            _isInTrampolineTrigger = false;
        }
        if (other.gameObject.CompareTag(StringConsts.SPRINGTRAP))
        {
            float forceDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
            _rigidbody.velocity = _rigidbody.velocity = new Vector3(forceDirection * _springTrepForceX, _springTrapForceY, _rigidbody.velocity.z);
        }
    }

    public void MoveToCoords(Vector3 coords)
    {
        _rigidbody.velocity = Vector3.zero;
        StartCoroutine(BlockMovementOnSeconds(.2f));
        _rigidbody.MovePosition(coords);
    }

    public void PauseUnpauseActions(bool doPausing)
    {
        if (doPausing)
        {
            onPauseVelocity = _rigidbody.velocity;
            onPauseAngularVelocity = _rigidbody.angularVelocity;
            _rigidbody.isKinematic = true;
        }
        else
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = onPauseVelocity;
            _rigidbody.angularVelocity = onPauseAngularVelocity;
        }
    }

    private void OnDrawGizmos()
    {
        // Sphere that detects ground
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);

        // Box that detects walls
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + _checkBoxCenterOffset, _wallCheckBoxHalfSize * 2);
    }
}


