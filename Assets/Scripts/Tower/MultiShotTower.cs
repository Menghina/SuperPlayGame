using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotTower : BaseTurret
{

    public GameObject projectile;
    public Transform core;
    protected Transform[] targets;

    protected override void Update()
    {
        //if the tower is ready to shoot
        if (Time.time - lastAction > attackCooldown)
        {
            //refresh every 0.1 sec to find a target;
            {
                if (Time.time - lastTick > refreshrRate)
                {
                    lastTick = Time.time;
                    // get a target
                    targets = GetEnemiesInRange();
                    if (targets != null)
                    {
                        foreach (Transform target in targets)
                            Action(target);
                    }
                }
            }
        }
    }


    protected override void Action(Transform target)
    {
        lastAction = Time.time;
        GameObject bullet = Instantiate(projectile, core.transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<BaseProjectile>().LaunchPosition = core.transform.position;
        bullet.GetComponent<BaseProjectile>().Launch(core.transform, target, towerDamageInfo);
    }

    protected virtual Transform[] GetEnemiesInRange()
    {
        Collider[] allEnemies = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        Transform[] allEnemiesPositions = new Transform[allEnemies.Length];
        if (allEnemies.Length != 0)
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
                allEnemiesPositions[i] = allEnemies[i].transform;
            }
            return allEnemiesPositions;
        }
        return null;
    }

}
