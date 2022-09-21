using UnityEngine;

public class ResurrectionEnemy3 : MonoBehaviour
{
    [Header("-----      Настройки      -----")]
    [SerializeField] private float _timeUntilRes;
    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Enemy3LookAtPlayer _enemy3LookAtPlayer;
    [SerializeField] private ColliderResizing _colliderResizingE3;
    [SerializeField] private Animator _animatorE3;
    [SerializeField] private GameObject _idleColliderE3;
    [SerializeField] private GameObject _agroColliderE3;
    [SerializeField] private Behaviour[] _componentsE3;

    private float _timerRes = 0;

    private void Update()
    {
        if (_componentsE3[0].enabled == false)
        {
            if (_timerRes >= _timeUntilRes)
            {
                _animatorE3.SetTrigger("Res");

                for (int i = 0; i < _componentsE3.Length; i++)
                {
                    _componentsE3[i].enabled = true;
                }
                _colliderResizingE3.IncreaseSizeCollider();

                _idleColliderE3.SetActive(true);
                _agroColliderE3.SetActive(true);

                _enemy3LookAtPlayer.ResurectionFromIdle();

                _timerRes = 0;
            }

            _timerRes += Time.deltaTime;
        }
    }
}
