using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent enemy; // 적의 네비메쉬

    [SerializeField]
    Transform[] wayPoint; // 정찰 포인트 ( Vecter3 의 값을 받는거지 ) 
    int count = 0;

    Transform target; // 주인공 (Player) 

    public bool check = true; // 살아있다 몬스터가 

    void MoveToNextPoint() // 다음 지역 이동 
    {
        if(target == null && check) // 타켓이 없으면 자유롭게 정찰 포인트를 움직이며 다녀야 겠지 
        {
            if (enemy.velocity == Vector3.zero) // 속도가 0 이 되면 
            {
                enemy.SetDestination(wayPoint[count++].position); // 셋데스티네이션 지역으로 순찰 순차적으로

                if (count >= wayPoint.Length) // 움직임이 포인트 보다 많으면 한바퀴 다돌았으니 초기화
                {
                    count = 0;
                }
            }
        }
   
    }
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        InvokeRepeating("MoveToNextPoint", 0f, 2f); // 함수는 시작과 동시에 2초마다 실행 
    }

    public void SetTarger(Transform _targer)
    {
        CancelInvoke();
        target = _targer;
    }

    public void RemoveTarger()
    {
        target = null;
        InvokeRepeating("MoveToNextPoint", 0f, 2f);
    }
    void Update()
    {
        if (target != null)
        {
            enemy.SetDestination(target.position);
        }
        
    }
    public void SlimDead()
    {
        check = false;
        Debug.Log("네비메시가꺼졌습니다.");
        enemy.enabled = false; // 네비 끄기 
        target = null; // 타켓도 없다 
        wayPoint = null; // 웨어 포인트도 없다 

    }    
}
