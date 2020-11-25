using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static int score = 0;

    static int level = 1;

    public static void setScore(int _count) // 경험치를 얻는다.
    {
        score += _count; // 몬스터한마리 잡을떄마다 setScore() 실행  몬스터가 죽을때 실행.

        Debug.Log("현재 경험치:  " + score);

        

        if(score % 20 == 0)
        {
            level++;
            
            print("현재 레벨 :  " + level);
        }
        
    }

    public static int getScore() // 이 Score 함수를 플레이어에게 전달 플레이어에서 Score함수 실행시 경험치 획득. 
    {
        return score;
    }
    
    public static int getLevel() // 이 Score 함수를 플레이어에게 전달 플레이어에서 Score함수 실행시 경험치 획득. 
    {
        return level;
    }


}
