using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurretSelectionButton : MonoBehaviour
{
    private IBuildManager buildManager;
    public GameObject towerPrefab;
    private TurretStats turretType;

    UIManager uiManager;

    public void Initalize(IBuildManager buildManager)
    {
        this.buildManager = buildManager;
    }
    private void Start()
    {
        turretType = towerPrefab.GetComponent<BaseTurret>().turretStats;
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
        uiManager = UIManager.Instance;

        if (turretType == null || btn == null || uiManager == null)
        {
            Debug.LogError("TurretSelectionButton -> Start(), one component is null");
        }
    }

    void OnButtonClick()
    {
        uiManager.UpdateTowerInfo(turretType);
        Debug.Log("You have clicked the button!");
        uiManager.SetBuyButtonText(turretType.buildCost + "$");
        buildManager.SelectedTurret = towerPrefab;
        uiManager.SetMenuForTowerButton();
    }

}
