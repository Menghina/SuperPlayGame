using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            LevelManager.Instance.EnemyCrossed();
            SpawnManager.Instance.DestroyEnemy(col.gameObject);
        }
    }
}
