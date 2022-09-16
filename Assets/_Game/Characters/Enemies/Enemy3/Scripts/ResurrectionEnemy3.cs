using UnityEngine;

public class ResurrectionEnemy3 : MonoBehaviour
{
    [Header("-----      Настройки      -----")]
    [SerializeField] private float _timeUntilRes;
    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Enemy3LookAtPlayer _enemy3LookAtPlayer;
    [SerializeField] private ColliderResizing _colliderResizing;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _idleCollider;
    [SerializeField] private GameObject _agroCollider;
    [SerializeField] private Behaviour[] _components;

    private float _timerRes = 0;

    private void Update()
    {
        if (_components[0].enabled == false)
        {
            if (_timerRes >= _timeUntilRes)
            {
                _animator.SetTrigger("Res");

                for (int i = 0; i < _components.Length; i++)
                {
                    _components[i].enabled = true;
                }
                _colliderResizing.IncreaseSizeCollider();

                _idleCollider.SetActive(true);
                _agroCollider.SetActive(true);

                _enemy3LookAtPlayer.ResurectionFromIdle();

                _timerRes = 0;
            }

            _timerRes += Time.deltaTime;
        }
    }
}
