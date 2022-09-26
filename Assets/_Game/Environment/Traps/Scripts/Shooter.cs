using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Префаб снаряда")]
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Точка, откуда летит снаряд")]
    [SerializeField] private Transform _firePoint;

    [Header("Пауза между выстрелами")]
    [SerializeField] private float _cooldownDuration;

    public bool СanShoot { get; private set; } = true;

    public void Shoot(Vector3 inDirection)
    {
        if (СanShoot)
        {
            Instantiate(_projectilePrefab, _firePoint.position, Quaternion.Euler(inDirection));
            СanShoot = false;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldownDuration);
        СanShoot = true;
    }

}
