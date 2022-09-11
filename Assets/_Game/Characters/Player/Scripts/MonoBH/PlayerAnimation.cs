using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private float _rotationDuration;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private Health _playerHealth;

    [SerializeField] private ColorLerp _colorLerp;

    private bool _isJumping = false;

    private float _kickAnimationDuration;


    private void OnEnable()
    {
        _playerHealth.onTakeDamage += AnimateDamage;
        _playerHealth.onOutOfLifes += AnimateDeath;
    }

    private void AnimateDeath()
    {
        _animator.SetTrigger("isDead");
    }

    private void OnDisable()
    {
        _playerHealth.onTakeDamage -= AnimateDamage;
        _playerHealth.onOutOfLifes -= AnimateDeath;

    }

    private void Start()
    {
        RuntimeAnimatorController runtimeAnimatorController = _animator.runtimeAnimatorController;
        foreach (var clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name.Equals("Kick"))
            {
                _kickAnimationDuration = clip.length;
            }
        }
    }

    private void Update()
    {
        ApplyKickAnimation();

        ApplyMovementAnimations();
    }

    private void ApplyKickAnimation()
    {
        if (_playerInput.IsAttackButtonPressed)
        {
            _animator.SetTrigger("Kick");
        }
    }


    private void ApplyMovementAnimations()
    {
        if (_playerMovement.IsGrounded)
        {
            _animator.SetBool("isGrounded", true);
            _animator.SetBool("isFalling", false);
            _animator.SetBool("isJumping", false);
            _isJumping = false;
            if (_playerInput.IsJumpButtonPressed)
            {
                _animator.SetBool("isJumping", true);
                _isJumping = true;
            }

            if (_playerMovement.ShouldApplyHorizontalMovement())
            {
                _animator.SetBool("isMoving", true);
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }
        }
        else
        {
            _animator.SetBool("isGrounded", false);

            if (_playerMovement.IsOnWall)
            {
                _animator.SetBool("isOnWall", true);
                _animator.SetBool("isFalling", false);

                if (_playerInput.IsJumpButtonPressed)
                {
                    _animator.SetBool("isJumping", true);
                }
            }
            else
            {
                _animator.SetBool("isOnWall", false);
            }
        }

        if (_isJumping && _playerMovement.VelocityY < 0 || _playerMovement.VelocityY < -2 && !_playerMovement.IsOnWall)
        {
            _animator.SetBool("isJumping", false);
            _isJumping = false;
            _animator.SetBool("isFalling", true);
        }
    }


    private void AnimateDamage()
    {
        _colorLerp.StartLerp();
    }
}
