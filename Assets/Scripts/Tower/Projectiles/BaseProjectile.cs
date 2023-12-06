using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public Vector3 LaunchPosition { set; get; }
    public Transform Target { set; get; }
    public Vector3 TargetLocation { set; get; }
    public DamageInfo DmgInfo { set; get; }
    public bool IsLockedOnTarget { set; get; }
    public float TimeToTarget { set; get; }
    private bool isLaunched = false;

    private float transition = 0.0f;

    public BaseProjectile()
    {
        IsLockedOnTarget = false;
        TimeToTarget = 0.3f;
    }

    private void Update()
    {
        if (!isLaunched)
            return;
        //reassign the target position every frame if we are locked on the target;

        transition += Time.deltaTime / TimeToTarget;
        if (transition >= 1.0f)
        {
            ReachTarget();
        }
        if (IsLockedOnTarget && Target)
        {
            TargetLocation = Target.position;
        }
        transform.position = Vector3.Lerp(LaunchPosition, TargetLocation, transition);
    }

    public virtual void Launch(Transform bulletPipe,Transform target, DamageInfo dmg)
    {
        isLaunched = true;
        LaunchPosition = bulletPipe.position;
        Target = target;
        DmgInfo = dmg;
    }

    protected virtual void ReachTarget()
    {
        if (Target != null)
        {
            Target.GetComponent<Enemy>()?.OnDamage(DmgInfo);
        }

        Destroy(gameObject);
    }
}
