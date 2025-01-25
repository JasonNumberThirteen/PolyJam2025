using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputEvents : MonoBehaviour
{
    public static EventHandler<Vector2> MoveAction;

    public static EventHandler<InputAction.CallbackContext> JumpActionEvents;
    public static EventHandler<bool> JumpAction;
    public static EventHandler ShootAction;
    public static EventHandler InteractAction;

    [SerializeField] private InputActionAsset inputActions;

    const string JUMP = "Jump";
    const string SHOOT = "Shoot";
    const string INTERACT = "Interact";

    private void Awake()
    {
        //inputActions.FindAction(JUMP).performed += JumpEvents;
        //inputActions.FindAction(JUMP).canceled += JumpEvents;
        inputActions.FindAction(SHOOT).performed += Shoot;
        inputActions.FindAction(INTERACT).performed += Interact;

    }

    private void Interact(InputAction.CallbackContext context)
    {
        InteractAction.Invoke(this, EventArgs.Empty);
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        ShootAction.Invoke(this, EventArgs.Empty);
    }

    private void JumpEvents(InputAction.CallbackContext obj)
    {
        JumpActionEvents.Invoke(this, obj);
    }

    private void OnDisable()
    {
        inputActions.FindAction(JUMP).performed -= JumpEvents;
        inputActions.FindAction(JUMP).canceled -= JumpEvents;
        inputActions.FindAction(SHOOT).performed -= Shoot;
    }

    private void Update()
    {
        MoveAction.Invoke(this, inputActions.FindAction("Move").ReadValue<Vector2>());

        JumpAction.Invoke(this, inputActions.FindAction(JUMP).IsPressed());
    }
}
