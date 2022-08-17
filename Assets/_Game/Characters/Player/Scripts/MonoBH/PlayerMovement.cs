using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("-----      ��������� ����������� � ������      -----")]

    [Header("�������� ��������� �������� �� ������� ������� �������")]
    [SerializeField] private AnimationCurve speedCurve;

    [Header("���� ������")]
    [SerializeField] private float jumpForce;

    [Header("������ ��� ��� ������ ���� �� ������� ����� ������")]
    [SerializeField] private bool doDownwardsImpulse = true;

    [Header("������� ���� � ������� ����� ������")]
    [SerializeField] private float downForceImpulse;

    [Header("����, ������� ������� �����")]
    [SerializeField] private LayerMask groundLayer;

    [Header("������ �� ������ ���������, � ������� ���������� �����")]
    [SerializeField] private float groundCheckRadius;

    [Space]
    [Header("-----      ����������      -----")]

    [Header("���������� ���� �������")]
    [SerializeField] private Rigidbody rigidbody;


    [Header("������ ��������� ����������������� �����")]
    [SerializeField] private PlayerInput input;

    public bool IsGrounded { get; private set; } = false;

    private float _horizontalInputTreshold = 0.1f;

    private Collider[] _groundCollider = new Collider[1];

    private bool _canMove = true;

    private bool _isHorizontalMove = false;



    private void Update()
    {
        if (Mathf.Abs(input.HorizontalDirection) > _horizontalInputTreshold && _canMove)
        {
            _isHorizontalMove = true;
        }
        else
        {
            _isHorizontalMove = false;
        }

        if (input.IsJumpButtonPressed)
        {
            if (IsGrounded)
            {
                StartCoroutine(Jump());
            }
        }
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (_isHorizontalMove)
        {
            MoveToDirection(input.HorizontalDirection);
        }
    }

    private void MoveToDirection(float direction)
    {
        rigidbody.velocity = new Vector2(speedCurve.Evaluate(direction), rigidbody.velocity.y);
    }

    private IEnumerator Jump()
    {
        //rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
        rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        if (!doDownwardsImpulse)
        {
            yield break;            
        }

        float previousY = transform.position.y;
        float currentY = transform.position.y;
        
        while(previousY <= currentY)
        {
            previousY = currentY;
            currentY = transform.position.y;
            yield return null;
        }
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, downForceImpulse, rigidbody.velocity.z);

    }

    private void CheckGround()
    {
        IsGrounded = Physics.OverlapSphereNonAlloc(transform.position, groundCheckRadius, _groundCollider, groundLayer) > 0;
    }

    private IEnumerator BlockMovementOnSeconds(float seconds)
    {
        _canMove = false;
        yield return new WaitForSeconds(seconds);
        _canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }
}


