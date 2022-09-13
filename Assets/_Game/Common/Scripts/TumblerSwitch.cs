using System.Collections;
using UnityEngine;

public class TumblerSwitch : MonoBehaviour
{
    [SerializeField] private Transform _switcherTransform;

    [Header("Положение тумблера")]
    [SerializeField] private bool _isOn;

    [Range(0, 90)]
    [SerializeField] private float _deflectionAngleWhenOff;

    [SerializeField] private float _switchingDuration;

    [SerializeField] private GameObject _lightOff;

    [SerializeField] private GameObject _lightOn;

    [SerializeField] private Activator _activator;

    private float _deflectionAngleWhenOn;

    private void OnEnable()
    {
        _activator.onActivate += SwitchTumbler;
        _activator.onActivatorRestored += SwitchTumbler;
    }

    private void OnDisable()
    {
        _activator.onActivate -= SwitchTumbler;
        _activator.onActivatorRestored -= SwitchTumbler;
    }


    private void Start()
    {
        _deflectionAngleWhenOn = _deflectionAngleWhenOff * -1;
        SwitchTumblerInstantly();
    }

    private void SwitchTumblerInstantly()
    {
        _switcherTransform.rotation = Quaternion.Euler(_switcherTransform.rotation.x, _switcherTransform.rotation.y, _isOn ? _deflectionAngleWhenOn : _deflectionAngleWhenOff);
        _lightOn.SetActive(_isOn);
        _lightOff.SetActive(!_isOn);
        _isOn = !_isOn;

    }

    public void SwitchTumbler()
    {
        StartCoroutine(Switch());
    }

    private IEnumerator Switch()
    {
        float estimatedTime = 0;

        Quaternion startRotation = _switcherTransform.rotation;
        Quaternion endRotation = Quaternion.Euler(_switcherTransform.rotation.x, _switcherTransform.rotation.y, _isOn ? _deflectionAngleWhenOn : _deflectionAngleWhenOff);

        while (estimatedTime < _switchingDuration)
        {
            estimatedTime += Time.deltaTime;
            _switcherTransform.rotation = Quaternion.Lerp(startRotation, endRotation, estimatedTime / _switchingDuration);
            yield return null;
        }        
        _lightOn.SetActive(_isOn);
        _lightOff.SetActive(!_isOn);
        _isOn = !_isOn;
    }

}

// -45 z - isOn
// +45 z - isOFF