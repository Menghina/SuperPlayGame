using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LvlButton : MonoBehaviour
{
    public int sceneIndex;
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick); 
    }
    public void TaskOnClick()
    {
        if (SceneManager.sceneCount <= sceneIndex)
            SceneManager.LoadScene(sceneIndex);
    }
}
