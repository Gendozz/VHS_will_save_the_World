using UnityEngine;

public class Enemy3AgroInteractionWithPlayer : MonoBehaviour
{
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Enemy3LookAtPlayer _enemy3LookAtPlayer;
    [SerializeField] private GameObject _enemy3Objs;
    [SerializeField] private AbilityStealing _abilityStealing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() != null)
        {
            if (_enemy3LookAtPlayer.IsSees)
            {
                _abilityStealing.StartTimerBreakingDoors();
                Destroy(_enemy3Objs);
            }
        }
    }
}
