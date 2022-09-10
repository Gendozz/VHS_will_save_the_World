using System.Collections;
using UnityEngine;

public class Enemy3IdleInteractionWithPlayer : MonoBehaviour
{
    [Header("-----      ���������      -----")]
    [Header("���� ��������")]
    [SerializeField] private float _vertically;
    [SerializeField] private float _horizontally;
    [Space]
    [Header("-----      ���������� � ���������      -----")]
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
                if (other.transform.position.x > transform.position.x)
                {
                    other.GetComponent<Rigidbody>().AddForce(new Vector3(_horizontally, _vertically, 0), ForceMode.Impulse);
                }
                else
                {
                    other.GetComponent<Rigidbody>().AddForce(new Vector3(-_horizontally, _vertically, 0), ForceMode.Impulse);
                }
            }
            else
            {
                _abilityStealing.StartTimerBreakingDoors();
                _animator.SetTrigger("Death");

                _enemy3LookAtPlayer.StopAllCor();

                for (int i = 0; i < _components.Length; i++)
                {
                    _components[i].enabled = false;
                }
            }
        }
    }
}
