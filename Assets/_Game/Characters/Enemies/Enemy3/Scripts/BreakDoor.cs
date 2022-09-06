using UnityEngine;

public class BreakDoor : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    private bool _isHaveAbility;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.R) && _isHaveAbility && other.GetComponent<PlayerInput>())
        {
            Destroy(_door);
        }
    }

    public void GetAbiluty(bool trigger)
    {
        _isHaveAbility = trigger;
    }
}