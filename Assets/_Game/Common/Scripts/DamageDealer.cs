using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageDealer : MonoBehaviour
{
    [Header("Размер наносимого урона")]
    [SerializeField] private int _damage;

    [Header("После причинения ущерба объект должен исчезнуть?")]
    [SerializeField] private bool _isOneTimeUse;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
        }

        if (_isOneTimeUse)
        {
            gameObject.SetActive(false);
        }
    }
}
