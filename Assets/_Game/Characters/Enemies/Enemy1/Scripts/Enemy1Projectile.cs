using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy1Projectile : MonoBehaviour
{
    [Header("-----      Настройки      -----")]
    [Header("Скорость снаряда")]
    [SerializeField] private float _speed;

    [Header("Урон от снаряда")]
    [SerializeField, Range(1, 10)] private int _damage;

    [Header("Время жизни снаряда")]
    [SerializeField] private float _timeLife;

    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Rigidbody _rigidbody;

    private float _time;

    private void Start()
    {
        _rigidbody.velocity = _rigidbody.transform.right * _speed;
        _time = 0;
    }

    private void Update()
    {
        if (_timeLife <= _time)
        {
            _time = 0;
            Destroy(gameObject);
        }
        _time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamagable>() != null)
        {
            other.GetComponent<IDamagable>().TakeDamage(_damage);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer(StringConsts.WALL))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
