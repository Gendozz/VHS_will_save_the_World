using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter), typeof(BoxCollider))]
public class ShootingTrap : MonoBehaviour
{
    [Header("������ �� ��������� Shooter")]
    [SerializeField] private Shooter shooter;

    private void OnTriggerStay(Collider other)
    {
        if (shooter.�anShoot)
        {
            if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
            {
                shooter.Shoot(transform.rotation.eulerAngles);
            }
        }
    }

}
