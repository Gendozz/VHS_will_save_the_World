using UnityEngine;

public class GameFlowController : MonoBehaviour
{

    [SerializeField] private Health _playerHealth;

    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private PlayerModelRotation _playerModelRotation;

    [SerializeField] private UIMenuController _menuController;

    private bool _isGamePaused = false;

    private void OnEnable()
    {
        _playerHealth.onOutOfLifes += DoAfterPlayerDieActions;
        //_menuController.onPauseCanvasSwitchedOff += SwitchPauseState;
    }

    private void OnDisable()
    {
        _playerHealth.onOutOfLifes -= DoAfterPlayerDieActions;
        //_menuController.onPauseCanvasSwitchedOff -= SwitchPauseState;

    }

    private void Update()
    {
        if (Input.GetButtonDown(StringConsts.ESC))
        {
            SwitchPauseState();
        }
    }

    private void DoAfterPlayerDieActions()
    {
        _isGamePaused = true;
        _menuController.ShowFailCanvas();
        SwitchMotions(!_isGamePaused);
    }

    public void SwitchPauseState()
    {
        _isGamePaused = !_isGamePaused;
        _menuController.SwitchPauseCanvas();
        SwitchMotions(!_isGamePaused);
    }

    private void SwitchMotions(bool needMotions)
    {
        _playerInput.SwitchInput(needMotions);
        _playerModelRotation.enabled = needMotions;
    }
}
