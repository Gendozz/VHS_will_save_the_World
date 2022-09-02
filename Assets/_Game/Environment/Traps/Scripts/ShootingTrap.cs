using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter), typeof(BoxCollider))]
public class ShootingTrap : MonoBehaviour
{
    [SerializeField] private Shooter shooter;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
    //    {
    //        shooter.Shoot(transform.rotation.eulerAngles);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (shooter.ÑanShoot)
        {
            if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
            {
                shooter.Shoot(transform.rotation.eulerAngles);
            }
        }
    }

}
