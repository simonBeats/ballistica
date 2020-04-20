using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour {

	public List<GameObject> upcommingEnemies = new List<GameObject>();

    public Transform[] spawnpoints;

    public float intervall;

    [SerializeField]
    public int index;

    public int waveReward;

    public bool finalWave;

    public void StartWave()
    {
        InvokeRepeating("Spawn", 0.0f, intervall);
    }

    public void Spawn()
    {
        if (index >= upcommingEnemies.ToArray().Length)
        {
            CancelInvoke();
            return;
        }
        
        Instantiate(upcommingEnemies[index], spawnpoints[index].position, Quaternion.identity);

        if (!WaveSystem.waveRunning) WaveSystem.waveRunning = true;

        index++;
    }
}
