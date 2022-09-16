using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(BoxCollider))] 
public class StartTimeLineOnUserPressQ : MonoBehaviour
{
    [SerializeField] private PlayableDirector _flybyCamera;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _flybyCamera.Play();
            }
        }
    }
}
