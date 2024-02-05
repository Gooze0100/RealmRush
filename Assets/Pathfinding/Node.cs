using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this will serialize our class, and can be used in inspector and will see all our public variables
[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    // walking between nodes
    public bool isWalkable;
    // explored by pathFinding
    public bool isExplored;
    public bool isPath;
    //this is parent node which node branched off 
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
