using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	public List<Building> basePool = new List<Building>();

    public static bool locked;

    public BuildButton[] buildButtons;

    public Button respinButton;

    public int respinCost;
    public Text respinCostDisplay;

    public Building rocket;
    private int rocketMaxhealth;

    public Button restoreButton;
    public Text restoreButtonText;

    public GameObject rotatingInfo;

    public static bool rotating;

    private void Awake()
    {
        Respin();
        rocketMaxhealth = rocket.health;
    }

    private void Update()
    {
        rotatingInfo.SetActive(rotating);

        if (locked)
        {
            for (int i = 0; i < buildButtons.Length; i++)
            {
                buildButtons[i].GetComponent<Button>().interactable = false;
            }
            return;
        }

        

        for(int i = 0; i < buildButtons.Length; i++)
        {
            if(buildButtons[i].building.GetComponent<Building>().cost > GameManager.cash)
            {
                buildButtons[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                buildButtons[i].GetComponent<Button>().interactable = true;
            }
        }

        respinButton.interactable = respinCost <= GameManager.cash;
        respinCostDisplay.text = respinCost.ToString();


        if(rocketMaxhealth - rocket.health > GameManager.cash || rocket.health == rocketMaxhealth)
        {
            restoreButton.interactable = false;
        }
        else
        {
            restoreButton.interactable = true;
        }

        restoreButtonText.text = "Restore " + (rocketMaxhealth - rocket.health) + " rocket health.";
    }

    public void Respin()
    {
        foreach(BuildButton bButton in buildButtons)
        {
            bButton.building = RandPick();
        }

        GameManager.cash -= respinCost;
    }

    public void RestoreRocket()
    {
        int restored = rocketMaxhealth - rocket.health;
        rocket.health = rocketMaxhealth;
        GameManager.cash -= restored;
    }

    public Building RandPick()
    {
        List<Building> temp = new List<Building>();
        for(int i = 0; i < basePool.ToArray().Length; i++)
        {
            int x = Mathf.RoundToInt(basePool.ToArray()[i].priority * 100);
            for (int j = 0; j < x; j++)
            {
                temp.Add(basePool.ToArray()[i]);
            }
        }

        return temp[Random.Range(0, temp.Count)];
    }

    public void RandomBuilding(BuildButton bButton)
    {
        bButton.building = RandPick();
    }

    public void Remove(Building b)
    {
        GameManager.cash += (b.cost / 2);
        Destroy(b.gameObject);
    }

    
}
