using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private Component _iActivatedComponent;

    private IActivated _activated;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _activated.Activate();
        }
    }

    private void OnValidate()
    {
        //_interfaceToExpose.TryGetComponent<IActivated>(out _activated);

        _activated = _iActivatedComponent.GetComponent<IActivated>();
        if (_activated == null) _iActivatedComponent = null;
    }
}
