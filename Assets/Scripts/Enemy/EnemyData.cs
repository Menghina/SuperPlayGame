using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public int worth;
    public float maxHitpoints;
    public float baseMovementSpeed;
    public Color damageTextColor;
    // Add other attributes as needed
}
