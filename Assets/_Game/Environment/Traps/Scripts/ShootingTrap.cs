using JSAM;
using UnityEngine;

[RequireComponent(typeof(Shooter), typeof(BoxCollider))]
public class ShootingTrap : MonoBehaviour
{
    [Header("Ссылка на коампнент Shooter")]
    [SerializeField] private Shooter shooter;

    private void OnTriggerStay(Collider other)
    {
        if (shooter.СanShoot)
        {
            if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
            {
                shooter.Shoot(transform.rotation.eulerAngles);
                AudioManager.PlaySound(Sounds.ShootingTrapSound);
            }
        }
    }

}
