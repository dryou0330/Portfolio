using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StatusControler : MonoBehaviour
{
    // 체력을 어떻게 회복할 것인가 ? 의 대한 고민 

    [SerializeField]
    private int hp; // 최대치 

    private int CurrentHp; //현재 체력 

    [SerializeField]
    private int sp; // 스테미나 

    private int CurrentSp; // 현재 스테미나 

    [SerializeField]
    private int IncreaseSpeed;// 스테미나 증가량

    [SerializeField]
    private int spRechargeTime; // 재회복 딜레이 

    private int currentSprechargeTime; 

    private bool SpUsed;

    [SerializeField]
    private GameObject Player1;

    [SerializeField]
    private Image[] images_Gauge; // 필요한 이미지

    void Start()
    {
        CurrentHp = hp;
        CurrentSp = sp;
    }

    void Update()
    {
        GaugeUpdate();
        SprechargeTime();
        SPRecover();
    }

    public void IncreaseHP(int _count) // 회복 합니다. 
    {
        if (CurrentHp + _count < hp)
        {
            CurrentHp += _count;
        }
        else
        {
            CurrentHp = hp;
        }
    }
    public void DecreaseHP(int _count) // 공격당했을때
    {
        
        CurrentHp -= _count;
        if(CurrentHp <= 0)
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("Game Over...");
              
            }
            SceneManager.LoadScene(2);
            //Player1.SetActive(false);
            
        }
    }

    private void GaugeUpdate() // 이미지 관리
    {
        images_Gauge[0].fillAmount = (float)CurrentHp / hp;
        images_Gauge[1].fillAmount = (float)CurrentSp / sp;

    }
    public void DecreaseStamina(int _count) // 스태미나 감소 
    {
        SpUsed = true;
        currentSprechargeTime = 0;

        if(CurrentSp - _count > 0)
        {
            CurrentSp -= _count;
        }
        else
        {
            CurrentSp = 0;
        }
    }
    private void SprechargeTime() // 스태미나 회복 딜레이 
    {
        if(SpUsed)
        {
            if(currentSprechargeTime < spRechargeTime)
            {
                currentSprechargeTime++;
            }
            else
            {
                SpUsed = false;
              
            }
        }
    }
    private void SPRecover() //스태미나 회복 
    {
        if (CurrentSp < sp && !SpUsed)
        {
            CurrentSp += IncreaseSpeed;
        }
    }
    public int GetCurrentSP() // 스태미나 상태 확정 
    {
        return CurrentSp;
    }
}
