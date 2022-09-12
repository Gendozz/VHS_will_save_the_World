using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    [Header("������ �� Health ������")]
    [SerializeField] private Health _playerHealth;

    [Header("��� ����� ������ � ������")]
    [Header("������ �� ��� PlayerInput (����� � ��� �����)")]
    [SerializeField] private PlayerInput[] _playerInputs;
    
    [Header("������ �� ��� PlayerModelRotation (����� � ��� �����)")]
    [SerializeField] private PlayerModelRotation[] _playerModelRotations;

    [Header("������ �� ��� PlayerMovement (����� � ��� �����)")]
    [SerializeField] private PlayerMovement[] _playerMovements;

    [Header("��� ����� Enemy3")]
    [Header("������ �� ��� Enemy3LookAtPlayer")]
    [SerializeField] private Enemy3LookAtPlayer[] _enemy3LookAtPlayer;

    [Header("������ �� ��� Enemy3AttackAoeDisplay")]
    [SerializeField] private Enemy3AttackAoeDisplay[] _enemy3AttackAoeDisplay;


    [SerializeField] private UIMenuController _menuController;

    private bool _isGamePaused = false;

    private void OnEnable()
    {
        _playerHealth.onOutOfLifes += DoAfterPlayerDieActions;
        LevelEnd.onPlyerGotToLevelEnd += DoPlayerWinActions;
    }

    private void OnDisable()
    {
        _playerHealth.onOutOfLifes -= DoAfterPlayerDieActions;
        LevelEnd.onPlyerGotToLevelEnd -= DoPlayerWinActions;
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

    private void DoPlayerWinActions()
    {
        _isGamePaused = true;
        _menuController.ShowWinCanvas();
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
        for (int i = 0; i < _playerInputs.Length; i++)
        {
            _playerInputs[i].enabled = needMotions;
            _playerModelRotations[i].enabled = needMotions;
            _playerMovements[i].PauseUnpauseActions(!needMotions);
        }

        for (int i = 0; i < _enemy3LookAtPlayer.Length; i++)
        {
            _enemy3LookAtPlayer[i].enabled = needMotions;
            _enemy3AttackAoeDisplay[i].enabled = needMotions;
        }
    }
}
