using System.Collections;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [Header("Размер зоны детекта \nнастраивается коллайдером")]

    [Header("Объект, который нужно активировать")]
    [SerializeField] private Component _iActivatedComponent;

    [Header("Задержка активации")]
    [SerializeField] private float _activationDelay;

    [Header("Период кулдауна активатора")]
    [SerializeField] private float _cooldownDelay;

    [Header("Активатор одноразового использования?")]
    [SerializeField] private bool _isOneTimeUse;

    private bool _isReady = true;

    private IActivatable _activatable;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            StartCoroutine(Activate());
        }
    }

    private void OnValidate()
    {
        _activatable = _iActivatedComponent.GetComponent<IActivatable>();
        if (_activatable == null) _iActivatedComponent = null;
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(_activationDelay);
        _activatable.Activate();
        if (_isOneTimeUse)
        {
            TurnOffActivator(); 
        }
    }

    private void TurnOffActivator()
    {
        gameObject.SetActive(false);
    }
}
