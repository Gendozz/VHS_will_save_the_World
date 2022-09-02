using System;
using System.Collections;
using UnityEngine;

public class MoveBetweenTwoDotsDifSpeed : MonoBehaviour, IMovingPlatform, IActivatable
{
    [Header("��������� ������")]
    [SerializeField] private Transform _transformToMove;

    [Header("�����, ����� �������� �������� ����� �������")]
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    [Header("����� ������� ���� ����� �������")]
    [Range(0, 10)]
    [SerializeField] private float _moveDuration;

    [Header("�� ������� ������ ������������� ��� ���������� �����")]
    [SerializeField] private float _delayWhenPointReached;

    [Header("������������ ��� ������?")]
    [SerializeField] private bool _toActivateOnStart = true;

    [Header("���������� ���������� ���������� ��������?")]
    [SerializeField] private bool _toLimitAmountOfIterations;

    [Header("���������� �������� � 1 ������� (���� �������� �����������)")]
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

                _transformToMove.position = Vector3.Lerp(currentStart, currentFinish, EaseInOutQuart(estimatedTime / _moveDuration));

                yield return null;
            }

            Vector3 temp = currentStart;
            currentStart = currentFinish;
            currentFinish = temp;

            if (_toLimitAmountOfIterations)
            {
                _currentAmountOfIterations--;
                if (_currentAmountOfIterations <= 0)
                {
                    _isActive = false;
                    yield break;
                }
            }

            yield return new WaitForSeconds(_delayWhenPointReached);
        }
    }

    private float EaseIn(float t)
    {
        return t * t;
    }

    private float Flip(float x)
    {
        return 1 - x;
    }

    private float EaseOut(float t)
    {
        return Flip(Mathf.Pow(Flip(t), 2));
    }


    private float EaseInOut(float t)
    {
        return Mathf.Lerp(EaseIn(t), EaseOut(t), t);
    }

    private float EaseInOutQuart(float x)
    {
        return x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
    }

    public void Activate()
    {
        if (!_isActive)
        {
            StartCoroutine(Move());
        }
    }
}
