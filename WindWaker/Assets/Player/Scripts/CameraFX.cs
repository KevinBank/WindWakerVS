using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;
    [SerializeField] private Transform cameraLocation;
    [SerializeField] private Transform player;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        transform.position = cameraLocation.position;
    }
}
