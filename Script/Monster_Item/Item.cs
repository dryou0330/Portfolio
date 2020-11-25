using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/ item")]
public class Item : ScriptableObject // 게임오브젝트에 붙일 필요가 없음 
{
    public string itemName; // 아이템 이름 
    public Sprite itemImage; // 아이템 이미지
    public GameObject itemPrefab; // 아이템 프리펩 

    public string weaponType; // 아이템 유형 

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC

    }
}
