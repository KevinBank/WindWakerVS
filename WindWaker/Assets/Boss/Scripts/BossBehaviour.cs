using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform followPosition;
    [SerializeField] private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followPosition.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, pointer.rotation, rotationSpeed);
    }
}
