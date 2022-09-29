using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PushPlayerOnContact : MonoBehaviour
{
    [SerializeField] private bool _isOneTimeUse;

    private bool _wasContact;

    private void OnCollisionEnter(Collision collision)
    {
        if (!_wasContact && collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.ApplyExternalForce(transform.position);
            if (_isOneTimeUse)
            {
                _wasContact = true;
            }
        }
    }
}
