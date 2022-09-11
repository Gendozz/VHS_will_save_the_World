using System;
using System.Collections;
using UnityEngine;

public class LerpAlpha : MonoBehaviour
{
    [SerializeField] private MeshRenderer _planeRenderer;

    [Range(0, 1)]
    [SerializeField] private float _requiredAlpha;

    [SerializeField] private float _changeAlphaDuration;

     private Color _colorOnStart;
    


    private void OnEnable()
    {
        _colorOnStart = _planeRenderer.material.color;
        //StartChangingAlpha();
        
    }

    public void StartChangingAlpha()
    {
        //_colorOnStart = _planeRenderer.material.color;
        StartCoroutine(ChangeAlpha());
    }

    private IEnumerator ChangeAlpha()
    {
        float estimatedTime = 0;

        Color requiredColor = new Color(_colorOnStart.r, _colorOnStart.g, _colorOnStart.b, _requiredAlpha);

        while(estimatedTime < _changeAlphaDuration)
        {
            estimatedTime += Time.deltaTime;

            _planeRenderer.material.color = Color.Lerp(_colorOnStart, requiredColor, estimatedTime / _changeAlphaDuration);

            yield return null;
        }
    }
}
