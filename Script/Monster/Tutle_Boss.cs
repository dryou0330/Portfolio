using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutle_Boss : MonoBehaviour
{
    private Monster monster;
    [SerializeField]
    private GameObject tutle_Item;

    public Audio theaudio;
    void Start()
    {
        monster = GetComponent<Monster>();
        tutle_Item.SetActive(false);
    }


    void Update()
    {
        if (monster.isDead) // 보스 몬스터가 죽었을때, 실행 
        {
            tutle_Item.SetActive(true); // 아이템 띄어라 tutle_Item. 사운드가 있으면 좋을것 같은데

            theaudio.current_Music = theaudio.backGround_Sound; // 사운드도 바꿨으면 좋겠다
            theaudio.PlayWP();
            
        }
    }
}
