using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GunController : MonoBehaviour
{
    
    
    [SerializeField]
    public Gun currentGun; // Gun 클래스 불러오기. SerializeField 로.

    private float currentFireRate; // 현재 연사속도

    private AudioSource audiosource; // 오디오 소스 

    private bool isReload = false;

    private RaycastHit hitinfo; //충돌 정보 받아옴 .

    [SerializeField]
    private GameObject hit_Effect; //이펙트를 GameObject로 받아옴 .

    [SerializeField]
    private Camera theCamera;

    [SerializeField]
    private AudioClip reRoad;

    [SerializeField]
    private GameObject bullet_Case;

    [SerializeField]
    private Transform bulletCase_Pos;

    [SerializeField]
    private GameManager gamemanager;

    //정조준 

    [SerializeField]
    private Vector3 originPos; // 본래 위치 

    public bool isFineSightMode = false;

    /*void OnDrawGizmos() // 기즈모 그리기 (반동 기능 추가 할 때 확인하기 위해서)
    {
        Vector3 Pos = transform.position + originPos;
        
        Vector3 no_sight  = new Vector3(originPos.x, originPos.y, -currentGun.retroActionForce);
        no_sight = transform.position + no_sight;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Pos, 0.5f);
       
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(no_sight, 0.5f);
        Gizmos.color = Color.white;
    }*/

    private void Start()
    {
        audiosource = GetComponent<AudioSource>(); //호출 

        originPos = transform.localPosition;
    }

  
    void Update()
    {
        GunFireRateCalc(); // 연사속도 계산
        TryFire(); // 발사
        TryReload(); // 재장전 
        TryFineSight(); // 정조준 
    }

    private void TryFineSight()
    {
        if(Input.GetMouseButtonDown(1) && !isReload)
        {
            FineSight();
        }
    }
    private void FineSight()
    {
        isFineSightMode = !isFineSightMode; //알아서 자동으로 true false 바뀌도록 스위치 
        currentGun.anim.SetBool("FineSightMode", isFineSightMode);

        if(isFineSightMode) // 정조준 할때
        {
            StopAllCoroutines();
            StartCoroutine(FineSightOk());
            currentGun.fireRate -= 0.07f;
            currentGun.range += 10f;
        }
        else // 아닐때
        {
            StopAllCoroutines();
            StartCoroutine(FineSightNo());
            currentGun.fireRate += 0.07f;
            currentGun.range -= 10f;
        }
    }
    IEnumerator FineSightOk()
    {
        
        while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }
    IEnumerator FineSightNo()
    {
        
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }

    private void GunFireRateCalc() // 연사 속도 
    {
        if (currentFireRate > 0) 
        {
            currentFireRate -= Time.deltaTime; // 역순으로 --> 즉 currentFireRate가 높을수록 연사속도는 낮다. 
        }
    }

    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && isReload == false && (currentGun.currentBulletCount < currentGun.reloadBulletCount))
        {
            
            StartCoroutine(ReloadCoroutine());
        }
    }

    private void TryFire()
    {
        if (Input.GetMouseButton(0) && currentFireRate <= 0 && isReload == false && !gamemanager.isOpenInventory) // 왼쪽버튼 누르며 연사속도 가 0 보다 작거나 같을때 실행
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (currentGun.currentBulletCount > 0) // 탄알이 하나라도 있을때 실행 
        {
            Shoot(); // 진짜 발사
            PlaySE(currentGun.fire_Sound); // 총 소리 
        }
        else
            StartCoroutine(ReloadCoroutine()); // 재장전
      
    }

    private void Shoot()
    {
        currentGun.currentBulletCount--; // 발사할때마다 총알 하나씩 까먹기
        currentFireRate = currentGun.fireRate; //currentGun 클래스에서 총의 연사속도 받아오기. 
        currentGun.muzzleFlash.Play(); // 동시에 이펙트 발사
        Hit();
        HitCase();

        // 총기 반동 코루틴 실행 
        StopAllCoroutines();
        StartCoroutine("RetroAction");
    }
    
    IEnumerator RetroAction() // 반동
    {
        Vector3 no_sight = new Vector3(originPos.x, originPos.y , -currentGun.retroActionForce); // No 정조준 
        

        if (!isFineSightMode) // 정조준 아닐때
        {
            currentGun.transform.localPosition = originPos; // 연출적느낌으로 처음위치로 돌린다
            

            while (currentGun.transform.localPosition.z <= currentGun.retroActionForce - 0.02f) // 원위치에서 레트로 위치 까지 
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, no_sight, 0.2f); //Vecter3.Lerp =>  (처음 위치 , 옮길 위치 , 속도) ; 
                yield return null;
            }
            while (currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
                
            }
            
        } 
        
    }
    
    
    private void HitCase() // 탄피
    {
        GameObject intanCase = Instantiate(bullet_Case, bulletCase_Pos.position, bulletCase_Pos.rotation);
        Destroy(intanCase, 3.0f);
        
        Vector3 caseVec = bulletCase_Pos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        Rigidbody caseRigid = intanCase.GetComponent<Rigidbody>();
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }

    private void Hit() // 맞춤 
    {
        if (Physics.Raycast(theCamera.transform.position, theCamera.transform.forward, out hitinfo, currentGun.range)) // currentGun.range << 사정거리 
        {
            var clone = Instantiate(hit_Effect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal)); //ObjectPool.GetObject(); // 오브젝트 풀링
            //Invoke("DestroyBullet", 0.5f);
                                  // Instantiate(hit_Effect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal)); 
                                  // var 말고 GameObject로 받아도됌 식별자를 알고있으니까. 
            Destroy(clone, 0.5f); // 0,5f 만에 바로 사라지게 만들어서 탄흔의 느낌이 나도록. 구현 
                                  // 바로 사라지게 하지않으면 이펙트가 계속 땅에 남는다. 만약 늦게 사라지게 하고싶으면
                                  // 이펙트 반복을 꺼주면 될것 같음. 

            if (hitinfo.transform.tag == "Monster") // Tutle, Slime
            {
                hitinfo.transform.GetComponent<Monster>().Damage(currentGun.damage, transform.position);
            }
            else if (hitinfo.transform.tag == "NPC") // 돼지 NPC 
            {
                hitinfo.transform.GetComponent<PigAI>().Damage(currentGun.damage, transform.position);
            }
            else if (hitinfo.transform.tag == "Dragon")
            {
                hitinfo.transform.GetComponent<Dragon>().Damage(currentGun.damage, transform.position);
            }
        }
    }
    private void DestroyBullet()
    {
        ObjectPool.ReturnObject(this);
    }

    public void PlaySE(AudioClip sound)
    {
        audiosource.clip = sound;
        audiosource.Play(); // 재생 
    }

    IEnumerator ReloadCoroutine() // 재장전 
    {
        isReload = true; // 플래그 리로드 중 

        PlaySE(reRoad);

        currentGun.anim.SetTrigger("Reload");

        currentGun.currentBulletCount = currentGun.reloadBulletCount; // 현재 총알 갯수를 재장전 갯수만큼    

        yield return new WaitForSeconds(currentGun.reloadTime); // 재장전 시간 

        isReload = false; //플래그 리로드 끝 
    }

    public Gun GetGun() // 호출의 호출 !
    {
        return currentGun;
    }


}
