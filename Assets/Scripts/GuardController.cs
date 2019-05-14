using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : Notifier
{
    public Transform pathHolder;
    public Transform player;

    private float speed = 10f;
    private float waitTime = .3f;
    private float turnSpeed = 90f;
    //public float timeToSpotPlayer = .5f;

    public float viewDistance;
    public Light spotlight;
    public LayerMask viewMask;

    float viewAngle;
    bool playerInView = false;
    bool notificationSent = false;
    string guardName;

    private void Awake()
    {
        guardName = gameObject.name;
    }

    private void Start()
    {
        if (pathHolder == null)
        {
            Debug.Log("Error: a guard is missing a pathHolder!");
            Destroy(gameObject);
        }

        if (player == null)
        {
            Debug.Log("Error: a guard is missing a player reference!");
            Destroy(gameObject);
        }

        viewAngle = spotlight.spotAngle;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i].y = transform.position.y;
        }

        StartCoroutine(FollowPath(waypoints));
        StartCoroutine(DetectPlayer());
    }

    bool CanSeePlayer()
    {
        if ((Vector3.Distance(transform.position, player.position) < viewDistance) //if the player is within view distance
        && (!Physics.Raycast(transform.position, transform.forward * viewDistance, viewDistance))) //if there are no walls/colliders blocking the guard from seeing the player
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }

        return false;
    }


    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 prevPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Vector3 currentPosition = waypoint.position;

            Gizmos.DrawSphere(currentPosition, .3f);
            Gizmos.DrawLine(prevPosition, currentPosition);

            prevPosition = currentPosition;
        }

        Gizmos.DrawLine(startPosition, prevPosition);
    }

    IEnumerator DetectPlayer()
    {
        while (true)
        {
            playerInView = CanSeePlayer();

            if (playerInView && !notificationSent)
            {
                Notify(new ObserverEvent(guardName + ":detected"));
                notificationSent = true;
            }


            if (!playerInView)
            {
                if (notificationSent)
                {
                    Notify(new ObserverEvent(guardName + ":lost"));
                    notificationSent = false;
                }
            }

            yield return null;
        }
    }

    IEnumerator FollowPath(Vector3[] waypoints)
{
    transform.position = waypoints[0];
    transform.LookAt(waypoints[0]);

    int targetWaypointIndex = 1;
    Vector3 targetWaypoint = waypoints[targetWaypointIndex];

    while (true)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
        if (transform.position == targetWaypoint)
        {
            targetWaypointIndex = ++targetWaypointIndex % waypoints.Length;
            targetWaypoint = waypoints[targetWaypointIndex];

            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(TurnToFace(targetWaypoint));
        }
        yield return null;
    }
}

IEnumerator TurnToFace(Vector3 lookTarget)
{
    Quaternion targetRotation = Quaternion.LookRotation(lookTarget - transform.position);
    while (!Mathf.Approximately(Quaternion.Angle(transform.rotation, targetRotation), 0))
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        targetRotation = Quaternion.LookRotation(lookTarget - transform.position);
        yield return null;
    }

}
}