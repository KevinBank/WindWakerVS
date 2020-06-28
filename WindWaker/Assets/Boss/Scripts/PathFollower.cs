using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// De path follower class is verantwoordelijk voor de beweging.
    /// Deze class zorgt ervoor dat het object (in Tower Defense) vaak een enemy, het path afloopt
    /// tip: je kunt de transform.LookAt() functie gebruiken en vooruitbewegen.
    /// </summary>
public class PathFollower : MonoBehaviour
{
    [SerializeField] private Waypoint targetWaypoint;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject leftEye;
    [SerializeField] private GameObject rightEye;
    [SerializeField] private Path path;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float arrivalThreshold = 0.5f;
    [SerializeField] public int targetNumber = 0;
    [SerializeField] private int numberOfWaypoints;
    [SerializeField] private float testLerp;
    [SerializeField] protected bool waited = true;
    [SerializeField] private float waitTime;
    [SerializeField] private int test;
    private int tickedAmount;

    private void Start()
    {
        path = GameObject.Find("Waypoints").GetComponent<Path>();
    }

    void Update()
    {
        if (!targetWaypoint && waited)
        {
            targetWaypoint = path.GetNextWaypoint(targetNumber, this.gameObject);
            targetNumber = Random.Range(0, numberOfWaypoints);
            boss.GetComponent<BossBehaviour>().State = BossState.Moving;
            int i = Random.Range(1, 3);
            if (i == 1)
                leftEye.SetActive(true);
            else if (i == 2)
                rightEye.SetActive(true);
            waited = false;
            StartCoroutine(wait(waitTime));
        }
        //transform.LookAt(targetWaypoint.Position);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(targetWaypoint != null)
        {
            transform.position = Vector3.Lerp(transform.position, targetWaypoint.Position, testLerp);

            if (Vector3.Distance(transform.position, targetWaypoint.Position) <= arrivalThreshold)
            {
                targetWaypoint = null;
                boss.GetComponent<BossBehaviour>().State = BossState.Shooting;
                test++;
            }
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