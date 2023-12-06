using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour
{
    public GameObject TowerBase;

    public TurretStats turretStats;

    public Transform partToRotate_Yaxis;
    public Transform partToRotate_Zaxis;
    public float turnSpeed = 10f;

    public bool requiresRotatingGun;
    public int buildCost;
    public int saleValue;
    public DamageInfo towerDamageInfo;
    public float attackCooldown;
    public float attackRange;

    protected float lastTick;
    protected float refreshrRate = 0.10f;
    protected float lastAction;

    protected Transform target;


    protected virtual void  Awake()      
    {
        EnrichTowerData();
    }
    protected void EnrichTowerData()
    {
        if (turretStats != null)
        {
            towerDamageInfo = new DamageInfo();
            towerDamageInfo.damage = turretStats.damage;
            towerDamageInfo.damageTextColor = turretStats.damageColour;
            buildCost = turretStats.buildCost;
            saleValue = turretStats.saleValue;
            attackCooldown = turretStats.attackCooldown;
            attackRange = turretStats.attackRange;
            requiresRotatingGun = turretStats.requiresRotatingGun;
        }
        else
        {
            Debug.LogWarning("TurretStats is null in EnrichTowerData");
        }
    }

    protected virtual void Update()
    {
        if (target != null && requiresRotatingGun)
        {
            LockOnTarget(target);
        }
        //if the tower is ready to shoot
        if (Time.time - lastAction > attackCooldown)
        {
            //refresh every 0.1 sec to find a target;
            {
                if (Time.time - lastTick > refreshrRate)
                {
                    lastTick = Time.time;
                    // get a target
                    target = GetNearestEnemy();
                    if (target != null)
                    {
                        Action(target);
                    }
                }
            }
        }
    }
    protected virtual Transform GetNearestEnemy()
    {
        Collider[] allEnemies = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        if (allEnemies.Length != 0)
        {
            int closestIndex = 0;
            float nearestDistance = Vector3.SqrMagnitude(transform.position - allEnemies[0].transform.position);
            for (int i = 0; i < allEnemies.Length; i++)
            {
                float distance = Vector3.SqrMagnitude(transform.position - allEnemies[i].transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    closestIndex = i;
                }
            }
            return allEnemies[closestIndex].transform;
        }
        return null;
    }
    protected virtual void Action(Transform target)
    {
        lastAction = Time.time;
        Debug.Log(gameObject.name + "is shooting at " + target.name);
    }

    protected virtual void LockOnTarget(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotationY = Quaternion.Lerp(partToRotate_Yaxis.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate_Yaxis.rotation = Quaternion.Euler(0f, rotationY.y, 0f);
        partToRotate_Zaxis.rotation = Quaternion.Euler(rotationY.x, rotationY.y, 0f);
    }
}
