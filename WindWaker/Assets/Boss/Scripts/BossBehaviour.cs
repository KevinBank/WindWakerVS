using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform followPosition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float shootDelay;
    [SerializeField] private int tickedAmount;
    [SerializeField] private bool waited;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followPosition.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, pointer.rotation, rotationSpeed);
        if(waited)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            waited = false;
            StartCoroutine(wait(shootDelay));
        }
    }

    public IEnumerator wait(float waitSecond)
    {
        while (tickedAmount < 1)
        {
            tickedAmount++;
            yield return new WaitForSecondsRealtime(waitSecond);
        }

        StopCoroutine(wait(0f));
        tickedAmount = 0;
        waited = true;

    }
}
