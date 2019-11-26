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
    [SerializeField] private GameObject cam;
    [SerializeField] private float rotateSpeed;

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

        Vector3 newForward = cam.transform.rotation * new Vector3(move.x, 0, move.y);
        transform.Translate(newForward * Time.deltaTime);
        playerModel.transform.LookAt(playerModel.transform.position + newForward);

        
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

    void Rotate(float h, float v)
    {
        Debug.Log("hey");
        Vector3 desiredDirection = Vector3.Normalize(new Vector3(h, 0f, v));
        if (desiredDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(desiredDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }
    }
}
