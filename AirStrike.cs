using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrike : MonoBehaviour {

    public GameObject explosionPrefab;

    private Vector3 strikePos;

    public int minBombs;
    public int maxBombs;

    public float radius;

    private int bombs;

    private void Start()
    {
        bombs = Random.Range(minBombs, maxBombs);

        Missile rocket = FindObjectOfType<Missile>();
        strikePos = rocket.transform.position;

        Invoke("Strike", 1.0f);

    }

    private void Strike()
    {
        for(int i = 0; i < bombs; i++)
        {
            float randX = Random.Range(strikePos.x - radius, strikePos.x + radius);
            float randY = Random.Range(strikePos.y - radius, strikePos.y + radius);

            Vector3 pos = new Vector3(randX, randY, 0);

            Instantiate(explosionPrefab, pos, Quaternion.identity);
        }
    }

}
