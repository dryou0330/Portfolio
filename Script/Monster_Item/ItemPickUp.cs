using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{

    [SerializeField]
    private float range; // 아이템 습득 거리

    private bool pickUpActivated = false; // 습득 하고 있는 상태 X 

    private RaycastHit hitinfo; // 충돌체 정보 저장 

    [SerializeField]
    private LayerMask layerMask;  // 땅을 바라보는데 아이템 습득이 되면안되기때문에.

    [SerializeField]
    private Text actionText;  // 필요한 컴포넌트 

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Inventory theinventory;
    void Update()
    {
        TryAction();
        CheckItem();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem(); // 체크 아이템 
            CanPickUp(); // 아이템 습득 행위
        }
    }

    private void CanPickUp()
    {
        if(pickUpActivated)
        {
            if(hitinfo.transform != null)
            {
                Debug.Log(hitinfo.transform.GetComponent<ItemPick>().item.itemName + " 획득 하였습니다");
                theinventory.AcquireItem(hitinfo.transform.GetComponent<ItemPick>().item);
                //around_start = true;

                hitinfo.transform.gameObject.SetActive(false); // 파괴하지말고 꺼보자
                //Destroy(hitinfo.transform.gameObject); // 습득한 아이템은 파괴 
                

                // 사운드 하나 입히자 습득할때. 빠빠빠빠람 이라던가 
                InfoDisappear(); // 정보는 안보이도록 .
            }
        }
    }

    private void CheckItem()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo , range ,layerMask)) // 로컬상 좌표로 변환 시켜줌 
        {
            if(hitinfo.transform.tag == "Item") // 아이템이 있는지 최종 확인 . 
            {
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
        }
    }
    private void InfoDisappear()
    {
        pickUpActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void ItemInfoAppear()
    {
        pickUpActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitinfo.transform.GetComponent<ItemPick>().item.itemName + " 획득" + "<color= Yellow>" + "(E)" + "</color>";
    }
}
