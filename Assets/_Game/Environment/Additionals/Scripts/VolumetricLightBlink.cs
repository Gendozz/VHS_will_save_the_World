using System.Collections;
using UnityEngine;

public class VolumetricLightBlink : MonoBehaviour
{
    [SerializeField] private VLight _vLight;

    [SerializeField] private float _minBlinkDuration;

    [SerializeField] private float _maxBlinkDuration;

    [SerializeField] private float _minLightDuration;

    [SerializeField] private float _maxLightDuration;

    [SerializeField] private float _onLightStateSpotExponent;

    [SerializeField] private float _onBlinkStateSpotExponent;


    private void Start()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            _vLight.spotExponent = _onLightStateSpotExponent;

            float currentLightInterval = Random.Range(_minLightDuration, _maxLightDuration);
            float estimatedLightDuration = 0;

            while (estimatedLightDuration < currentLightInterval)
            {
                estimatedLightDuration += Time.deltaTime;
                yield return null;
            }

            _vLight.spotExponent = _onBlinkStateSpotExponent;

            float currentBlinkInterval = Random.Range(_minBlinkDuration, _maxBlinkDuration);
            float estimatedBlinkDuration = 0;

            while (estimatedBlinkDuration < currentBlinkInterval)
            {
                estimatedBlinkDuration += Time.deltaTime;
                yield return null;
            }

        }
    }

}
