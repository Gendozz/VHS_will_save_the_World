using System.Collections;
using UnityEngine;

public class Enemy3LookAtPlayer : MonoBehaviour
{
    public bool IsSees { get; private set; } = false;

    [Header("-----      Настройки      -----")]
    [SerializeField] private float _speedRotationIdle;
    [SerializeField] private float _speedRotationAgro;
    [SerializeField] private float _delayTimeState;
    [SerializeField] private float _visionTimeAbroad;
    [SerializeField] private float _timeAgroWhenPlayerLeft;
    [SerializeField] private float _additionalVisibility;
    [Space]
    [Header("-----      Компоненты и системное      -----")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerTransform;

    private float _lowerLimitFieldView;
    private float _upperLimitFieldView;
    private float _timerAgroWhenPlayerLeft;
    private float _angleRotation = 89.9f;
    private bool _onScreen;
    private bool _startIlde;
    private bool _playerInZone;
    private bool _isSight;

    private void Start()
    {
        _lowerLimitFieldView = transform.position.y - transform.localScale.y;
        _upperLimitFieldView = transform.position.y + transform.localScale.y + _additionalVisibility;
        _timerAgroWhenPlayerLeft = _timeAgroWhenPlayerLeft;
        _startIlde = true;
    }

    private void Update()
    {
        PlayerInZone();
        StandingRightLeft();

        if ((_onScreen && _playerInZone && _isSight) || 
            (_timerAgroWhenPlayerLeft < _timeAgroWhenPlayerLeft && _timerAgroWhenPlayerLeft != 0))
        {
            _animator.SetBool("ToAgro", true);
            IsSees = true;
            _timerAgroWhenPlayerLeft += Time.deltaTime;
            ToAgro(); 
        }
        else if (_startIlde)
        {
            _animator.SetBool("ToAgro", false);
            IsSees = false;
            _timerAgroWhenPlayerLeft = 0;
            _startIlde = false;
            StartCoroutine(ToIdle());
        }
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

    private void StandingRightLeft()
    {
        if (_playerTransform.position.x > transform.position.x && transform.rotation.y < 0 ||
            _playerTransform.position.x < transform.position.x && transform.rotation.y > 0)
        {
            _isSight = true;
        }
        else
        {
            _isSight = false;
        }
    }

    private void ToAgro()
    {
        StopAllCor();
        _startIlde = true;

        if (_playerTransform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -_angleRotation, 0), _speedRotationAgro);
        }
        if (_playerTransform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, _angleRotation, 0), _speedRotationAgro);
        }
    }

    private IEnumerator ToIdle()
    {
        float angle = Quaternion.Euler(0, -_angleRotation + 1, 0).y;
        while (transform.rotation.y > angle)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -_angleRotation, 0), _speedRotationIdle);
            yield return null;
        }
        yield return new WaitForSeconds(_delayTimeState);

        angle = Quaternion.Euler(0, _angleRotation - 1, 0).y;

        while (transform.rotation.y < angle)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, _angleRotation, 0), _speedRotationIdle);
            yield return null;
        }
        yield return new WaitForSeconds(_delayTimeState);

        yield return StartCoroutine(ToIdle());
    }

    private void OnBecameVisible()
    {
        _onScreen = true;
    }

    private void OnBecameInvisible()
    {
        _onScreen = false;
    }

    public void StopAllCor()
    {
        StopAllCoroutines();
    }

    public void ResurectionFromIdle()
    {
        _startIlde = true;
    }
}
