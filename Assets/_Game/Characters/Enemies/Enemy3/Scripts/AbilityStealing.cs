using System;
using System.Collections;
using UnityEngine;

public class AbilityStealing : MonoBehaviour
{
    [HideInInspector] public bool IsStartTimerBreakDoors = false;

    [SerializeField] private float _timeDoubleJump;
    [SerializeField] private float _timeBreakingDoors;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private UIAbility _uiAbility;

    public Action onStartBreakDoorAbility;
    public Action onEndBreakDoorAbility;

    public void StartTimerDoubleJump()
    {
        _uiAbility.TimerDisplayDoubleJump(_timeDoubleJump);
        _playerMovement.SetDoubleJumpAbility(true);
        StopAllCoroutines();
        StartCoroutine(TimerDoubleJump());
    }

    private IEnumerator TimerDoubleJump()
    {
        yield return new WaitForSeconds(_timeDoubleJump);
        _playerMovement.SetDoubleJumpAbility(false);
    }

    public void StartTimerBreakingDoors()
    {
        _uiAbility.TimerDisplayBreakingDoors(_timeBreakingDoors);
        IsStartTimerBreakDoors = true;
        onStartBreakDoorAbility?.Invoke();
        StopAllCoroutines();
        StartCoroutine(TimerBreakingDoors());
    }

    private IEnumerator TimerBreakingDoors()
    {
        yield return new WaitForSeconds(_timeBreakingDoors);
        IsStartTimerBreakDoors = false;
        onEndBreakDoorAbility?.Invoke();
    }
}
