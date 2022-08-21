using System;
using System.Collections;
using UnityEngine;

public class MoveBetweenTwoDots : MonoBehaviour, IMovingPlatform, IActivated
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

    private bool isActive;

    private void Start()
    {
        if (_toActivateOnStart)
        {
            StartCoroutine(Move());
        }
        
    }

    private IEnumerator Move()
    {
        Vector3 currentStart = _firstPoint.position;
        Vector3 currentFinish = _secondPoint.position;

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

            yield return new WaitForSeconds(_delayWhenPointReached);
        }       
    }

    public void Activate()
    {
        StartCoroutine(Move());
    }
}
