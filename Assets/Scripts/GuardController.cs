using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public Transform pathHolder;

    private float speed = 10f;
    private float waitTime = .3f;
    private float turnSpeed = 90f;
    public float timeToSpotPlayer = .5f;

    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;

    float viewAngle;
    Transform player;
    public GameObject playerGameObject;

    public GameObject timebar;
    RectTransform timebarParent;
    DetectionBarController barController;

    public GameObject gameOverPanel; //Panel for displaying the "You have been spotted!" text

    void Awake()
    {
        gameOverPanel.SetActive(false);
        timebar.SetActive(false);
        //timebar.transform.SetAsLastSibling();
        //timebarParent = timebar.GetComponentInParent<RectTransform>();
        //timebarParent.SetAsLastSibling();
    }

    private void Start()
    {
        
        barController = timebar.GetComponent<DetectionBarController>();

        

        player = playerGameObject.transform;
          
        viewAngle = spotlight.spotAngle;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i].y = transform.position.y;
        }

        StartCoroutine(FollowPath(waypoints));
    }

    void Update()
    {
        spotlight.transform.position = transform.position;

        if (CanSeePlayer())
        {
            timebar.SetActive(true);
            barController.StartIncreasing(Spotted);

        }
        else
        {
            if ((timebar.activeSelf) && !barController.HasBeenCalled())
            {
                barController.StopIncreasing(hideTimebar);
                //barController.ResetSliders();
                
            }
        }
    }

    void Spotted()
    {
        gameOverPanel.SetActive(true);
        hideTimebar();
        Destroy(player.gameObject);
    }

    void hideTimebar()
    {
        timebar.SetActive(false);
    }

    bool CanSeePlayer()
    {
        if (player != null)
        {
            if ((Vector3.Distance(transform.position, player.position) < viewDistance) //if the player is within view distance
                && (!Physics.Raycast(transform.position, Vector3.forward, viewDistance))) //if there are no walls/colliders blocking the guard from seeing the player
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

        else return false;
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