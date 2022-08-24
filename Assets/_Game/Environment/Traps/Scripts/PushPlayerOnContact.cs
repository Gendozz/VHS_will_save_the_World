using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PushPlayerOnContact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.ApplyExternalForce(transform.position);
        }
    }
}
