using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayerUp : MonoBehaviour
{
    [SerializeField] private float _force;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.GetComponent<Rigidbody>().AddForce(_force * Vector3.up, ForceMode.Impulse);
        }
    }
}
