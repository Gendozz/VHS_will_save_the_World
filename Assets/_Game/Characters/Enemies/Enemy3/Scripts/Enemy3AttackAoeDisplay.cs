using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy3AttackAoeDisplay : MonoBehaviour
{
    [Header("-----      Настройки      -----")]
    [Header("AOE аттака")]
    [SerializeField] private float _delayAttackAoe;
    [SerializeField] private float _scaleMaxAoeMultiply;
    [SerializeField] private float _timeToMaxAoe;
    [SerializeField] private float _timeToMinAoe;
    [SerializeField] private float _delayTimeMaxStateAoe;
    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private GameObject _objAoe;
    [SerializeField] private Enemy3LookAtPlayer _enemy3LookAtPlayer;

    private Transform _playerTransform;
    private Vector3 _startScaleAOE;
    private Vector3 _radiusAoe;
    private bool _isDelayAttackPassed;
    private string _playerName = "PlayerMain";

    private void Start()
    {
        _playerTransform = GameObject.Find(_playerName).transform;

        _isDelayAttackPassed = true;
        _startScaleAOE = _objAoe.transform.localScale;
        _radiusAoe = _startScaleAOE * _scaleMaxAoeMultiply;
    }

    private void Update()
    {
        if (_enemy3LookAtPlayer.IsSees && Vector3.Distance(_objAoe.transform.position, _playerTransform.position) < _radiusAoe.x)
        {
            if (_isDelayAttackPassed)
            {
                Collider[] aoeAttack = Physics.OverlapSphere(transform.position, _radiusAoe.x);
                foreach (Collider c in aoeAttack)
                {
                    if (c.GetComponent<PlayerInput>() != null)
                    {
                        _isDelayAttackPassed = false;
                        StartCoroutine(AoeResizing());
                    }
                }
            }
        }
    }

    private IEnumerator AoeResizing()
    {
        Vector3 startScale = _objAoe.transform.localScale;
        var elapsedTime = 0.0f;

        while ((elapsedTime += Time.deltaTime) <= _timeToMaxAoe)
        {
            _objAoe.transform.localScale = Vector3.Lerp(startScale, _radiusAoe, elapsedTime / _timeToMaxAoe);

            yield return null;
        }

        yield return new WaitForSeconds(_delayTimeMaxStateAoe);

        startScale = _objAoe.transform.localScale;

        elapsedTime = 0.0f;

        while ((elapsedTime += Time.deltaTime) <= _timeToMinAoe)
        {
            _objAoe.transform.localScale = Vector3.Lerp(startScale, _startScaleAOE, elapsedTime / _timeToMinAoe);

            yield return null;
        }

        _objAoe.transform.localScale = _startScaleAOE;

        StartCoroutine(WaitDelay());
    }

    private IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(_delayAttackAoe);
        _isDelayAttackPassed = true;
    }
}
