using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ShowTextOnPlayerEnterTrigger : MonoBehaviour
{
    [Header("Объект с текстом")]
    [SerializeField] private GameObject _textObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _textObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _textObject.SetActive(false);
        }
    }
}
