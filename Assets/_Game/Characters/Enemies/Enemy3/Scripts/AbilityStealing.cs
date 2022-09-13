using System;
using UnityEngine;

public class AbilityStealing : MonoBehaviour
{
    [HideInInspector] public bool IsStartTimerBreakDoors = false;

    [SerializeField] private float _timeDoubleJump;
    [SerializeField] private float _timeBreakingDoors;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _timer = 0;
    private bool _isStartTimerJump = false;

    public Action onStartBreakDoorAbility;
    public Action onEndBreakDoorAbility;

    private void Update()
    {
        if (_isStartTimerJump)
        {
            if (_timer > _timeDoubleJump)
            {
                _isStartTimerJump = false;
                _playerMovement.SetDoubleJumpAbility(false);
            }
            _timer += Time.deltaTime;
        }

        if (IsStartTimerBreakDoors)
        {
            if (_timer > _timeBreakingDoors)
            {
                IsStartTimerBreakDoors = false;
                onEndBreakDoorAbility?.Invoke();
            }
            _timer += Time.deltaTime;
        }
    }

    public void StartTimerDoubleJump()
    {
        _timer = 0;
        _playerMovement.SetDoubleJumpAbility(true);
        _isStartTimerJump = true;
        IsStartTimerBreakDoors = false;
    }

    public void StartTimerBreakingDoors()
    {
        _timer = 0;
        IsStartTimerBreakDoors = true;
        _isStartTimerJump = false;
        onStartBreakDoorAbility?.Invoke();
    }
}
