using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField]
    private float dragon_HP; // 드래곤 체력 
    [SerializeField]
    private GameObject dragon_Attack; // 드래곤 공격 프리펩
    [SerializeField]
    private GameObject dragon_Second; // 두번쨰 드래곤 소환
    //오디오 클립 
    [SerializeField]
    private AudioClip dragon_Die; // 드래곤 죽는 소리
    [SerializeField]
    private AudioClip dragon_Get_Damage; // 드래곤 데미지 입는 소리

    private AudioSource audio; // 오디오 소스

    private Vector3 direction;
    private Rigidbody rigid;

    private float currentCreatTime;
    public float attack_Time;

    [SerializeField]
    private float Counter_Speed;

    //불 값
    private bool isDead = false;

    [SerializeField]
    private GameObject Player;


    void Start()
    {
        dragon_Second.gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (!isDead)
        {
            currentCreatTime += Time.deltaTime;

            Quaternion look = Quaternion.LookRotation(Player.transform.position - this.transform.position);
            transform.rotation = look;
            if (currentCreatTime >= attack_Time)
            {

                var clone = Instantiate(dragon_Attack, transform.position, look);
                Destroy(clone, 3.0f);
                currentCreatTime = 0;

            }
        }
    }
    public void Damage(int _dmg, Vector3 _targetPos) 
    {
        dragon_HP -= _dmg;
        Counter(_targetPos);
        PlaySound(dragon_Get_Damage); 

        if (dragon_HP <= 0)
        {
            isDead = true;
            PlaySound(dragon_Die);
            this.gameObject.SetActive(false);
            dragon_Second.gameObject.SetActive(true);


            return;
        }

    }
    private void Counter(Vector3 _targetPos)
    {

        direction = Quaternion.LookRotation(transform.position + _targetPos).eulerAngles;
        rigid.MovePosition(transform.position + (transform.forward * Counter_Speed * Time.deltaTime));
        Debug.Log("반격중");
    }
    public void PlaySound(AudioClip _clip)
    {
        audio.clip = _clip;
        audio.Play();
    }

    private void Rotation() 
    {
        Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
        rigid.MoveRotation(Quaternion.Euler(_rotation));
    }
}
