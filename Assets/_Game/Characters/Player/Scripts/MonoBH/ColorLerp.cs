using System.Collections;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    [Header("Цвет, на который меняется")]
    [SerializeField] private Color _colorToLerpTo;
    
    [Header("Продолжительность мигания")]
    [SerializeField] private float _animationDuration;

    [Header("Количество повторений")]
    [SerializeField] private int _amountOfItereations;

    [Header("Ссылка на рендерер")]
    [SerializeField] private Renderer _meshRenderer;

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

    public void StartLerp(float lerpDuration)
    {
        if (_lerpRoutine != null)
        {
            StopCoroutine(_lerpRoutine);
        }
        _animationDuration = lerpDuration;
        _lerpRoutine = StartCoroutine(LerpColor());
    }

    private IEnumerator LerpColor()
    {
        float elapsedTime;

        float halfTimeDuration = (_animationDuration / _amountOfItereations) / 2;

        for (int i = 0; i < _amountOfItereations; i++)
        {
            elapsedTime = 0;
            while (elapsedTime < halfTimeDuration)
            {
                elapsedTime += Time.deltaTime;
                _meshRenderer.material.color = Color.Lerp(_startColor, _colorToLerpTo, elapsedTime / halfTimeDuration);
                yield return null;
            }
            Debug.Log("Lerped To color " + _meshRenderer.material.color);

            elapsedTime = 0;

            while (elapsedTime < halfTimeDuration)
            {
                elapsedTime += Time.deltaTime;
                _meshRenderer.material.color = Color.Lerp(_colorToLerpTo, _startColor, elapsedTime / halfTimeDuration);
                yield return null;
            }

            Debug.Log("Lerped To color " + _meshRenderer.material.color);
        }


    }


}
