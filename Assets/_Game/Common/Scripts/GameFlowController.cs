using UnityEngine;

public class GameFlowController : MonoBehaviour
{

    [SerializeField] private Health _playerHealth;

    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private PlayerModelRotation _playerModelRotation;

    private bool _gameIsPaused;

    private void OnEnable()
    {
        _playerHealth.onOutOfLifes += DoAfterPlayerDieActions;
    }

    private void OnDisable()
    {
        _playerHealth.onOutOfLifes -= DoAfterPlayerDieActions;
    }

    private void Update()
    {
        if (Input.GetButtonDown(StringConsts.ESC))
        {

        }
    }

    private void DoAfterPlayerDieActions()
    {
        _playerInput.SwitchInput(false);
        _playerModelRotation.enabled = false;
        _gameIsPaused = true;
    }

    private void ShowPauseMenu()
    {

    }


}
