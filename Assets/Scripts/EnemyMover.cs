using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// it is good to add this like it is dependency
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    // [SerializeField] float waitTime = 1f;
    [SerializeField][Range(0f, 5f)] float speed = 1f;

    Enemy enemy;

    // void Start()
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }

    void FindPath()
    {
        path.Clear();

        // GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Path");

        // foreach (GameObject waypoint in waypoints)
        foreach (GameObject tile in tiles)
        {
            // path.Add(waypoint.GetComponent<Waypoint>());
            Waypoint waypoint = tile.GetComponent<Waypoint>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            // transform.position = waypoint.transform.position;

            Vector3 startPostion = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            // always need to update travel position
            float travelPercent = 0f;

            // facing endPosition
            transform.LookAt(endPosition);

            //while we are not in the end position
            while (travelPercent < 1)
            {
                // update with Time.deltaTime
                travelPercent += Time.deltaTime * speed;
                // LERP - Linear Interpolation (moves smoother and with midpoints) Vector3.LERP()
                transform.position = Vector3.Lerp(startPostion, endPosition, travelPercent);
                // when the end of frame will be ending coroutine will visit this function
                yield return new WaitForEndOfFrame();
            }

            //yield means give up control and comeback to me in 1 second in coroutines area
            //yield return new WaitForSeconds(waitTime);
        }

        // Destroy(gameObject);
        // enemy.StealGold();
        // gameObject.SetActive(false);

        FinishPath();
    }

}
