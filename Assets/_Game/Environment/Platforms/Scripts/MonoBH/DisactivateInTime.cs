using System.Collections;
using UnityEngine;

public class DisactivateInTime : MonoBehaviour
{
    [Header("Объект, который дезактивируется")]
    [SerializeField] private GameObject _objectToDisactivate;

    [Header("Задержка после активации перед дезактивацией")]
    [SerializeField] private float _delayBeforeDisactivate;

    private void OnEnable()
    {
        StartCoroutine(Disactivate());
    }

    private IEnumerator Disactivate()
    {
        yield return new WaitForSeconds(_delayBeforeDisactivate);
        _objectToDisactivate.SetActive(false);
    }


}
