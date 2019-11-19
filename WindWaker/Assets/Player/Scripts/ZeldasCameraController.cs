using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldasCameraController : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] Transform cameraLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * turnSpeed);

        if(cameraLocation.position.z > -3.5f || cameraLocation.position.z < -10f)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * zoomSpeed, Space.Self);
        }
    }
}