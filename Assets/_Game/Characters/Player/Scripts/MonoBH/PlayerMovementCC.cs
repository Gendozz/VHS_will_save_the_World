using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMovementCC : MonoBehaviour
{
    [Header("-----     Настройки перемещения     -----")]
    [Header("Скорость горизонтального перемещения")]
    [SerializeField] private float _speed;
    
    [Header("Сила прыжка")]
    [SerializeField] private float _jumpForce;

    [Header("Сила притяжения")]
    [SerializeField] private float _gravity;

    [Header("-----     Компоненты и системные     -----")]
    [Header("Скорость горизонтального перемещения")]
    [SerializeField] private CharacterController _characterController;

    [Header("Пользовательский ввод")]
    [SerializeField] private PlayerInput _playerInput;


    private bool _isPlayerGrounded;

    private Vector3 _playerVelocity;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _isPlayerGrounded = _characterController.isGrounded;

        if (_isPlayerGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(_playerInput.HorizontalDirection, 0, 0);

        _characterController.Move(move * Time.deltaTime * _speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (_playerInput.IsJumpButtonPressed && _isPlayerGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpForce * -3.0f * _gravity);
        }

        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }
}
