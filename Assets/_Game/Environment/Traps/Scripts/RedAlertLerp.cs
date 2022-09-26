using System.Collections;
using UnityEngine;

public class RedAlertLerp : MonoBehaviour
{
    [SerializeField] private VLight _vLight;

    [Header("����������������� �������")]
    [SerializeField] private float _animationDuration;

    [Header("���������� ����������")]
    [SerializeField] private int _amountOfItereations;

    private float _startLightMultiplier;

    [SerializeField ] private float _lightMultiplierToLerpTo;


    public void StartLerp()
    {
        StartCoroutine(LerpLightMultiplier());
    }


    private IEnumerator LerpLightMultiplier()
    {
        float elapsedTime;

        float halfTimeDuration = (_animationDuration / _amountOfItereations) / 2;

        for (int i = 0; i < _amountOfItereations; i++)
        {
            elapsedTime = 0;
            while (elapsedTime < halfTimeDuration)
            {
                elapsedTime += Time.deltaTime;
                _vLight.lightMultiplier = Mathf.Lerp(_startLightMultiplier, _lightMultiplierToLerpTo, elapsedTime / halfTimeDuration);
                yield return null;
            }

            elapsedTime = 0;

            while (elapsedTime < halfTimeDuration)
            {
                elapsedTime += Time.deltaTime;
                _vLight.lightMultiplier = Mathf.Lerp(_lightMultiplierToLerpTo, _startLightMultiplier, elapsedTime / halfTimeDuration);
                yield return null;
            }
        }
    }
}
