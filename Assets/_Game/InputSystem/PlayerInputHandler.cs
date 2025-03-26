using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    [Header("Control Settings")]
    public bool useKeyboardInput = true;

    public UIVirtualJoystick joystick;
    public UIVirtualTouchZone touchZone;
    private void Update()
    {
        if (!useKeyboardInput)
        {
            move = joystick.Coordinate();
            look = touchZone.TouchDist;
        }

    }

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value) => JumpInput(value.isPressed);

    public void MoveInput(Vector2 newMoveDirection) => move = newMoveDirection;
    public void LookInput(Vector2 newLookDirection) => look = newLookDirection;
    public void JumpInput(bool newJumpState) => jump = newJumpState;

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    public void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.Confined;
    }
}
