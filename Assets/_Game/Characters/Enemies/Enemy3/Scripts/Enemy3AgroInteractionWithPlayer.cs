using UnityEngine;

public class Enemy3AgroInteractionWithPlayer : MonoBehaviour
{
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private GameObject _idleCollider;
    [SerializeField] private ColliderResizing _colliderResizing;
    [SerializeField] private Enemy3LookAtPlayer _enemy3LookAtPlayer;
    [SerializeField] private AbilityStealing _abilityStealing;
    [SerializeField] private Animator _animator;
    [SerializeField] private Behaviour[] _components;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() != null && _enemy3LookAtPlayer.IsSees)
        {
            _abilityStealing.StartTimerBreakingDoors();
            _animator.SetTrigger("Death");

            for (int i = 0; i < _components.Length; i++)
            {
                _components[i].enabled = false;
            }

            _colliderResizing.ReductionSizeCollider();

            _enemy3LookAtPlayer.StopAllCor();

            gameObject.SetActive(false);
            _idleCollider.SetActive(false);
        }
    }
}
