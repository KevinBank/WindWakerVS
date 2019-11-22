using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZeldaController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private int dashDistance;
    [SerializeField] private GameObject playerModel;

    private PlayerInput inputHandler;
    private Vector2 inputMove;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = new PlayerInput();

        inputHandler.Gameplay.Dash.performed += ctx => StartDash();

        inputHandler.Gameplay.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        inputHandler.Gameplay.Move.canceled += ctx => inputMove = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(inputMove.x, inputMove.y) * movementSpeed;
        transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime);

        Debug.Log(rb.velocity);

        playerModel.transform.up = rb.velocity;
    }

    private void StartDash()
    {
        StartCoroutine(Dash(inputMove));
    }

    private IEnumerator Dash(Vector2 input)
    {
        for(int i = 0; i < dashDistance; i++)
        {
            Vector2 move = input.normalized * dashSpeed;
            transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime);
            yield return null;
        }
    }

    private void OnEnable()
    {
        inputHandler.Gameplay.Enable();
    }
}
