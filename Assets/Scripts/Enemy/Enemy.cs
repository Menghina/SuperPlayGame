using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public float damage;
    public Color damageTextColor;
}

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;

    [SerializeField]
    protected int worth;
    [SerializeField]
    protected float maxHitpoints;
    protected float currentHitpoints;
    [SerializeField]
    protected float baseMovementSpeed;
    public float MaxHitpoints { set { MaxHitpoints = value; } get { return MaxHitpoints; } }
    public float CurrentHitpoints { set { currentHitpoints = value; } get { return currentHitpoints; } }
    public float BaseMovementSpeed { get { return baseMovementSpeed; } }
    public int Worth { get { return Worth; } }


    private void EnrichData()
    {
        maxHitpoints = enemyData.maxHitpoints;
        worth = enemyData.worth;
        baseMovementSpeed = enemyData.baseMovementSpeed;
    }

    private void Awake()
    {
        //this will be used in inhertitted classes later on
        EnrichData();
    }
    private void Start()
    {
        InitCombat();
    }
    public virtual void InitCombat()
    {
        currentHitpoints = maxHitpoints;
    }

    public virtual void OnDamage(DamageInfo dmgInfo)
    {
        CombatTextManager.Instance.Show(dmgInfo.damage.ToString(), 100, dmgInfo.damageTextColor, transform.position, Vector3.up, 3f);
        currentHitpoints -= dmgInfo.damage;
        if (currentHitpoints <= 0)
        {
            OnDeath();
        }
    }
    public virtual void OnDeath()
    {
        LevelManager.Instance.getRewardOnKill(worth);
        Debug.Log(name + "has died");
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
