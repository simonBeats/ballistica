using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public bool playerBullet;

    public int damage;

    public bool impactExplode;
    public GameObject explosionPrefab;

    private void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerBullet)
        {
            if(collision.tag == "enemy")
            {
                if(impactExplode)
                {
                    GameObject explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
                    explosion.GetComponent<Explosion>().player = playerBullet;
                }
                collision.GetComponent<Enemy>().Hit(damage);
                Destroy(gameObject);
            }
        }

        if (!playerBullet)
        {
            if(collision.GetComponent<Building>() != null)
            {
                if (impactExplode)
                {
                    GameObject explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
                    explosion.GetComponent<Explosion>().damage = this.damage;
                }
                collision.GetComponent<Building>().health -= damage;
                Destroy(gameObject);
            }
        }
    }

    
}
