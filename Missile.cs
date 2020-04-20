using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Missile : MonoBehaviour {

    public Slider healthBar;

    private Building building;

    private void Awake()
    {
        building = GetComponent<Building>();
        healthBar.maxValue = building.health;
    }

    private void Update()
    {
        healthBar.value = building.health;
    }


}
