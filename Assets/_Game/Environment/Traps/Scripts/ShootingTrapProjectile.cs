using System.Collections;
using UnityEngine;

public class ShootingTrapProjectile : MonoBehaviour
{
    [Header("Скорость снаряда")]
    [SerializeField] private float _projectileSpeed;

    [Header("Время жизни")]
    [SerializeField] private float _lifeTime;

    [Header("Время жизни после попадания")]
    [SerializeField] private float _delayBeforeHideProjectileAfterContact;

    [Header("Ссылка на компонент Rigidbody")]
    [SerializeField] private Rigidbody _rigidbody;

    private int _damage = 1;

    private bool _wasContact = false;

    void Start()
    {
        _rigidbody.velocity = _rigidbody.transform.right * _projectileSpeed;
        StartCoroutine(DeactivateProjectileInTime(_lifeTime));
    }

    private IEnumerator DeactivateProjectileInTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_wasContact && collision.gameObject.TryGetComponent(out IDamagable idamagable))
        {
            idamagable.TakeDamage(_damage);
            _wasContact = true;
            StopAllCoroutines();
            StartCoroutine(DeactivateProjectileInTime(_delayBeforeHideProjectileAfterContact));
        }
    }
}
