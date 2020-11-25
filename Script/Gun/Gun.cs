using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public string gunName; // 총이름 
    public float range; // 사정거리
    public float accuracy; // 정확도
    public float fireRate; // 연사속도
    public float reloadTime; // 재장전 속도

    public int damage; // 데미지

    public int reloadBulletCount; // 총알 재장전 갯수
    public int currentBulletCount; // 현재 탄알집에 남아있는 총알의 갯수

    public ParticleSystem muzzleFlash; // 섬광 이펙트

    public Animator anim;

    public AudioClip fire_Sound; // 발사 사운드

    public float retroActionForce; // 반동세기
    public float retroActionFIneSightForce; // 정조준 반동 세기

    public Vector3 fineSightOriginPos; // 정조준 위치값

    public ParticleSystem levelUp; // 레벨업 이펙트 

    [SerializeField]
    private Text level_Text;

    private int level_score = 20;

    void Update()
    {
        Level();
    }


    public void Level()
    {
        
        int score = ScoreManager.getScore();
        int level = ScoreManager.getLevel();

        if( score % level_score == 0 && score != 0)
        {
            level_score += 20;
            damage += 10;
            range += 3;
            fireRate -= 0.05f;
            level_Text.text = "<color= Yellow>" + "Level : " + "</color>" + level.ToString();
            levelUp.Play();
        }

    }
}
