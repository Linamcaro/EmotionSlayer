using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private static PlayerControls _instance;

    public static PlayerControls Instance
    {
        get
        {
            return _instance;
        }
    }

    private InputActions inputActions;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;

        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnDestroy()
    {
        inputActions.Dispose();

    }

    public Vector2 GetPlayerMovement()
    {
        Vector2 inputMovement = inputActions.Player.Move.ReadValue<Vector2>();
        inputMovement = inputMovement.normalized;

        return inputMovement;
    }

    public bool PlayerJumped()
    {
        return inputActions.Player.Jump.triggered;
    }

    public bool PlayerFired()
    {
        return inputActions.Player.Fire.triggered;
    }

}
