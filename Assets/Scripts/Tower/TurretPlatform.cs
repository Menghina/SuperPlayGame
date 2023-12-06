using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlatform : MonoBehaviour
{
    private IBuildManager buildManager;

    public Color hoverColor;
    private Color startColor;
    private Color selectedColor;
    private Renderer rend;
    [SerializeField]
    private bool isOccupied;
    public bool IsOccupied { get { return isOccupied; }set { isOccupied = value; } }

    private GameObject turret;


    public void Initialize(IBuildManager buildManager)
    {
        this.buildManager = buildManager;
    }
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        isOccupied = false;
        selectedColor = Color.green;
    }

    public void UpdateColor(Color newColor)
    {
        if (!isOccupied)
            rend.material.color = newColor;
    }
    private void OnMouseEnter()
    {
        UpdateColor(hoverColor);
    }

    public void BackToBaseColor()
    {
        UpdateColor(startColor);
    }

    private void OnMouseExit()
    {
        if (!isOccupied)
        {
            if (buildManager.SelectedTurretBase == transform)
            {
                rend.material.color = selectedColor;
            }
            else
            {
                rend.material.color = startColor;
            }
        }
    }

    private void OnMouseDown()
    {
        if (isOccupied == false)
        {
            if (buildManager.SelectedTurretBase != null)
            {
                if (buildManager.SelectedTurretBase != transform)
                {
                    buildManager.ChangeSelectedTurretBaseColor(selectedColor);
                }
            }
            }
            buildManager.SelectedTurretBase = transform;
            if (turret != null)
            {
                Debug.Log("Can'tBuild there!");
                return;
            }
            rend.material.color = selectedColor;
            UIManager.Instance.SetMenuForTower();
            UIManager.Instance.TurretBaseShopMenu();
            UIManager.Instance.RangeIndicator.SetActive(false);
            UIManager.Instance.RangeCheckButton.SetActive(false);

    }
}
