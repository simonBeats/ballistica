using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

    public Enemy enem;

    private void Start()
    {
        enem = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Building>() != null && !enem.targeted && collision.tag != "enemy")
        {
            enem.Target(collision.GetComponent<Building>());
        }
    }
}
