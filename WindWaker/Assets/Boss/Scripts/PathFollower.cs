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
    [SerializeField] private Path path;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float arrivalThreshold = 0.5f;
    [SerializeField] public int targetNumber = 0;
    [SerializeField] private float testLerp;
    private int tickedAmount;

    private void Start()
    {
        path = GameObject.Find("Waypoints").GetComponent<Path>();
    }

    void Update()
    {
        if (!targetWaypoint)
        {
            targetWaypoint = path.GetNextWaypoint(targetNumber, this.gameObject);
            //wait(5f);
            targetNumber++;
        }
        //transform.LookAt(targetWaypoint.Position);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetWaypoint.Position, testLerp);

        if (Vector3.Distance(transform.position, targetWaypoint.Position) <= arrivalThreshold)
            targetWaypoint = null;
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
    }
}