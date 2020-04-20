using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public int health;

    public GameObject GFX;

    public GameObject hitParticles;

    public Transform rocket;

    public float speed;
    public bool moving;

    public Animator anim;

    public Building target;
    public bool targeted;

    public float fireRate;
    public float bulletForce;

    public GameObject bulletPrefab;

    public Transform barrelEnd;

    public List<string> blacklist = new List<string>();

    public Slider healthBar;

    public int reward;

    private void Start()
    {
        rocket = FindObjectOfType<Missile>().transform;
        healthBar.maxValue = health;
    }

    public void Update()
    {
        healthBar.value = health;

        

        if (moving && rocket != null)
        {
            Vector3 toBase = rocket.position - transform.position;
            Walk(toBase);
            anim.SetBool("moving", true);

            if (toBase.x < 0)
            {
                GFX.transform.rotation = new Quaternion(0, 180, 0, 0);
                
            }
            else if (toBase.x > 0)
            {
                GFX.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            
        }

        if(health <= 0)
        {
            Die();
        }

        if(target == null)
        {
            moving = true;
            targeted = false;
            CancelInvoke();
        }
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (hitParticles != null)
        {
            GameObject gj = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(gj, 3.0f);
        }
    }

    public void Die()
    {
        GameManager.cash += reward;
        Destroy(gameObject);
    }

    public void Walk(Vector3 direction)
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    public void Target(Building building)
    {
        if (blacklist.Contains(building.buildingName)) return;

        target = building;
        targeted = true;
        moving = false;
        anim.SetBool("moving", false);
        InvokeRepeating("Fire", 0.0f, fireRate);
    }

    public void Fire()
    {
        if (rocket == null) return;

        Vector3 direction = target.transform.position - transform.position;

        GameObject bullet = Instantiate(bulletPrefab, barrelEnd.position, Quaternion.identity);
        bullet.transform.right = direction;
        Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();
        rb2D.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }
}
