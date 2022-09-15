using UnityEngine;

public class Enemy3AgroInteractionWithPlayer : MonoBehaviour
{
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Enemy3LookAtPlayer _enemy3LookAtPlayer;
    [SerializeField] private AbilityStealing _abilityStealing;
    [SerializeField] private Animator _animator;
    [SerializeField] private Behaviour[] _components;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() != null)
        {
            if (_enemy3LookAtPlayer.IsSees)
            {
                _abilityStealing.StartTimerDoubleJump();
                _animator.SetTrigger("Death");

                for (int i = 0; i < _components.Length; i++)
                {
                    _components[i].enabled = false;
                }
            }
        }
    }
}
