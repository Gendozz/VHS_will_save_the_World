using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private Rigidbody _pendulumRigidbody;

    [SerializeField] private Transform _fasteners;

    [SerializeField] private float _addingForce;

    [SerializeField] private int _direction = 1;

    void Update()
    {
        if(_pendulumRigidbody.velocity.y > 0 && Mathf.Sign(_fasteners.position.x - _pendulumRigidbody.transform.position.x) != _direction)
        {
            _pendulumRigidbody.AddForce(_direction * _addingForce * Vector3.right, ForceMode.VelocityChange);
            _direction *= -1;
        }
    }
}
