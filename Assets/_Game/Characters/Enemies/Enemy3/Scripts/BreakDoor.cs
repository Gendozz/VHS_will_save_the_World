using UnityEngine;

public class BreakDoor : MonoBehaviour
{
    [SerializeField] private AbilityStealing _abilityStealing;
    [SerializeField] private GameObject _door;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.R) && _abilityStealing.IsStartTimerBreakDoors && other.GetComponent<PlayerInput>())
        {
            Destroy(_door);
        }
    }
}