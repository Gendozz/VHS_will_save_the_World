using System;
using System.Collections;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [Header("Размер зоны детекта \nнастраивается коллайдером")]
    
    [Header("Список объектов, которые нужно активировать")]
    [SerializeField] private GameObject[] _iActivatedComponents;

    [Header("Задержка активации")]
    [SerializeField] private float _activationDelay;

    [Header("Период кулдауна активатора")]
    [SerializeField] private float _cooldownDelay;

    [Header("Активатор одноразового использования?")]
    [SerializeField] private bool _isOneTimeUse;

    private bool _isReady = true;

    private IActivatable[] _activatables;

    public Action onActivate;
    public Action onActivatorRestored;

    private void Awake()
    {
        GrabActivatables();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement) && _isReady)
        {
            StartCoroutine(Activate());
        }
    }

    private void OnValidate()
    {
        GrabActivatables();
    }

    private IEnumerator Activate()
    {
        onActivate?.Invoke();
        yield return new WaitForSeconds(_activationDelay);
        
        foreach (var activatable in _activatables)
        {
            activatable.Activate();
        }
        if (_isOneTimeUse)
        {
            TurnOffActivator();
        }
        else
        {
            _isReady = false;
            StartCoroutine(RestoreActivator());
        }
    }

    private void TurnOffActivator()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator RestoreActivator()
    {
        yield return new WaitForSeconds(_cooldownDelay);
        _isReady = true;
        onActivatorRestored?.Invoke();
    }

    private void GrabActivatables() 
    {
        _activatables = new IActivatable[_iActivatedComponents.Length];
        for (int i = 0; i < _iActivatedComponents.Length; i++)
        {
            _activatables[i] = _iActivatedComponents[i].GetComponent<IActivatable>();
            if (_activatables[i] == null)
            {
                _iActivatedComponents[i] = null;
                Debug.LogError("Добавленный объект не может быть активирован. Добавь другой объект");
            }
        }
    }
}
