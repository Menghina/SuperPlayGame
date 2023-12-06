using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SellButton : MonoBehaviour
{
    private IBuildManager buildManager;
    public Button thisButton;
    private GameObject turret;

    public void Initalize(IBuildManager buildManager)
    {
        this.buildManager = buildManager;
    }

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(SellTower);
    }

    public void SellTower()
    {
        turret = buildManager.LastSelectedTurret;

        if (IsTurretValid())
        {
            SellTurret(turret);
        }
    }

    private bool IsTurretValid()
    {
        if (turret == null)
        {
            Debug.LogError("Turret object is null.");
            return false;
        }

        BaseTurret baseTurret = turret.GetComponent<BaseTurret>();

        if (baseTurret == null)
        {
            Debug.LogError("BaseTurret component not found on turret.");
            return false;
        }

        return true;
    }

    private void SellTurret(GameObject turret)
    {
        BaseTurret baseTurret = turret.GetComponent<BaseTurret>();

        LevelManager.Instance.Resources += baseTurret.saleValue;
        UIManager.Instance.DrawResourcesRemainingInfo();

        TurretPlatform turretBase = baseTurret.TowerBase.GetComponent<TurretPlatform>();
        turretBase.IsOccupied = false;

        CombatTextManager.Instance.Show(
            $"+{baseTurret.saleValue}$",
            100,
            Color.green,
            turret.transform.position + new Vector3(0, 2, 0),
            Vector3.up,
            3f
        );

        Destroy(turret);
    }
}
