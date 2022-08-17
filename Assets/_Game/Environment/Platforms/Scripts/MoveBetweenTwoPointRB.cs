using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveBetweenTwoPointRB : MonoBehaviour
{
    [Header("-----      Настройки      -----")]
    [Header("Точки, между которыми движется пивот объекта")]
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;

    [Header("Скорость перемещения")]
    [SerializeField] private float speed;

    [Header("На сколько объект задерживается при достижении точки")]
    [SerializeField] private float delayWhenPointReached;
    
    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [Header("Физическое тело объекта")]
    [SerializeField] private Rigidbody rigidbody;

    [Header("Погрешность до достижения точки")]
    [SerializeField] private float pointReachingDistanceTreshold;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        rigidbody.isKinematic = true;
    }

    private void Start()
    {

        StartCoroutine(Move());
        
    }

    private IEnumerator Move()
    {
        Vector3 currentStart = firstPoint.position;
        Vector3 currentFinish = secondPoint.position;

        Vector3 currentDirection = (currentFinish - transform.position).normalized;

        while (true)
        {
            rigidbody.MovePosition(transform.position + (currentDirection * speed) * Time.fixedDeltaTime);


            if (Vector3.Distance(transform.position, currentFinish) < pointReachingDistanceTreshold)
            {
                Vector3 temp = currentStart;
                currentStart = currentFinish;
                currentFinish = temp;
                currentDirection = (currentFinish - transform.position).normalized;
                yield return new WaitForSeconds(delayWhenPointReached);

            }
            yield return waitForFixedUpdate; 
            
        }

    }
}
