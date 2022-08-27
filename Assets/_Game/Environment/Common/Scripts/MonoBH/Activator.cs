using System.Collections;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [Header("������ ���� ������� \n������������� �����������")]
    
    [Header("������ ��������, ������� ����� ������������")]
    [SerializeField] private GameObject[] _iActivatedComponents;

    [Header("�������� ���������")]
    [SerializeField] private float _activationDelay;

    [Header("������ �������� ����������")]
    [SerializeField] private float _cooldownDelay;

    [Header("��������� ������������ �������������?")]
    [SerializeField] private bool _isOneTimeUse;

    //private bool _isReady = true;

    private IActivatable[] _activatables;

    private void Awake()
    {
        GrabActivatables();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
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
        yield return new WaitForSeconds(_activationDelay);
        foreach (var activatable in _activatables)
        {
            activatable.Activate();
        }
        if (_isOneTimeUse)
        {
            TurnOffActivator();
        }
    }

    private void TurnOffActivator()
    {
        gameObject.SetActive(false);
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
                Debug.LogError("����������� ������ �� ����� ���� �����������. ������ ������ ������");
            }
        }
    }
}
