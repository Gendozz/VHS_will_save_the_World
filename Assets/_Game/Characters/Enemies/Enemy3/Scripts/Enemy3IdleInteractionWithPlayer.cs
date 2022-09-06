using UnityEngine;

public class Enemy3IdleInteractionWithPlayer : MonoBehaviour
{
    [Header("-----      Настройки      -----")]
    [Header("Сила импульса")]
    [SerializeField] private float _vertically;
    [SerializeField] private float _horizontally;
    [Space]
    [Header("-----      Компоненты и системные      -----")]
    [SerializeField] private Enemy3LookAtPlayer _enemy3LookAtPlayer;
    [SerializeField] private GameObject _enemy3Objs;
    [SerializeField] private AbilityStealing _abilityStealing;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerInput>() != null)
        {
            if (_enemy3LookAtPlayer.IsSees)
            {
                if (collision.collider.transform.position.x > transform.position.x)
                {
                    collision.collider.GetComponent<Rigidbody>().AddForce(new Vector3(_horizontally, _vertically, 0), ForceMode.Impulse);
                }
                else
                {
                    collision.collider.GetComponent<Rigidbody>().AddForce(new Vector3(-_horizontally, _vertically, 0), ForceMode.Impulse);
                }
            }
            else
            {
                _abilityStealing.StartTimerBreakingDoors();
                Destroy(_enemy3Objs);
            }
        }
    }
}
