using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy1Attack : MonoBehaviour
{
    [Space]
    [Header("-----      ���������� � ���������      -----")]

    [Header("������ �������")]
    [SerializeField] private GameObject _projectile;

    [Header("����� ��������")]
    [SerializeField] private Transform _firePoint;

    [Header("����� ����� ����������")]
    [SerializeField] private float _delayShoot = 2f;

    private float _time;
    private float errorAngleWhichEnemyShoot = 10;
    [SerializeField] private bool _isSees;                               //����� � ��������� ��� ������.

    private void Start()
    {
        _time = _delayShoot;
    }

    private void Update()
    {
        if (_delayShoot <= _time && _isSees)
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

    /*private void OnBecameVisible()
    {
        _isSees = true;
    }

    private void OnBecameInvisible()
    {
        _isSees = false;
    }*/
}
