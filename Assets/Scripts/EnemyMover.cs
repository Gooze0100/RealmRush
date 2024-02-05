using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// it is good to add this like it is dependency
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    // [SerializeField] List<Tile> path = new List<Tile>();
    List<Node> path = new List<Node>();
    // [SerializeField] float waitTime = 1f;
    [SerializeField][Range(0f, 5f)] float speed = 1f;

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    // void Start()
    void OnEnable()
    {
        ReturnToStart();
        // RecalculatePath();
        RecalculatePath(true);
        // ReturnToStart();
        // StartCoroutine(FollowPath());
    }

    // void Start()
    void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    // void FindPath()
    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        //stops all coroutines until path is found
        StopAllCoroutines();

        path.Clear();

        // // GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");
        // GameObject[] tiles = GameObject.FindGameObjectsWithTag("Path");

        // // foreach (GameObject waypoint in waypoints)
        // foreach (GameObject tile in tiles)
        // {
        //     // path.Add(waypoint.GetComponent<Waypoint>());
        //     Tile waypoint = tile.GetComponent<Tile>();

        //     if (waypoint != null)
        //     {
        //         path.Add(waypoint);
        //     }
        // }

        // after new pathfinder
        // path = pathfinder.GetNewPath();
        path = pathfinder.GetNewPath(coordinates);

        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        // transform.position = path[0].transform.position;
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        // foreach (Tile waypoint in path)
        // for (int i = 0; i < path.Count; i++)
        // go straight to next path node
        for (int i = 1; i < path.Count; i++)
        {
            // transform.position = waypoint.transform.position;

            Vector3 startPostion = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
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
