using System.Collections;
using UnityEngine;

public class BreakDoor : MonoBehaviour
{
    [SerializeField] private AnimationClip _animKick;
    [SerializeField] private AbilityStealing _abilityStealing;
    [SerializeField] private GameObject _door;
    [SerializeField] private Animator _animatorDoor;
    [SerializeField] private AnimationClip _breakDoor;

    private float _timeAnimKick;
    private bool _canDestroyDoor;

    private void Start()
    {
        _timeAnimKick = _animKick.length / 2;
    }

    private void Update()
    {
        if (_canDestroyDoor && Input.GetKeyDown(KeyCode.R) && _abilityStealing.IsStartTimerBreakDoors)
        {
            StartCoroutine(IToBreakDoor());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>())
        {
            _canDestroyDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInput>())
        {
            _canDestroyDoor = false;
        }
    }

    private IEnumerator IToBreakDoor()
    {
        yield return new WaitForSeconds(_timeAnimKick);
        _animatorDoor.SetTrigger("Break");
        yield return new WaitForSeconds(_breakDoor.length);
        Destroy(_door);
    }
}