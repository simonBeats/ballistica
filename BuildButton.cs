using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour {

    public Building building;

    public Image preview;
    public Text cashDisplay;

    private Vector3 mousePos;
    private GameObject curPlacing;

    private void Update()
    {
        preview.sprite = building.preview;

        if (building.turret)
        {
            Building[] allBuildings = FindObjectsOfType<Building>();
            int turrets = 0;

            foreach (Building b in allBuildings)
            {
                if (b.turret) turrets++;
            }

            building.cost = Mathf.RoundToInt(6 * (Mathf.Pow(turrets, 2)) + building.baseCost);
        }

        cashDisplay.text = building.cost.ToString();

        mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
       
    }

    public void Place()
    {
        curPlacing = Instantiate(building.gameObject, mousePos, Quaternion.identity);
        curPlacing.GetComponent<Placable>().beingPlaced = true;
    }

}
