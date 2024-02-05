using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // [SerializeField] GameObject towerPrefab;
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;

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
    void OnMouseDown()
    {
        if (isPlaceable)
        {
            // if(Input.GetMouseButtonDown(0)){}
            // Debug.Log(transform.name);
            // Instantiate(towerPrefab, transform.position, Quaternion.identity);

            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
