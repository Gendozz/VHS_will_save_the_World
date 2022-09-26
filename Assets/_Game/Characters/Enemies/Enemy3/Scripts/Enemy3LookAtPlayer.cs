using System.Collections;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class Enemy3LookAtPlayer : MonoBehaviour
{
    public bool IsSees { get; private set; } = false;

    [Header("-----      Настройки      -----")]
    [SerializeField] private float _speedRotationIdle;
    [SerializeField] private float _speedRotationAgro;
    [SerializeField] private float _delayTimeState;
    [SerializeField] private float _timeAgroWhenPlayerLeft;
    [SerializeField] private float _additionalVisibility;
    [Space]
    [Header("-----      Компоненты и системное      -----")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerTransform;

    private Coroutine _isIdle;
    private Coroutine _isAgro;
    private Coroutine _isDoesntSeeButAggro;
    private float _lowerLimitFieldView;
    private float _upperLimitFieldView;
    private float _angleRotation = 89.9f;
    private bool _onScreen;
    private bool _startAgro;
    private bool _startDoesntSeeButAggro;
    private bool _startIlde;
    private bool _playerInZone;
    private bool _isSight;

    private void Start()
    {
        _lowerLimitFieldView = transform.position.y;
        _upperLimitFieldView = transform.position.y + transform.localScale.y + _additionalVisibility;
        _startIlde = true;
        _startAgro = true;
        _startDoesntSeeButAggro = false;
    }

    private void Update()
    {
        PlayerInZone();
        StandingRightLeft();

        if ((_onScreen && _playerInZone && _isSight))
        {
            if (!_startDoesntSeeButAggro)
            {
                _startDoesntSeeButAggro = true;

                if (_isDoesntSeeButAggro != null)
                {
                    StopCoroutine(_isDoesntSeeButAggro);
                }
            }

            if (_startAgro)
            {
                _startAgro = false;
                _startIlde = false;
                _startDoesntSeeButAggro = true;
                _isAgro = StartCoroutine(ToAgro());
            }
        }
        else if(_startDoesntSeeButAggro)
        {
            _startAgro = true;
            _startDoesntSeeButAggro = false;
            _isDoesntSeeButAggro = StartCoroutine((ToDoesntSeeButAggro()));
        }
        else if (_startIlde)
        {
            _startIlde = false;
            _isIdle = StartCoroutine(ToIdle());
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

    private IEnumerator ToAgro()
    {        
        _animator.SetBool("ToAgro", true);
        IsSees = true;
        StopCoroutine(_isIdle);

        while (true)
        {
            if (_playerTransform.position.x > transform.position.x)
            {
                yield return transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -_angleRotation, 0), _speedRotationAgro);
            }
            if (_playerTransform.position.x < transform.position.x)
            {
                yield return transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, _angleRotation, 0), _speedRotationAgro);
            }
        }
    }

    private IEnumerator ToDoesntSeeButAggro()
    {
        yield return new WaitForSeconds(_timeAgroWhenPlayerLeft);
        _startIlde = true;
        StopCoroutine(_isAgro);

        Debug.Log("вот и всё");
    }

    private IEnumerator ToIdle()
    {
        _animator.SetBool("ToAgro", false);
        IsSees = false;

        while (true)
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

    public void StopAllCor()
    {
        _startIlde = true;
        _startAgro = true;
        _startDoesntSeeButAggro = false;
        StopAllCoroutines();
    }

    public void ResurectionFromIdle()
    {
        _startIlde = true;
    }
}
