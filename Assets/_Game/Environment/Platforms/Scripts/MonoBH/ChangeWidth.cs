using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWidth : MonoBehaviour, IMovingPlatform
{
    [Header("-----     ���������     -----")]
    [Header("�������������� ������")]
    [SerializeField] private Transform _transformToScale;

    [Header("����� �� ������ ��������� ������")]
    [SerializeField] private float _changeScaleDuration;

    [Header("���������� � n ��� �� ������� ������")]
    [SerializeField] private float _targetScaleX;

    [Header("����������������� ����� ��� ���������� ���������� ������")]
    [SerializeField] private float _delayWhenTargetScaleReached;

    [Header("-----     ����������     -----")]
    [Header("��������� ���������")] 
    [SerializeField] private BoxCollider _thisCollider;

    private float _startScaleX;

    private float _endScaleX;

    private void Start()
    {
        _startScaleX = _transformToScale.localScale.x;
        _endScaleX = _startScaleX * _targetScaleX;

        StartCoroutine(ChangeScale());
    }

    private IEnumerator ChangeScale()
    {
        float currentStartScaleX = _startScaleX;
        float currentEndScaleX = _endScaleX;

        while (true)
        {
            float estimatedTime = 0;

            while (estimatedTime < _changeScaleDuration)
            {
                estimatedTime += Time.deltaTime;

                float currentXScale = Mathf.Lerp(currentStartScaleX, currentEndScaleX, estimatedTime / _changeScaleDuration);
                _transformToScale.localScale = new Vector3(currentXScale, _transformToScale.localScale.y, _transformToScale.localScale.z);

                _thisCollider.size = new Vector3(currentXScale, _thisCollider.size.y, _thisCollider.size.z);

                yield return null;
            }

            float temp = currentStartScaleX;            
            currentStartScaleX = currentEndScaleX;
            currentEndScaleX = temp;

            yield return new WaitForSeconds(_delayWhenTargetScaleReached);
        }
    }
}
