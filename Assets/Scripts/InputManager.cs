using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInventoryAction;
    public event EventHandler OnAttackAction;
    public event EventHandler OnSprintAction;

    public event EventHandler OnReadyStart;
    public event EventHandler OnReadyFinish;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Inventory.performed += Inventory_performed;
        playerInputActions.Player.Attack.performed += Attack_performed;
        playerInputActions.Player.Sprint.performed += Sprint_performed;

        playerInputActions.Player.Ready.started += Ready_started;
        playerInputActions.Player.Ready.canceled += Ready_canceled;
    }





    //---------------ATTACK---------------
    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    //---------------READY---------------
    private void Ready_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnReadyStart?.Invoke(this, EventArgs.Empty);
    }
    private void Ready_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnReadyFinish?.Invoke(this, EventArgs.Empty);
    }

    //---------------INVENTORY---------------
    private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInventoryAction?.Invoke(this, EventArgs.Empty);
    }

    //---------------INTERACT---------------
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    //---------------MOVEMENT---------------
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    //---------------SPRINT---------------
    private void Sprint_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSprintAction?.Invoke(this, EventArgs.Empty);
    }

    //---------------ROTATION---------------
    public Vector2 GetRotationVector()
    {
        Vector2 inputVector = playerInputActions.Player.Rotate.ReadValue<Vector2>();

        return inputVector;
    }
}
