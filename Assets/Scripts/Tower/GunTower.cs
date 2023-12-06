using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTower : BaseTurret
{
    public GameObject[] fireHoles;
    public GameObject projectile;

    protected override void Action(Transform target)
    {
        lastAction = Time.time;

        for( int i = 0; i < fireHoles.Length; i++)
        {
            GameObject bullet = Instantiate(projectile, fireHoles[i].transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<BaseProjectile>().LaunchPosition = fireHoles[i].transform.position;
            bullet.GetComponent<BaseProjectile>().Launch(fireHoles[i].transform, target, towerDamageInfo);
        }
    }
}
