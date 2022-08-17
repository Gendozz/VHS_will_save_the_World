using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalDirection { get; private set; }
    public bool IsJumpButtonPressed { get; private set; }
    public bool IsAttackButtonPressed { get; private set; }

    private bool shouldDetectInput = true;

    private void BlockInput()
    {
        shouldDetectInput = false;
        HorizontalDirection = 0;
    }


    void Update()
    {
        if (shouldDetectInput)
        {
            HorizontalDirection = Input.GetAxis(StringConsts.HORIZONTAL_AXIS);
            IsJumpButtonPressed = Input.GetButtonDown(StringConsts.JUMP);
            IsAttackButtonPressed = Input.GetButtonDown(StringConsts.ATTACK);
        }
    }
}
