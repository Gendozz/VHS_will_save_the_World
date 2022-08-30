using System.Collections;
using UnityEngine;

public class Enemy3AttackDisplay : MonoBehaviour
{
    [Header("-----      Настройки      -----")]
    [Header("AOE аттака")]
    [SerializeField] private float _delayAttack;
    [SerializeField] private float _scaleMax;
    [SerializeField] private float _timeToMax;
    [SerializeField] private float _timeToMin;
    [SerializeField] private float _delayTimeMaxState;

    [Header("Размер врага")]


    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [Header("Трансформ игрока")]
    [SerializeField] private Transform _playerTransform;

    [Header("Отображение АОЕ аттаки")]
    [SerializeField] private GameObject _vfxAoe;
    

    private bool _isDelayAttackPassed;
    private Vector3 _startScaleAOE = Vector3.one;
    private bool _isSees = true;                                    // для тестов.

    private void Start()
    {
        _isDelayAttackPassed = true;

        _vfxAoe.transform.localScale = Vector3.one;
    }

    private void Update()
    {
        if (_isSees && _isDelayAttackPassed)
        {
            Collider[] aoeAttack = Physics.OverlapSphere(transform.position, _scaleMax / transform.localScale.x);
            foreach (Collider c in aoeAttack)
            {
                if (c.GetComponent<PlayerInput>() != null)
                {
                    _isDelayAttackPassed = false;
                    StartCoroutine(ObjectResizing());
                }
            }


        }
    }

    private IEnumerator EnemyDecrease()
    {
        Vector3 startScale = transform.localScale;
        var elapsedTime = 0.0f;
        var scaleMax = new Vector3(_scaleMax, _scaleMax, _scaleMax);

        while ((elapsedTime += Time.deltaTime) <= _timeToMin)
        {
            _vfxAoe.transform.localScale = Vector3.Lerp(startScale, _startScaleAOE, elapsedTime / _timeToMin);

            yield return null;
        }
    }

    private IEnumerator EnemyIncrease()
    {
        Vector3 startScale = transform.localScale;
        var elapsedTime = 0.0f;
        var scaleMax = new Vector3(_scaleMax, _scaleMax, _scaleMax);

        while ((elapsedTime += Time.deltaTime) <= _timeToMax)
        {
            _vfxAoe.transform.localScale = Vector3.Lerp(startScale, scaleMax, elapsedTime / _timeToMax);

            yield return null;
        }
    }


    private IEnumerator ObjectResizing()
    {
        Vector3 startScale = _vfxAoe.transform.localScale;
        var elapsedTime = 0.0f;
        var scaleMax = new Vector3(_scaleMax, _scaleMax, _scaleMax);

        while ((elapsedTime += Time.deltaTime) <= _timeToMax)
        {
            _vfxAoe.transform.localScale = Vector3.Lerp(startScale, scaleMax, elapsedTime / _timeToMax);

            yield return null;
        }

        yield return new WaitForSeconds(_delayTimeMaxState);

        startScale = _vfxAoe.transform.localScale;

        elapsedTime = 0.0f;

        while ((elapsedTime += Time.deltaTime) <= _timeToMin)
        {
            _vfxAoe.transform.localScale = Vector3.Lerp(startScale, _startScaleAOE, elapsedTime / _timeToMin);

            yield return null;
        }

        _vfxAoe.transform.localScale = _startScaleAOE;

        StartCoroutine(WaitDelay());
    }

    private IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(_delayAttack);
        yield return _isDelayAttackPassed = true;
    }


}
