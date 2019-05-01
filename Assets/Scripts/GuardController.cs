using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public Transform pathHolder;

    private float speed = 10f;
    private float waitTime = .3f;
    private float turnSpeed = 90f;

    private void Start()
    {
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i].y = transform.position.y;
        }

        StartCoroutine(FollowPath(waypoints));

        Debug.Log("transform.position: " + transform.position.ToString() + ", waypoint pos: " + waypoints[2].ToString() + ", angle: " + Vector3.Angle(transform.position, waypoints[2]));
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
        while ( ! Mathf.Approximately( Quaternion.Angle(transform.rotation, targetRotation), 0 ) )
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            targetRotation = Quaternion.LookRotation(lookTarget - transform.position);
            yield return null;
        }

    }
}