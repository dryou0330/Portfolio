using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlmeSound : MonoBehaviour
{
    [SerializeField]
    private float lange;

    [SerializeField]
    private Audio audio;

    [SerializeField]
    private Monster monster;



    void Update()
    {
        Play();
        Play_Die();
    }


    private void Play()
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
                    audio.current_Music = audio.tutle_Fight;
                    audio.PlayWP();
                }
            }
        }
    }
    private void Play_Die()
    {
        if(monster.isDead)
        {
            audio.current_Music = audio.backGround_Sound; // 사운드도 바꿨으면 좋겠다
            audio.PlayWP();
        }
    }

}