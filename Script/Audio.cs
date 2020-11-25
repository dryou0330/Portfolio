using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    
    private AudioSource theAudio; // 오디오 소스 불러오기 
    [SerializeField]
    public AudioClip tutle_Fight; // 오디오 클립 
    [SerializeField]
    public AudioClip backGround_Sound; // 배경음악 클립 

    public AudioClip current_Music;

    [SerializeField]
    public AudioClip front; // 드래곤 동굴 가까워 졌을때

    public float lange;
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        
        current_Music = backGround_Sound;

        PlayWP();
    }

    void Update()
    {
        PlaySE();
        
    }

    public void PlaySE()
    {   
        Collider[] col = Physics.OverlapSphere(transform.position, lange);
        if (col.Length > 0) // 범위안에 하나라도 있으면
        {  
            for (int i = 0; i < col.Length; i++)
            {
                Transform tf_Target = col[i].transform;
                
                if (tf_Target.gameObject.tag == "Player")
                {
                    lange = 0.01f;
                    current_Music = tutle_Fight;
                    PlayWP();     
                }

            } 

        }

           
    }
    public void PlayWP() // 거북이 /슬라임 전투일때 실행
    {
        Debug.Log("현재 진행 되는 곡:  " + current_Music);
        theAudio.clip = current_Music;
        theAudio.Play();
    }
}

