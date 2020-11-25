using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tutle_Attack : MonoBehaviour // 거북이 프리펩 가시 
{
    [SerializeField]
    private float speed; // 날아가는 속도 
    [SerializeField]
    private int damage; //맞았을때 데미지 


    
    public StatusControler status;
   
    void Start()
    {
        Debug.Log(this.gameObject.name);
        status = FindObjectOfType<StatusControler>(); // 이런게 다 찾아서 끌어오는것. 

    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        Destroy(this.gameObject, 4.0f);
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            
            status.DecreaseHP(damage);
            Destroy(this.gameObject);
        }
    }
    
 
        
    
}
