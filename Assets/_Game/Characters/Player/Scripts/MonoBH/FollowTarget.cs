using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _zOffset;

    void Update()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, _target.position.z + _zOffset);
    }
}
