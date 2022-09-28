using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy1Attack : MonoBehaviour
{
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Enemy1Moving _enemy1Moving;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _delayAttack;

    private float _time;

    private void Start()
    {
        _time = _delayAttack;
    }

    private void Update()
    {
        if (_enemy1Moving.IsSees)
        {
            if (_delayAttack <= _time)
            {
                if (_firePoint.localPosition.z < 0)
                {
                    Instantiate(_projectile, _firePoint.position, Quaternion.Euler(0, 0, 0));
                    _time = 0;
                }
                else
                {
                    Instantiate(_projectile, _firePoint.position, Quaternion.Euler(0, 180, 0));
                    _time = 0;
                }
            }
            _time += Time.deltaTime;
        }
    }
}
