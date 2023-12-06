using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyButton : MonoBehaviour
{
    private IBuildManager buildManager;
    public Button thisButton;
    private GameObject turret;

    public void Initialize(IBuildManager buildManager)
    {
        this.buildManager = buildManager;
    }
    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(BuyTower);
    }
    void BuyTower()
    {
        BaseTurret towerToBuild = buildManager.SelectedTurret.GetComponent<BaseTurret>();
        if (CanAffordTower(towerToBuild))
        {
            DeductResources(towerToBuild.buildCost);
            BuildTower(towerToBuild);
            ShowCombatText(towerToBuild.buildCost);
            HideUIAndResetSelection();
        }
    }

    bool CanAffordTower(BaseTurret tower)
    {
        return LevelManager.Instance.Resources >= tower.buildCost;
    }

    void DeductResources(int cost)
    {
        LevelManager.Instance.Resources -= cost;
        UIManager.Instance.DrawResourcesRemainingInfo();
    }

    void BuildTower(BaseTurret towerToBuild)
    {
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = Instantiate(turretToBuild, buildManager.SelectedTurretBase.position, buildManager.SelectedTurretBase.rotation);
        turret.GetComponent<BaseTurret>().TowerBase = buildManager.SelectedTurretBase.gameObject;
        buildManager.SelectedTurretBase.GetComponent<TurretPlatform>().BackToBaseColor();
        buildManager.SelectedTurretBase.GetComponent<TurretPlatform>().IsOccupied = true;
        buildManager.SelectedTurretBase = null;
    }

    void ShowCombatText(int amount)
    {
        CombatTextManager.Instance.Show("-" + amount + "$", 14, Color.red, turret.transform.position + new Vector3(0, 2, 0), Vector3.up, 3f);
    }

    void HideUIAndResetSelection()
    {
        UIManager.Instance.MainTowerPannel.SetActive(false);
    }
}
