using UnityEngine;

public class Enemy1InteractionWithPlayer : MonoBehaviour
{
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private GameObject _enemy1;
    [SerializeField] private AbilityStealing _abilityStealing;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbodyEnemy1;
    [SerializeField] private Behaviour[] _scripts;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() != null)
        {
            _animator.SetTrigger("Death");
            _abilityStealing.StartTimerDoubleJump();

            for (int i = 0; i < _scripts.Length; i++)
            {
                _scripts[i].enabled = false;
            }

            _rigidbodyEnemy1.isKinematic = true;

            gameObject.SetActive(false);
        }
    }
}
