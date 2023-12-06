using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceProjectile : BaseProjectile
{

    int bounces = 4;
    int bounceRange = 10;
    float newTimeToTarget = 3f;
    DamageInfo bounceDamageInfo;
    public void Start()
    {
        bounceDamageInfo = new DamageInfo();
        bounceDamageInfo.damage = 5;
        bounceDamageInfo.damageTextColor = Color.magenta;

    }
    public override void Launch(Transform bulletPipe, Transform target, DamageInfo dmg)
    {
        base.Launch(bulletPipe, target, dmg);
        TimeToTarget = newTimeToTarget;
        IsLockedOnTarget = true;
    }

    protected virtual Transform GetNearestEnemy(Transform alreadyHit)
    {
        Collider[] allEnemies = Physics.OverlapSphere(transform.position, bounceRange, LayerMask.GetMask("Enemy"));
        if (allEnemies.Length != 0)
        {
            int closestIndex = 0;
            float nearestDistance = Vector3.SqrMagnitude(transform.position - allEnemies[0].transform.position);
            for (int i = 0; i < allEnemies.Length; i++)
            {
                float distance = Vector3.SqrMagnitude(transform.position - allEnemies[i].transform.position);
                if (distance < nearestDistance && allEnemies[i].transform != alreadyHit)
                {
                    nearestDistance = distance;
                    closestIndex = i;
                }
            }
            return allEnemies[closestIndex].transform;
        }
        return null;
    }



    protected override void ReachTarget()
    {
        if (Target)
        {
            Target.gameObject.GetComponent<Enemy>().OnDamage(DmgInfo);
        }
        if (bounces == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Transform nextTarget = GetNearestEnemy(Target);
            if (GetNearestEnemy(Target) != null)
            {
                Launch(transform, nextTarget, bounceDamageInfo);
                bounces--;
            }
        }
        //  gameObject.SetActive(false);
        // transform.position = new Vector3(0, 0, 0);
    }
}
