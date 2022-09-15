using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Перевёрнуто ли управление")]
    [SerializeField] private bool _isInputInversed = false;

    public float HorizontalDirection { get; private set; }
    public bool IsJumpButtonPressed { get; private set; }
    public bool IsAttackButtonPressed { get; private set; }
    public bool IsGrappleButtonPressed { get; private set; }
    public bool IsCloneActivationButtonPressed { get; private set; }

    private bool _shouldDetectInput = true;

    public bool isHorizontalInput { get; private set; }

    public void SwitchInput(bool shouldDetectInput)
    {
        HorizontalDirection = 0;
        _shouldDetectInput = shouldDetectInput;
    }

    void Update()
    {
        if (_shouldDetectInput)
        {
            IsJumpButtonPressed = Input.GetButtonDown(StringConsts.JUMP);
            IsCloneActivationButtonPressed = Input.GetKeyDown(KeyCode.E);
            IsAttackButtonPressed = Input.GetKeyDown(KeyCode.R);
            isHorizontalInput = Input.GetButton(StringConsts.HORIZONTAL_AXIS);

            switch (_isInputInversed)
            {
                // For main
                case false:
                    HorizontalDirection = Input.GetAxis(StringConsts.HORIZONTAL_AXIS);
                    IsGrappleButtonPressed = Input.GetKeyDown(KeyCode.Q);
                    break;

                // For clone
                case true:
                    HorizontalDirection = -Input.GetAxis(StringConsts.HORIZONTAL_AXIS);
                    IsGrappleButtonPressed = Input.GetKeyDown(KeyCode.R);
                    break;
            }

        }
    }
}
