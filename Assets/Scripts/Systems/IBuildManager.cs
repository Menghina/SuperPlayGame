using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildManager
{
    public GameObject LastSelectedTurret { get; set; }
    public GameObject SelectedTurret { get; set; }
    public GameObject SelectedBase { get; set; }
    public Transform SelectedTurretBase { get; set; }
    public GameObject StandardTurretPrefab { get; set; }

    void Initialize();
    void ChangeSelectedTurretBaseColor(Color newColor);
    GameObject GetTurretToBuild();
}
