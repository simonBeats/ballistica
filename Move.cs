using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;
    public Vector3 direction;

    public float destroyDelay;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
