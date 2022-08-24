using System;
using System.Collections;
using UnityEngine;

public class MoveBetweenTwoDots : MonoBehaviour, IMovingPlatform, IActivatable
{
    [Header("Двигаемый объект")]
    [SerializeField] private Transform _transformToMove;

    [Header("Точки, между которыми движется пивот объекта")]
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    [Header("Время полного пути между точками")]
    [Range(0,10)]
    [SerializeField] private float _moveDuration;

    [Header("На сколько объект задерживается при достижении точки")]
    [SerializeField] private float _delayWhenPointReached;

    [Header("Активировать при старте?")]
    [SerializeField] private bool _toActivateOnStart = true;

    [Header("Ограничить количество повторений движения?")]
    [SerializeField] private bool _toLimitAmountOfIterations;

    [Header("Количество движений в 1 сторону (если включено ограничение)")]
    [SerializeField] private int _amountOfItereations;

    private int _currentAmountOfIterations;

    private bool _isActive;

    private void Start()
    {
        if (_toActivateOnStart)
        {
            StartCoroutine(Move());
        }        
    }

    private IEnumerator Move()
    {
        _isActive = true;
        Vector3 currentStart = _firstPoint.position;
        Vector3 currentFinish = _secondPoint.position;
        _currentAmountOfIterations = _amountOfItereations;

        while (true)
        {
            float estimatedTime = 0;

            while (estimatedTime < _moveDuration)
            {
                estimatedTime += Time.deltaTime;

                _transformToMove.position = Vector3.Lerp(currentStart, currentFinish, estimatedTime / _moveDuration);

                yield return null;
            }

            Vector3 temp = currentStart;
            currentStart = currentFinish;
            currentFinish = temp;

            
            if (_toLimitAmountOfIterations)
            {
                _currentAmountOfIterations--;
            }

            if(_currentAmountOfIterations <= 0)
            {
                _isActive = false;
                yield break;
            }
            yield return new WaitForSeconds(_delayWhenPointReached);

        }       
    }

    public void Activate()
    {
        if (!_isActive)
        {
            StartCoroutine(Move()); 
        }
    }
}
