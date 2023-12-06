using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoSingleton<LevelManager>
{
    public GameObject startWaveButton;
    private int lives = 10;
    [SerializeField]
    public int resources = 1000;
    private int currentWave;
    private int ammountOfWaves;
    private bool spawnActive = false;
    private bool waveActive = false;

    public int Lives { get { return lives; } }
    public int Resources { get { return resources; } set { resources = value; } }
    public void getRewardOnKill(int enemyWorth)
    {
        resources += enemyWorth;
        UIManager.Instance.DrawResourcesRemainingInfo();
    }
    private List<Wave> waves = new List<Wave>();

    public override void Init()
    {
        foreach (Wave w in GetComponents<Wave>())
        {
            waves.Add(w);
            Debug.Log(waves.Count);
        }
        Debug.Log(waves.Count);
        currentWave = 0;
        ammountOfWaves = waves.Count;

    }
    private void Start()
    {
        UIManager.Instance.DrawWaveInfo();
        UIManager.Instance.DrawLivesRemainingInfo();
        UIManager.Instance.DrawResourcesRemainingInfo();
    }

    private void Update()
    {
        if (!waveActive)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                StartWave();
            }
        }
        else
        {
            if (!spawnActive && !GameObject.FindGameObjectWithTag("Enemy"))
            {
                Debug.Log("Wave cleared");
                waveActive = false;
                if (waves.Count == 0)
                {
                    Victory();
                }
            }
        }
    }

    private void StartWave()
    {
        currentWave++;
        Debug.Log("Wave start");
        waves[0].StartWave();
        spawnActive = true;
        waveActive = true;

        UIManager.Instance.DrawWaveInfo();
    }

    public void EndWave()
    {
        Debug.Log("Wave end");
        Destroy(waves[0]);
        waves.RemoveAt(0);
        spawnActive = false;

    }

    public void EnemyCrossed()
    {
        lives--;
        UIManager.Instance.DrawLivesRemainingInfo();
        if (lives <= 0)
            Defeat();
    }

    public string GetWaveInfo()
    {
        return currentWave + " / " + ammountOfWaves;
    }

    public void Defeat()
    {
        //Wipe all enemies
        //clean the level
        UIManager.Instance.Defeat();
    }

    public void Victory()
    {
        //Wipe all enemies
        //clean the level
        Debug.Log("Victory");
    }

}
