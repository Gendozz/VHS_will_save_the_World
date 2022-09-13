using System.Collections;
using UnityEngine;

public class LerpLight : MonoBehaviour
{
    [SerializeField] private Light _light;

    [SerializeField] private float _lerpDuration;

    [SerializeField] private float _requiredIntensity;
    
    private float _startIntensity;

    private void Awake()
    {
        _startIntensity = _light.intensity;
    }

    public void StartLerp()
    {
        StartCoroutine(LightLerp());
    }

    private IEnumerator LightLerp()
    {
        float elapsedTime = 0;

        while(elapsedTime < _lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            _light.intensity = Mathf.Lerp(_startIntensity, _requiredIntensity, elapsedTime / _lerpDuration);
            yield return null;
        }
    }
}
