using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform followPosition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject arrowPickup;
    [SerializeField] private GameObject leftEye;
    [SerializeField] private GameObject rightEye;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float offset;
    [Range(0,1)] [SerializeField] private float delayTime;
    [SerializeField] private int tickedAmount;
    [SerializeField] private bool waited;
    [SerializeField] private BossState state;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BossState.Moving:
                break;
            case BossState.Shooting:
                Shoot();
                leftEye.SetActive(false);
                rightEye.SetActive(false);
                break;
            case BossState.died:
                break;
            default:
                break;
        }

        transform.position = followPosition.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, pointer.rotation, rotationSpeed);

        
    }

    public void Shoot()
    {
        if(waited)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - offset, transform.position.z), transform.rotation);
            waited = false;
            StartCoroutine(wait());
        }
    }

    private void dropArrows()
    {
        Instantiate(arrowPickup, transform.position, Quaternion.Euler(0,0,0));
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(delayTime);
        waited = true;
        StopCoroutine(wait());
    }

    public BossState State { set { this.state = value; } }
}
