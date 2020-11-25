using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject baseUI;
  
    public void Call()
    {
        if (!GameManager.isMenu)
        {
            CallMenu();
        }
        else
        {
            CloseMenu();
        }
        
    }

    private void CallMenu()
    {
        GameManager.isMenu = true;
        baseUI.SetActive(true);
        Time.timeScale = 0f; // 시간의 흐름을 0 배속
    }
    private void CloseMenu()
    {
        GameManager.isMenu = false;
        baseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClickSave()
    {
        Debug.Log("Save");
    }

    public void ClickLoad()
    {
        Debug.Log("Load");
    }

    public void ClickExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
