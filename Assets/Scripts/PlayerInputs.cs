using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour {
    [Header("Character Input Values")]
    public Vector2 Move;
    public Vector2 Look;
    public bool Jump;
    public bool Sprint;

    [Header("Mouse Cursor Settings")]
    public bool CursorLocked = true;
    public bool CursorInputForLook = true;

    private PlayerActionMap _actions;

    private void Awake() {
        _actions = new PlayerActionMap();
        _actions.Enable();
        _actions.Player.Move.started += OnMove;
        _actions.Player.Move.performed += OnMove;
        _actions.Player.Move.canceled += OnMove;
        _actions.Player.Look.started += OnLook;
        _actions.Player.Look.performed += OnLook;
        _actions.Player.Look.canceled += OnLook;
        _actions.Player.Sprint.started += OnSprint;
        _actions.Player.Sprint.started += OnSprint;
        _actions.Player.Sprint.performed += OnSprint;
        _actions.Player.Jump.canceled += OnJump;
        _actions.Player.Jump.performed += OnJump;
        _actions.Player.Jump.canceled += OnJump;
    }

    private void OnEnable() {
        _actions.Enable();
    }

    private void OnDisable() {
        _actions.Disable();
    }

    public void OnMove(InputAction.CallbackContext context) {
        MoveInput(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context) {
        if (CursorInputForLook) {
            LookInput(context.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        JumpInput(context.ReadValueAsButton());
    }

    public void OnSprint(InputAction.CallbackContext context) {
        SprintInput(context.ReadValueAsButton());
    }


    public void MoveInput(Vector2 newMoveDirection) {
        Move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection) {
        Look = newLookDirection;
    }

    public void JumpInput(bool newJumpState) {
        Jump = newJumpState;
    }

    public void SprintInput(bool newSprintState) {
        Sprint = newSprintState;
    }

    private void OnApplicationFocus(bool hasFocus) {
        SetCursorState(CursorLocked);
    }

    private void SetCursorState(bool newState) {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}