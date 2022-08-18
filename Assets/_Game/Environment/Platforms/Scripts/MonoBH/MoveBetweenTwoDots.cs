using System;
using System.Collections;
using UnityEngine;

public class MoveBetweenTwoDots : MonoBehaviour, IMovingPlatform
{
    [Header("Двигаемый объект")]
    [SerializeField] private Transform transformToMove;

    [Header("Точки, между которыми движется пивот объекта")]
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;

    [Header("Время полного пути между точками")]
    [Range(0,10)]
    [SerializeField] private float moveDuration;

    [Header("На сколько объект задерживается при достижении точки")]
    [SerializeField] private float delayWhenPointReached;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 currentStart = firstPoint.position;
        Vector3 currentFinish = secondPoint.position;

        while (true)
        {
            float estimatedTime = 0;

            while (estimatedTime < moveDuration)
            {
                estimatedTime += Time.deltaTime;

                transformToMove.position = Vector3.Lerp(currentStart, currentFinish, estimatedTime / moveDuration);

                yield return null;
            }

            Vector3 temp = currentStart;
            currentStart = currentFinish;
            currentFinish = temp;

            yield return new WaitForSeconds(delayWhenPointReached);
        }       
    }
}
