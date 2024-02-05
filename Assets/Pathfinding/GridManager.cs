using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// here we will store all our node in our world
public class GridManager : MonoBehaviour
{
    // [SerializeField] Node node;

    [SerializeField] Vector2Int gridSize;
    [Tooltip("Unity grid size - should match UnityEditor snap settings")]
    [SerializeField] int unityGridSize = 10;


    // key value cannot work with negative values in our nodes
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    // properties have uppercase first letter
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    public int UnityGridSize { get { return unityGridSize; } }

    void Awake()
    {
        CreateGrid();
    }

    // cannot get error can return null but with error you can crash game
    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {

        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();

        //algebra
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;

        return position;
    }

    // loop through x axis by one then 5 by y and then x 2 and y 5 if our grid size would be 5x5 
    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
                // Debug.Log(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
            }
        }
    }

    // void Start()
    // {
    //     // Debug.Log(node.coordinates);
    //     // Debug.Log(node.isWalkable);
    // }

}
