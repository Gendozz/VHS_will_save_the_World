using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ChangeTextColorWhenHovered : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TMP_Text _text;

    private Color _colorHover = new Color(0.075f, 0.090f, 0.294f, 1);

    private Color _colorStill = new Color(0.976f, 0.000f, 0.643f, 1);


    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = _colorHover;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = _colorStill;
    }

    public void AddTMP_Text(TMP_Text tMP_TextToAdd)
    {
        _text = tMP_TextToAdd;        
    }
}
