using UnityEngine;

public class Enemy1InteractionWithPlayer : MonoBehaviour
{
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private GameObject _enemy1;
    [SerializeField] private AbilityStealing _abilityStealing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() != null)
        {
            _abilityStealing.StartTimerDoubleJump();
            Destroy(_enemy1);
        }
    }
}
