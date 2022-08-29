using System.Collections;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [Header("-----      Настройки      -----")]
    [Header("Радиус АОЕ аттаки")]
    [SerializeField] private float _radiusAttack;

    [Header("Урон от аттаки")]
    [SerializeField] private int _damage;

    [Header("Время между аттаками")]
    [SerializeField] private float _delayAttack = 2f;

    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [Header("Трансформ игрока")]
    [SerializeField] private Transform _playerTransform;

    [Header("Отображение АОЕ аттаки")]
    [SerializeField] private GameObject _vfxAoe;

    private bool _isDelayAttackPassed = true;
    private bool _isSees = true;                // для тестов.

    private void Start()
    {
        _vfxAoe.active = false;
        _vfxAoe.transform.localScale = Vector3.one * _radiusAttack;
    }

    private void Update()
    {
        if (_isSees && _isDelayAttackPassed)
        {
            Collider[] aoeAttack = Physics.OverlapSphere(transform.position, _radiusAttack);
            foreach (Collider c in aoeAttack)
            {
                Debug.Log(c);
                if (c.GetComponent<PlayerInput>() != null)
                {
                    Debug.Log("зашёл в апдейте");
                    StartCoroutine(Attck(aoeAttack));
                    _isDelayAttackPassed = false;
                }
            }
        }
    }

    private IEnumerator Attck(Collider[] _AOEAttack)
    {

        DealingDamage(_AOEAttack);
        yield return new WaitForSeconds(_delayAttack);
        _vfxAoe.active = false;
        yield return _isDelayAttackPassed = true;
    }

    private void DealingDamage(Collider[] _AOEAttack)
    {
        _vfxAoe.active = true;

        foreach (Collider c in _AOEAttack)
        {
            if (c.GetComponent<IDamagable>() != null)
            {
                c.GetComponent<IDamagable>().TakeDamage(_damage);
                Debug.Log("урон нанёсся");
            }
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
