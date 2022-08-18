using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveBetweenTwoPointRB : MonoBehaviour, IMovingPlatform
{
    [Header("-----      ���������      -----")]
    [Header("�����, ����� �������� �������� ����� �������")]
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    [Header("�������� �����������")]
    [SerializeField] private float _speed;

    [Header("�� ������� ������ ������������� ��� ���������� �����")]
    [SerializeField] private float _delayWhenPointReached;
    
    [Space]
    [Header("-----      ���������� � ���������      -----")]
    [Header("���������� ���� �������")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("����������� �� ���������� �����")]
    [SerializeField] private float _pointReachingDistanceTreshold;

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        _rigidbody.isKinematic = true;
    }

    private void Start()
    {
        StartCoroutine(ToMove()); 
    }

    private IEnumerator ToMove()
    {
        Vector3 currentStart = _firstPoint.position;
        Vector3 currentFinish = _secondPoint.position;

        Vector3 currentDirection = (currentFinish - transform.position).normalized;

        while (true)
        {
            _rigidbody.MovePosition(transform.position + (currentDirection * _speed) * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, currentFinish) < _pointReachingDistanceTreshold)
            {
                Vector3 temp = currentStart;
                currentStart = currentFinish;
                currentFinish = temp;
                currentDirection = (currentFinish - transform.position).normalized;
                yield return new WaitForSeconds(_delayWhenPointReached);

            }
            yield return _waitForFixedUpdate; 
        }
    }
}
