using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomRockets : MonoBehaviour {

    public GameObject rocketPrefab;

    public Transform[] spawns;

    private void Start()
    {
        InvokeRepeating("SpawnRocket", 0.0f, 1.5f);
    }

    private void SpawnRocket()
    {
        int i = Random.Range(0, spawns.Length);

        Instantiate(rocketPrefab, spawns[i].position, Quaternion.identity);
    }
}
