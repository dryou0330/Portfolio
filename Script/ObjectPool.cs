using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour // 탄피, 총알 , 총구 이펙트 , 몬스터 공격 (슬라임 , 터틀 , 드래곤)
{
    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    private Queue<GunController> poolingObjectQueue = new Queue<GunController>();
    private void Awake()
    {
        Instance = this;
        Initialize(10);
    }

    private GunController CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<GunController>();
        newObj.gameObject.SetActive(false);
        return newObj;

    }

    private void Initialize(int _count)
    {
        for(int i = 0; i <_count; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }
    public static GunController GetObject()
    {
        if(Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newobj = Instance.CreateNewObject();
            newobj.transform.SetParent(null);
            newobj.gameObject.SetActive(true);
            return newobj;
        }
    }
    public static void ReturnObject(GunController bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(bullet);
    }

}
