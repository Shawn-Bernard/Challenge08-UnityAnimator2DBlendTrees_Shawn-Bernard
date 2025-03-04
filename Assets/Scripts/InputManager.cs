using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour, PlayerInput.IPlayerActions
{
    PlayerInput PlayerInput;
    private void Awake()
    {
        //Making a new instance of gameinput
        PlayerInput = new PlayerInput();
        //Enable my new instance of gameinput
        PlayerInput.Player.Enable();

        PlayerInput.Player.SetCallbacks(this);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        PlayerInputActions.MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerInputActions.InteractEvent?.Invoke();
        }
        else if (context.canceled)
        {
            PlayerInputActions.DropEvent?.Invoke();
        }
        
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //This will mean the button is held down
        if (context.started)
        {
            //This will increase my player speed when button is held
            PlayerInputActions.SprintEvent?.Invoke();
        }
        //This means the button was lifted
        else if (context.canceled)
        {
            //Since the player speed would be greater than 3 this will take away by amount set
            PlayerInputActions.SprintEvent?.Invoke();
        }
    }
}
public static class PlayerInputActions
{
    public static Action<Vector2> MoveEvent;

    public static Action SprintEvent;

    public static Action InteractEvent;

    public static Action DropEvent;
}
