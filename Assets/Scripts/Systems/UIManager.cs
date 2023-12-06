using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
[System.Serializable]

public class UIManager : MonoSingleton<UIManager>
{
    private IBuildManager buildManager;

    public GameObject RangeIndicator;

    public List<TurretSelectionButton> turretSelectionButtons;

    // the turret info fields
    public const string TowerInfoName = "Turret: ";
    public const string TowerInfoAtkSpeed = "AttackSpeed: ";
    public const string TowerInfoAtkRange = "AttackRange: ";
    public const string TowerInfoDamage = "Damage: ";
    public const string TowerInfoExtra = "Extra: ";
    public const string BuyTower = "BUY: ";
    public const string UpgradeTower = "UPGRADE: ";
    public const string SellTower = "SELL: ";
    public const int MainMenuSceneIndex = 0;
    //level infos
    public GameObject root;
    public GameObject levelInfo;
    private Text[] levelInfoText;
    //button infos

    public GameObject MainTowerPannel;
    public GameObject TowerPrefabs;
    public GameObject towerInfo;
    private Text[] turretInfoText;
    public GameObject SellMenu;
    public BuyButton BuyButton;
    public GameObject SellButton;
    public GameObject UpgradeButton;
    public GameObject RangeCheckButton;
    public GameObject DefeatCanvas;

    public void Initialize(IBuildManager buildManager)
    {
        this.buildManager = buildManager;

        foreach (TurretSelectionButton turretSelectionButton in turretSelectionButtons)
        {
            turretSelectionButton.Initalize(buildManager);
        }

        SellButton.GetComponent<SellButton>().Initalize(buildManager);
        BuyButton.Initialize(buildManager);
    }
    public class GameEvents
    {
        public UnityEvent OnGameStart = new UnityEvent();
        public UnityEvent OnGameOver = new UnityEvent();
    }
    public void TowerShopMenu()
    {
        BuyButton.gameObject.SetActive(false);
        SellButton.SetActive(true);
        UpgradeButton.SetActive(true);
    }

    public void SetBuyButtonText(string txt)
    {
        BuyButton.gameObject.GetComponentInChildren<Text>().text = BuyTower + txt;
    }

    public void TurretBaseShopMenu()
    {
        BuyButton.gameObject.SetActive(true);
        SellButton.SetActive(false);
        UpgradeButton.SetActive(false);
    }

    public void SetMenuForTower()
    {
        MainTowerPannel.SetActive(true);
        TowerPrefabs.SetActive(true);
        towerInfo.SetActive(false);
        SellMenu.SetActive(false);
    }
    public void SetMenuForTowerButton()
    {
        towerInfo.SetActive(true);
        SellMenu.SetActive(true);
        TurretBaseShopMenu();
    }

    public override void Init()
    {
        levelInfoText = levelInfo.GetComponentsInChildren<Text>();
        towerInfo.SetActive(false);
        MainTowerPannel.SetActive(false);
        DefeatCanvas.SetActive(false);
        RangeIndicator.SetActive(false);
        RangeCheckButton.SetActive(false);
    }

    public void DrawWaveInfo()
    {
        levelInfoText[0].text = "Current wave : " + LevelManager.Instance.GetWaveInfo();
        levelInfoText[1].text = "Enemies Left : " + SpawnManager.Instance.GetEnemiesLeft();
    }

    public void DrawLivesRemainingInfo()
    {
        levelInfoText[2].text = "Lives : " + LevelManager.Instance.Lives;
    }

    public void DrawResourcesRemainingInfo()
    {
        levelInfoText[3].text = LevelManager.Instance.Resources + " $ ";
    }

    public void UpdateTowerInfo(TurretStats turretStats)
    {

        //facut clasa towerinfo displayer + functie cu showinfo
        if (turretStats == null)
        {
            Debug.Log("UpdateTowerInfo function ->> BaseTurret is null");
        }
        turretInfoText = towerInfo.GetComponentsInChildren<Text>();
        turretInfoText[0].text = UIManager.TowerInfoName + turretStats.turretName;
        turretInfoText[1].text = UIManager.TowerInfoAtkSpeed + turretStats.attackCooldown;
        turretInfoText[2].text = UIManager.TowerInfoAtkRange + turretStats.attackRange;
        turretInfoText[3].text = UIManager.TowerInfoDamage + turretStats.damage;
        turretInfoText[4].text = UIManager.TowerInfoExtra;
        UIManager.Instance.towerInfo.SetActive(true);
    }

    public void RangeButtonPressed()
    {
        if (RangeIndicator.activeSelf == false)
        {
            Vector3 positionAdjust = new Vector3(0, -0.8f, 0);
            RangeIndicator.transform.position = buildManager.LastSelectedTurret.transform.position + positionAdjust;
            float turretRange = buildManager.LastSelectedTurret.GetComponent<BaseTurret>().attackRange;
            RangeIndicator.transform.localScale = new Vector3(turretRange * 2, 4, turretRange * 2);
            RangeIndicator.SetActive(true);
        }
        else
        {
            RangeIndicator.SetActive(false);
        }
    }

    public void Defeat()
    {
        DefeatCanvas.SetActive(true);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneIndex);
    }

}
