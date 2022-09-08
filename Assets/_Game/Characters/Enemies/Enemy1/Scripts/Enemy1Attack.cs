using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy1Attack : MonoBehaviour
{
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Enemy1Moving _enemy1Moving;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _delayAttack = 2f;

    private float _time;
    private float errorAngleWhichEnemyShoot = 20;

    private void Start()
    {
        _time = _delayAttack;
    }

    private void Update()
    {
        if (_delayAttack <= _time && _enemy1Moving.IsSees)
        {
            if (transform.rotation.eulerAngles.y > 180 - errorAngleWhichEnemyShoot)
            {
                Instantiate(_projectile, _firePoint.position, Quaternion.Euler(0, 180, 0));
                _time = 0;
            }
            else if (transform.rotation.eulerAngles.y < 0 + errorAngleWhichEnemyShoot)
            {
                Instantiate(_projectile, _firePoint.position, Quaternion.Euler(0, 0, 0));
                _time = 0;
            }
        }
        _time += Time.deltaTime;
    }
}
