using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool canPlayerMove = true; // 플레이어 움직임 제어 

    public bool isOpenInventory = false;

    public static bool isMenu = false; // 메뉴가 켜지면 true;

    [SerializeField]
    private Inventory invetoryBool;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 게임 시작시 마우스 커서 잠그고, 안보이게 만들기 .
        Cursor.visible = false; // lockState 에 visible = false 도 포함되어 있다. 
    }
    void Update()
    {
        isOpenInventory = invetoryBool.InvenReturn();



        if (isOpenInventory || isMenu) // 인벤토리가 열리면 마우스 커서가 보인다.
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canPlayerMove = false;

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canPlayerMove = true;
        }
    }
}
