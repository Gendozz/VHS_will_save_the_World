using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private AbilityStealing _abilityStealing;

    [SerializeField] private float _rotationDuration;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private Health _playerHealth;

    [SerializeField] private ColorLerp _colorLerp;

    private bool _isJumping = false;

    private bool _canKick = false;

    private void OnEnable()
    {
        _playerHealth.onTakeDamage += AnimateDamage;
        _playerHealth.onOutOfLifes += AnimateDeath;
        if (_abilityStealing != null)
        {
            _abilityStealing.onStartBreakDoorAbility += SwitchCanKick;
            _abilityStealing.onEndBreakDoorAbility += SwitchCanKick;
        }
    }



    private void OnDisable()
    {
        _playerHealth.onTakeDamage -= AnimateDamage;
        _playerHealth.onOutOfLifes -= AnimateDeath;
        if (_abilityStealing != null)
        {
            _abilityStealing.onStartBreakDoorAbility -= SwitchCanKick;
            _abilityStealing.onEndBreakDoorAbility -= SwitchCanKick;
        }
    }

    private void AnimateDeath()
    {
        _animator.SetTrigger("isDead");
    }

    private void Update()
    {
        if (_canKick)
        {
            ApplyKickAnimation(); 
        }

        ApplyMovementAnimations();
    }

    private void SwitchCanKick()
    {
        _canKick = !_canKick;
        Debug.Log("canKick switched");
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
            if ((_isJumping && _playerMovement.VelocityY < 0) || (_playerMovement.VelocityY < -2 && !_playerMovement.IsOnWall))
            {
                _animator.SetBool("isJumping", false);
                _isJumping = false;
                _animator.SetBool("isFalling", true);
            }

            _animator.SetBool("isGrounded", false);

            if (_playerMovement.IsOnWall)
            {
                _animator.SetBool("isJumping", false);
                _isJumping = false;
                _animator.SetBool("isOnWall", true);
                _animator.SetBool("isFalling", false);

                if (_playerInput.IsJumpButtonPressed)
                {
                    _animator.SetBool("isJumping", true);
                    _isJumping = true;
                    _animator.SetBool("isOnWall", false);
                }
            }
            else
            {
                _animator.SetBool("isOnWall", false);
            }
        }
    }

    private void AnimateDamage()
    {
        _colorLerp.StartLerp();
    }
}
