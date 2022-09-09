using System.Collections;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    [Header("Цвет, на который меняется")]
    [SerializeField] private Color _colorToLerpTo;
    
    [Header("Продолжительность мигания")]
    [SerializeField] private float _lerpDuration;

    [Header("Количество повторений")]
    [SerializeField] private int _amountOfItereations;

    [Header("Ссылка на рендерер")]
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private Coroutine _lerpRoutine;

    private Color _startColor;

    private void Awake()
    {
        _startColor = _meshRenderer.material.color;
    }

    public void StartLerp()
    {
        if (_lerpRoutine != null)
        {
            StopCoroutine(_lerpRoutine);
        }
        _lerpRoutine = StartCoroutine(LerpColor());
    }

    private IEnumerator LerpColor()
    {
        float elapsedTime;

        for (int i = 0; i < _amountOfItereations; i++)
        {
            elapsedTime = 0;
            while (elapsedTime < _lerpDuration)
            {
                elapsedTime += Time.deltaTime;
                _meshRenderer.material.color = Color.Lerp(_startColor, _colorToLerpTo, elapsedTime / _lerpDuration);
                yield return null;
            }

            elapsedTime = 0;

            while (elapsedTime < _lerpDuration)
            {
                elapsedTime += Time.deltaTime;
                _meshRenderer.material.color = Color.Lerp(_colorToLerpTo, _startColor, elapsedTime / _lerpDuration);
                yield return null;
            } 
        }


    }


}
