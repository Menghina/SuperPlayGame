using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTowerInfo : MonoBehaviour
{
    public IBuildManager buildManager;
    TurretStats towerPrefab;

    public void Initialize(IBuildManager buildManager)
    {
        this.buildManager = buildManager;
    }

    private void Start()
    {
        BaseTurret baseTurret = GetComponent<BaseTurret>();

        if (baseTurret != null)
        {
            towerPrefab = baseTurret.turretStats;
        }
        else
        {
            Debug.LogError("BaseTurret component not found on the game object.");
        }
    }

    private void OnMouseDown()
    {
        ShowTowerInformation();
    }

    private void ShowTowerInformation()
    {
        BaseTurret baseTurret = GetComponent<BaseTurret>();

        if (baseTurret == null)
        {
            Debug.LogError("BaseTurret component not found on the game object.");
            return;
        }

        TurretStats turretPrefab = baseTurret.turretStats;

        UIManager uiManager = UIManager.Instance;

        buildManager.LastSelectedTurret = this.gameObject;
        buildManager.SelectedTurretBase = null;

        uiManager.MainTowerPannel.SetActive(true);
        uiManager.TowerPrefabs.SetActive(false);
        uiManager.towerInfo.SetActive(true);
        uiManager.SellMenu.SetActive(true);
        uiManager.UpdateTowerInfo(turretPrefab);
        uiManager.RangeCheckButton.SetActive(true);
        uiManager.RangeIndicator.SetActive(false);
        uiManager.BuyButton.gameObject.SetActive(false);
        uiManager.UpgradeButton.SetActive(true);
        uiManager.SellButton.SetActive(true);
    }
}
