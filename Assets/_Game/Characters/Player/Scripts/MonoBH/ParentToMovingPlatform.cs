using UnityEngine;

public class ParentToMovingPlatform : MonoBehaviour
{
    [Header("Физическое тело объекта")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Ссылка PlayerMovement")]
    [SerializeField] private PlayerMovement _playerMovement;

    private Transform _originParent;

    private void Awake()
    {
        _originParent = transform.parent;
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
            transform.parent = _originParent;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }
}
