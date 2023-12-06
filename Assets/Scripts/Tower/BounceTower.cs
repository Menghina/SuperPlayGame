using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTower : BaseTurret
{
    public GameObject core;
    public GameObject projectile;

    protected override void Action(Transform target)
    {
        lastAction = Time.time;


        GameObject bullet = Instantiate(projectile, core.transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<BaseProjectile>().LaunchPosition = core.transform.position;
        bullet.GetComponent<BaseProjectile>().Launch(core.transform, target, towerDamageInfo);
    }
}

