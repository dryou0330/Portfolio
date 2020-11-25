using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour , IPointerClickHandler
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 갯수
    public Image itemImage; // 아이템 이미지 

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;


    private void SetColor(float _color) // 이미지 투명도 조절 
    {
        Color color = itemImage.color;
        color.a = _color;
        itemImage.color = color;
    }
    public void AddItem(Item _item , int _count = 1) // 아이템 획득
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

   
        go_CountImage.SetActive(true);
        text_Count.text = itemCount.ToString();

        SetColor(1);

    }

    public void SetSlotCount(int _count) // 아이템 갯수조정 
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }

    private void ClearSlot() // 슬롯 초기화
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        
        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                
                Debug.Log(item.itemName + "을 사용 했습니다.");
                SetSlotCount(-1);
            }
        }
    }
}
