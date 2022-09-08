using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private float _rotationDuration;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private Health _playerHealth;

    private bool isJumping = false;

    private void Update()
    {
        ApplyInputToAnimator();

        WatchHealth();
    }

    private void WatchHealth()
    {
        if (_playerHealth.IsOutOfLifes)
        {
            _animator.SetTrigger("isDead");
        }
    }

    private void ApplyInputToAnimator()
    {
        if (_playerMovement.IsGrounded)
        {
            _animator.SetBool("isGrounded", true);
            _animator.SetBool("isFalling", false);
            _animator.SetBool("isJumping", false);
            isJumping = false;
            if (_playerInput.IsJumpButtonPressed)
            {
                _animator.SetBool("isJumping", true);
                isJumping = true;
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
                    //_animator.SetBool("isOnWall", false);
                    _animator.SetBool("isJumping", true);
                }

            }
            else
            {
                _animator.SetBool("isOnWall", false);
            }




        }

        if (isJumping && _playerMovement.VelocityY < 0 || _playerMovement.VelocityY < -2 && !_playerMovement.IsOnWall)
        {
            _animator.SetBool("isJumping", false);
            isJumping = false;
            _animator.SetBool("isFalling", true);
        }

    }


    private void FixedUpdate()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position - transform.up * 2);
    }
}
