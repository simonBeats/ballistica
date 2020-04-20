using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public int damage;

    Animator anim;
    AudioSource aSource;
    public AudioClip clip;

    public bool player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();


        anim.SetTrigger("explosion");
        
        Invoke("PlayClip", 0.5f);

        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player)
        {
            Enemy hit = collision.gameObject.GetComponent<Enemy>();
            
            if (hit != null)
            {
                Debug.Log("Hit enemy!");
                hit.Hit(damage);
            }
        }
        if (!player)
        {
            Building hit = collision.gameObject.GetComponent<Building>();
            if(hit != null)
            {
                hit.Hit(damage);
                
            }
        }

    }

    private void PlayClip()
    {
        aSource.PlayOneShot(clip);
    }
}
