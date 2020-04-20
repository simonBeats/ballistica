using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour {

    public float speed;
    public bool moving;

    public float ejectDistance;

    private Transform rocket;

    public Animator anim;

    public int reward;

    public GameObject GFX;

    public GameObject squadPrefab;
    public Transform ejectPoint;

    private void Start()
    {
        rocket = FindObjectOfType<Missile>().transform;
    }

    private void Update()
    {
        if (moving)
        {
            Vector3 toBase = rocket.position - transform.position;
            Move(toBase);
            anim.SetBool("moving", true);
            
            if (toBase.x < 0)
            {
                GFX.transform.rotation = new Quaternion(0, 180, 0, 0);

            }
            else if (toBase.x > 0)
            {
                GFX.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            if(toBase.magnitude <= ejectDistance)
            {
                moving = false;
                Eject();
            }
        }
    }

    void Eject()
    {
        anim.SetBool("empty", true);
        Instantiate(squadPrefab, ejectPoint.position, Quaternion.identity);
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void Die()
    {
        GameManager.cash += reward;
        Destroy(gameObject);
    }
}
