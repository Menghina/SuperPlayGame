using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoSingleton<MainMenuManager>
{
    public GameObject MaineMenuCanvas;
    public GameObject LevelSelectMenu;
    public GameObject SelectLevelPanel;

    private void Start()
    {        
        int buttonText = 1;
        foreach (Transform child in SelectLevelPanel.transform)
        {
            if (child.tag == "LevelSelectButton")
            {
                Button btn = child.gameObject.GetComponent<Button>();
                btn.GetComponent<LvlButton>().sceneIndex = buttonText;
                btn.GetComponentInChildren<Text>().text = ""+ buttonText;
                buttonText++;
            }
        }
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void SelectLevelMenu()
    {
        MaineMenuCanvas.SetActive(false);
        LevelSelectMenu.SetActive(true);
    }

}
