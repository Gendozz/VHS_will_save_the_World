using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ChangeTextColorWhenHovered : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TMP_Text _text;

    private Color _colorHover = new Color(19, 23, 75, 1);

    private Color _colorStill = new Color(249, 0, 164, 1);


    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = _colorHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = _colorStill;

        Debug.Log("");
    }
}
