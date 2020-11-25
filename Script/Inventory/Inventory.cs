using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    [SerializeField]
    private GameObject inventoryBase;
    [SerializeField]
    private GameObject slotsParent; // 그리드 

    [SerializeField]
    private GameObject menu;


    private Slot[] slots;
    void Start()
    {

        slots = slotsParent.GetComponentsInChildren<Slot>(); // 슬롯안으로 그리드 자식이 들어간다
    }


    void Update()
    {
        OpenInventory();
        
    }
    private void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
            {
                inventoryBase.SetActive(true);
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
                inventoryBase.SetActive(false);
            }
                
        }
    }

    public bool InvenReturn()
    {
        return inventoryActivated;
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        for (int i = 0; i <slots.Length; i++ ) // 아이템이있으면 갯수만 워주고
        {
            if(slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            } 
        }

        for (int i = 0; i < slots.Length; i++) // 아이템이 없으면 빈자리 찾아서 채워주기.
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}

