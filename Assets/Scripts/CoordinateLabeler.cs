using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// this tag [ExecuteAlways] now should be used in caution because it will be executed in edit mode and play mode also
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    //grabbing it by a code
    TextMeshPro label;
    //representing vector position with integers
    Vector2Int coordinates = new Vector2Int();
    // Waypoint waypoint;

    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        //parent because Waypoint is the root of our object
        // waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }

    void Update()
    {
        // need to check if application is just working and it will be working in edit mode
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        // if (waypoint.IsPlaceable)
        // {
        //     label.color = defaultColor;
        // }
        // else
        // {
        //     label.color = blockedColor;
        // }

        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        // path should be first after walkable because after isExplored path won't be with another color
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void DisplayCoordinates()
    {
        // transform.parent because it is on a child(text) of parent(cube)
        // UnityEditor.EditorSnapSettings.move.x need to divide because positions are roundabouts
        // anything associated with UnityEditor cannot be built in our project if we would like build it it would throw and error
        //if you would move this script into Editor folder it would exclude it and no problem would be found
        // coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        // coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        if (gridManager == null) { return; }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        // make names like coordinates
        transform.parent.name = coordinates.ToString();
    }
}
