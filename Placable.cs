using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placable : MonoBehaviour {

    public bool beingPlaced;
    public bool overlap;

    private Building building;

    private void Start()
    {
        building = GetComponent<Building>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        overlap = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        overlap = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        overlap = false;
    }

    private void Update()
    {
        if (beingPlaced)
        {
            BuildManager.locked = true;

            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            if (Input.GetMouseButton(0) && !overlap)
            {
                beingPlaced = false;
                BuildManager.locked = false;
                building.Buy();
            }
        }
    }
}
