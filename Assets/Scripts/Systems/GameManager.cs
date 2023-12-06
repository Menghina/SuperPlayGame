using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<TurretPlatform> turretPlatforms;

    public BuildManager buildManager;
    // public BuildManager2 buildManager2; //only for show purpose
    public UIManager uiManager;

    public void Awake()
    {
        // Pass dependencies to other components
        buildManager.Initialize();
        uiManager.Initialize(buildManager);

        foreach (var t in turretPlatforms)
        {
            t.Initialize(buildManager);
        }
    }
}
