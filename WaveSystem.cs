using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSystem : MonoBehaviour {

    public List<Wave> waves = new List<Wave>();

    public GameObject buildingSystem;

    public DialogueSystem dSystem;

    [SerializeField]
    private int index = 0;

    public static bool waveRunning = false;

    public Text waveDisplay;
    public Text rewardDisplay;

    public SelectSystem sSystem;

    
    public int countdown = 180;

    public Text countdownText;

    private AudioSource aSource;
    public AudioClip music;

    public Animator missileAnimator;
    public GameObject rocketPrefab;
    public Transform missileSocket;

    public bool gameOver;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(countdown <= 0 && !gameOver)
        {
            gameOver = true;
            GameWon();
            Invoke("SwitchEndScreen", 5f);
        }

        if (gameOver) return;

        if (!EnemieOnField() && waveRunning && waves[index].index == waves[index].upcommingEnemies.Count)
        {
            GameManager.cash += waves[index].waveReward;

            Debug.Log("wave clear");
            index++;

            Turret[] allTurrets = FindObjectsOfType<Turret>();
            foreach (Turret turret in allTurrets) turret.StopFire();

            Truck[] allTrucks = FindObjectsOfType<Truck>();
            foreach (Truck truck in allTrucks)
            {
                Destroy(truck.gameObject);
                GameManager.cash += truck.reward;
            }

            dSystem.gameObject.SetActive(true);
            dSystem.StartDialogue(dSystem.dialogue[index]);

            waveRunning = false;
        }

        countdownText.text = "Seconds until liftoff: " + countdown;

        waveDisplay.gameObject.SetActive(waveRunning);
        waveDisplay.text = "Wave " + (index + 1).ToString();

        rewardDisplay.gameObject.SetActive(waveRunning);
        rewardDisplay.text = "Reward for clearance: " + waves[index].waveReward;
    }

    public void GameWon()
    {
        missileAnimator.SetBool("won", true);
        Instantiate(rocketPrefab, missileSocket.position, Quaternion.identity);
        Invoke("SwitchEndScreen", 5f);
    }

    private void SwitchEndScreen()
    {
        SceneManager.LoadScene(3);
    }

    public void NextWave()
    {
        waves[index].StartWave();
        buildingSystem.SetActive(false);

        Turret[] allTurrets = FindObjectsOfType<Turret>();

        sSystem.eraseMode = false;
        sSystem.eraseToggle.isOn = false;

        foreach (Turret t in allTurrets) t.startFire = true;

        if (waves[index].finalWave)
        {
            countdownText.gameObject.SetActive(true);
            StartCoroutine("Countdown");
            aSource.PlayOneShot(music);
        }

    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            countdown--;
            Debug.Log("countdown");
        }
    }

    private bool EnemieOnField()
    {
        return (FindObjectsOfType<Enemy>().Length > 0);
    }
}
