using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private InputMaster inputHandler;
    [SerializeField] private ZeldaController zeldaController;
    [SerializeField] private ZeldasCameraController zeldasCameraController;
    [SerializeField] private Bow bow;

    private void Awake()
    {
        inputHandler = new InputMaster();

        inputHandler.Gameplay.Dash.performed += ctx => zeldaController.StartDash();

        inputHandler.Gameplay.Move.performed += ctx => zeldaController.InputMove = ctx.ReadValue<Vector2>();
        inputHandler.Gameplay.Move.canceled += ctx => zeldaController.InputMove = Vector2.zero;

        inputHandler.Gameplay.Turn.performed += ctx => zeldaController.InputMove = ctx.ReadValue<Vector2>();
        inputHandler.Gameplay.Turn.canceled += ctx => zeldaController.InputMove = Vector2.zero;

        inputHandler.Gameplay.Shoot.performed += ctx => bow.Shoot();
        inputHandler.Gameplay.LockOn.performed += ctx => bow.LockOn(true);
        inputHandler.Gameplay.LockOn.canceled += ctx => bow.LockOn(false);
    }

    private void OnEnable()
    {
        inputHandler.Gameplay.Enable();
    }
}
