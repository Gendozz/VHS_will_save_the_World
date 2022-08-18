using UnityEngine;

public class ParentToMovingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private PlayerMovement _playerMovement;


    private Transform originParent;

    private void Awake()
    {
        originParent = transform.parent;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IMovingPlatform>(out IMovingPlatform movingPlatform) && _playerMovement.IsGrounded)
        {
            transform.parent = collision.transform;
            _rigidbody.interpolation = RigidbodyInterpolation.None;   
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IMovingPlatform>(out IMovingPlatform movingPlatform))
        {
            transform.parent = originParent;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }
}
