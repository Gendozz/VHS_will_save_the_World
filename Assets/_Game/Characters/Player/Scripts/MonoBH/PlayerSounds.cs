using System;
using JSAM;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private Health _playerHealth;

    private bool _isJumping;

    private void OnEnable()
    {
        _playerHealth.onTakeDamage += TakeDamageSound;
        _playerHealth.onOutOfLifes += DeathSound;
    }

    private void OnDisable()
    {
        _playerHealth.onTakeDamage -= TakeDamageSound;
        _playerHealth.onOutOfLifes -= DeathSound;
    }

    private void Update()
    {
        ApplyMovementSounds();
    }

    private void ApplyMovementSounds()
    {
        bool isPlayerGrounded = _playerMovement.IsGrounded;
        bool isPlayerMoving = _playerMovement.ShouldApplyHorizontalMovement();
        bool isPlayerIsOnWall = _playerMovement.IsOnWall;

        if (isPlayerGrounded)
        {
            if (isPlayerMoving)
            {
                ApplyHorizontalMovementSounds();
            }
            // else if(AudioManager.IsSoundPlaying(Sounds.NormalSteps) && _playerInput.isHorizontalInput)
            // {
            //     Debug.Log("isGrounded - true, isPlayerMoving - true, normalSteps is Playing");
            //     AudioManager.StopSound(Sounds.NormalSteps);
            // }

        }

        if ((_playerInput.IsJumpButtonPressed && (isPlayerGrounded || isPlayerIsOnWall)) || _playerMovement.IsOnTrampoline ||
            (_playerInput.IsJumpButtonPressed && _playerMovement.IsInTrampolineTrigger))
        {
            AudioManager.PlaySound(Sounds.Jump);
        }
        
        
    }

    private void ApplyHorizontalMovementSounds()
    {
        if (!_playerMovement.IsOnSlippery)
        {
            if (!AudioManager.IsSoundPlaying(Sounds.NormalSteps))
            {
                AudioManager.PlaySound(Sounds.NormalSteps);
            }
        }
        else
        {
            if (!AudioManager.IsSoundPlaying(Sounds.SlipperySteps))
            {
                AudioManager.PlaySound(Sounds.SlipperySteps);
            }
        }
    }

    private void TakeDamageSound()
    {
        AudioManager.PlaySound(Sounds.TakeDamage);
    }

    private void DeathSound()
    {
        AudioManager.PlaySound(Sounds.Death);
    }
}