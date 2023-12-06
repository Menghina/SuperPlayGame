using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseProjectile
{
    public override void Launch(Transform bulletPipe, Transform target, DamageInfo dmg)
    {
        base.Launch(bulletPipe, target, dmg);
        IsLockedOnTarget = true;
    }

}
