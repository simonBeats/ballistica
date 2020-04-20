using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSystem : MonoBehaviour {

    public BuildManager bManager;
    public GameObject selection;

    public Toggle eraseToggle;
    public bool eraseMode;

    private void Update()
    {
        selection = null;

        eraseToggle.interactable = !BuildManager.locked;
        if (BuildManager.locked) eraseToggle.isOn = false;

        eraseMode = eraseToggle.isOn;
        

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                selection = hit.collider.gameObject;
            }
        }

        if (selection == null) return;

        if(selection.GetComponent<Building>() != null && eraseMode && !selection.GetComponent<Building>().missile)
        {
            bManager.Remove(selection.GetComponent<Building>());
        }

        if (selection.tag == "turret")
        {
            selection.GetComponent<Turret>().rotating = true;
            selection = null;
        }
    }

    
}
