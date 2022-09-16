using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFlowController : MonoBehaviour
{
    [Header("Ссылка на Health игрока")]
    [SerializeField] private Health _playerHealth;

    [Header("Для паузы игрока и клонов")]
    [Header("Ссылки на все PlayerInput (игрок и все клоны)")]
    [SerializeField] private PlayerInput[] _playerInputs;
    
    [Header("Ссылки на все PlayerModelRotation (игрок и все клоны)")]
    [SerializeField] private PlayerModelRotation[] _playerModelRotations;

    [Header("Ссылки на все PlayerMovement (игрок и все клоны)")]
    [SerializeField] private PlayerMovement[] _playerMovements;

    [Header("Для паузы Enemy3")]
    [Header("Ссылки на все Enemy3LookAtPlayer")]
    [SerializeField] private Enemy3LookAtPlayer[] _enemy3LookAtPlayer;

    [Header("Ссылки на все Enemy3AttackAoeDisplay")]
    [SerializeField] private Enemy3AttackAoeDisplay[] _enemy3AttackAoeDisplay;

    [SerializeField] private UIMenuController _menuController;

    private bool _isGamePaused = false;

    private void OnEnable()
    {
        _playerHealth.onOutOfLifes += DoAfterPlayerDieActions;
        LevelEnd.onPlayerGotToLevelEnd += DoPlayerWinActions;
    }

    private void OnDisable()
    {
        _playerHealth.onOutOfLifes -= DoAfterPlayerDieActions;
        LevelEnd.onPlayerGotToLevelEnd -= DoPlayerWinActions;
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
        for (int i = 0; i < _playerInputs.Length; i++)
        {
            _playerInputs[i].SwitchInput(false);
            _playerModelRotations[i].enabled = false;
        }
    }

    private void DoPlayerWinActions()
    {
        //_isGamePaused = true;
        //_menuController.ShowWinCanvas();
        //SwitchMotions(!_isGamePaused);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
