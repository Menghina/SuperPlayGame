using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class spawnPoint
{
    public Transform self;
    public Transform destination;
}

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public List<spawnPoint> spawnPoint = new List<spawnPoint>();
    public List<GameObject> spawnPrefabs = new List<GameObject>();

    private List<GameObject> activeEnemies = new List<GameObject>();
    public List<GameObject> basicProjectiles = new List<GameObject>();
    public void Spawn(int spawnPrefabIndex)
    {
        Spawn(spawnPrefabIndex, 0);
    }

    public void Spawn(int spawnPrefabIndex, int spawnPointIndex)
    {
        GameObject spawnedEnemy = Instantiate(spawnPrefabs[spawnPrefabIndex],
            spawnPoint[spawnPointIndex].self.position,
            spawnPoint[spawnPointIndex].self.rotation) as GameObject;
        //in caz ca scot aiMotor aici trebuie modificat unde sa se duca
        spawnedEnemy.GetComponent<AIMotor>().SetDestination(spawnPoint[spawnPointIndex].destination);
        activeEnemies.Add(spawnedEnemy);
        UIManager.Instance.DrawWaveInfo();
    }

    public void DestroyEnemy(GameObject spawnedEnemy)
    {
        activeEnemies.Remove(spawnedEnemy);
        Destroy(spawnedEnemy);
        UIManager.Instance.DrawWaveInfo();
    }

    public int GetEnemiesLeft()
    {
        return activeEnemies.Count;
    }

    //Temporary
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Spawn(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Spawn(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Spawn(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Spawn(3);
    }

}
