using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildManager2 : MonoBehaviour, IBuildManager
{


    public GameObject LastSelectedTurret { get; set; }
    public GameObject SelectedTurret { get; set; }
    public GameObject SelectedBase { get; set; }
    public Transform SelectedTurretBase { get; set; }
    public GameObject StandardTurretPrefab { get; set; }
    public void Initialize()
    {
        Debug.Log(gameObject.name);
        SelectedTurret = StandardTurretPrefab;
        SelectedTurretBase = null;
    }

    public void ChangeSelectedTurretBaseColor(Color newColor)
    {
        if (SelectedTurretBase != null)
        {
            SelectedTurretBase.GetComponent<TurretPlatform>().UpdateColor(newColor);
        }
    }

    public GameObject GetTurretToBuild()
    {
        if (SelectedTurret == null)
        {
            Debug.LogError("Selected turret is null.");
            return null;
        }

        return SelectedTurret;
    }
}
