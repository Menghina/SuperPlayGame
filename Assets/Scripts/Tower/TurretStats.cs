using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameItems",menuName = "Turret")]
public class TurretStats : ScriptableObject
{
    public string turretName;
    public string description;


    public Sprite artwork;
    public int type;
    public float attackRange;
    public float attackCooldown;
    public float damage;
    public Color damageColour;
    public int buildCost;
    public int saleValue;
    public bool requiresRotatingGun;
    
}
