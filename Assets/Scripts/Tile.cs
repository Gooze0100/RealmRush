using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // [SerializeField] GameObject towerPrefab;
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
    }

    // public bool GetIsPlaceable()
    // {
    //     return isPlaceable;
    // }

    // there is possibility to have still mouse down with if statement but it is a little bit cumbersome
    // void OnMouseOver()

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        // if (isPlaceable)
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            // if(Input.GetMouseButtonDown(0)){}
            // Debug.Log(transform.name);
            // Instantiate(towerPrefab, transform.position, Quaternion.identity);

            // bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            // isPlaceable = !isPlaced;

            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
        }
    }
}
