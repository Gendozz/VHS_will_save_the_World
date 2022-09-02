using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private Transform _firePoint;

    [SerializeField] private float _cooldownDuration;

    public bool ÑanShoot { get; private set; } = true;

    public void Shoot(Vector3 inDirection)
    {
        if (ÑanShoot)
        {
            Instantiate(_projectilePrefab, _firePoint.position, Quaternion.Euler(inDirection));
            ÑanShoot = false;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldownDuration);
        ÑanShoot = true;
    }

}
