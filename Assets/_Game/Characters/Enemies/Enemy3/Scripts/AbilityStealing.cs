using UnityEngine;

public class AbilityStealing : MonoBehaviour
{
    [SerializeField] private float _timeDoubleJump;
    [SerializeField] private float _timeBreakingDoors;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BreakDoor _breakDoor;

    private float _timer = 0;
    private bool _isStartTimerJump = false;
    private bool _isStartTimerBreakDoors = false;

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

        if (_isStartTimerBreakDoors)
        {
            if (_timer > _timeBreakingDoors)
            {
                _isStartTimerBreakDoors = false;
                _breakDoor.GetAbiluty(false);
            }
            _timer += Time.deltaTime;
        }
    }

    public void StartTimerDoubleJump()
    {
        _timer = 0;
        _playerMovement.SetDoubleJumpAbility(true);
        _isStartTimerJump = true;
        _isStartTimerBreakDoors = false;
    }

    public void StartTimerBreakingDoors()
    {
        _timer = 0;
        _breakDoor.GetAbiluty(true);
        _isStartTimerBreakDoors = true;
        _isStartTimerJump = false;
    }
}
