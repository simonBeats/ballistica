using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Building : MonoBehaviour {

    public int cost;
    public int baseCost;

    public int health;

    public bool missile;
    public bool turret;

    public string buildingName;

    public float priority;

    public Sprite preview;

    public void Buy()
    {
        GameManager.cash -= cost;
    }

    public void Hit(int damage)
    {
        health -= damage;
        Debug.Log(name + " got hit an lost " + damage + " health");
    }

    private void Update()
    {

        if (turret)
        {
            Building[] allBuildings = FindObjectsOfType<Building>();
            int turrets = 0;

            foreach(Building b in allBuildings)
            {
            if (b.turret) turrets++;
            }

            cost = Mathf.RoundToInt(6 * (Mathf.Pow(turrets, 2)) + baseCost);

            Debug.Log("There are " + turrets + " turrets so " + name + " costs " + cost);
        }

        if(health <= 0)
        {
            if (missile)
            {
                SceneManager.LoadScene(2);
                return;
            }

            Destroy(gameObject);
        }
    }
}
