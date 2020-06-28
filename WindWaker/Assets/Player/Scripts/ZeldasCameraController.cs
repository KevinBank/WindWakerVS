using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldasCameraController : MonoBehaviour
{
    [SerializeField] private Transform playerHead, cameraPos, index, target;
    [SerializeField] private MovementState state;
    [Header("Follow")]
    [SerializeField] private Transform cam;
    [SerializeField] private float cameraSpeed, maxDistance, minDistance;

    private void LateUpdate()
    {
        switch (state)
        {
            case MovementState.NORMAL:
                Follow();
                break;
            case MovementState.LOCKED_ON:
                LockedOn();
                break;
            default:
                Follow();
                break;
        }
    }

    private void Follow()
    {
        //moves the camera holder forwards when the player gets to far away
        if (Vector3.Distance(transform.position, playerHead.position) > maxDistance)
            transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime);

        //moves the camera holder backwards when the player gets to close
        if (Vector3.Distance(transform.position, playerHead.position) < minDistance)
            transform.Translate(Vector3.forward * -cameraSpeed * Time.deltaTime);

        //makes sure the camera looks at the player
        cam.LookAt(playerHead);
        transform.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);
    }

    private void LockedOn()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, cameraPos.position, 0.05f);

        //makes sure the camera looks at the player
        index.LookAt(target);
        cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, index.rotation, 20f);
        transform.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);
    }

    public float CameraSpeed { set { cameraSpeed = value; } }
    public MovementState State { set { state = value; } }
    public Transform Target { set { target = value; } }
}