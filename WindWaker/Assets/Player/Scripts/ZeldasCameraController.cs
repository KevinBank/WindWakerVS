using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldasCameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerHead;
    [Header("Normal")]
    [SerializeField] private Transform camera;
    [SerializeField] private float turnSpeed, cameraSpeed, maxDistance, minDistance;
    [Header("Free Look")]
    [SerializeField] private bool freeLook;
    [Header("Locked On")]
    [SerializeField] private bool lockedOn;

    private PlayerInput inputHandler;
    private Vector2 inputTurn;

    private void Awake()
    {
        inputHandler = new PlayerInput();

        inputHandler.Gameplay.Turn.performed += ctx => inputTurn = ctx.ReadValue<Vector2>();
        inputHandler.Gameplay.Turn.canceled += ctx => inputTurn = Vector2.zero;
    }

    private void Update()
    {
        if(!freeLook && !lockedOn)
        {
            //moves the camera holder forwards when the player gets to far away
            if (Vector3.Distance(transform.position, playerHead.position) > maxDistance)
                transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime);
            
            //moves the camera holder backwards when the player gets to close
            if (Vector3.Distance(transform.position, playerHead.position) < minDistance)
                transform.Translate(Vector3.forward * -cameraSpeed * Time.deltaTime);

            //makes sure the camera looks at the player
            camera.LookAt(playerHead);
            transform.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0);
        }
        else
        {
            /*
            transform.Rotate(new Vector3(0, inputTurn.x, 0) * Time.deltaTime * turnSpeed);

            if(cameraLocation.position.z > -3.5f || cameraLocation.position.z < -10f)
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                transform.Translate(0, 0, scroll * zoomSpeed, Space.Self);
            }
            */
        }
    }

    private void OnEnable()
    {
        inputHandler.Gameplay.Enable();
    }
}