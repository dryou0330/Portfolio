using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private string monster_name; // 몬스터 구별 
    [SerializeField]
    private int monster_Hp; // 몬스터 Hp 

    [SerializeField]
    private float walkSpeed; // 걷는 속도
    private float currentSpeed; // 현재 스피드

    [SerializeField]
    private float Counter_Speed; // 뛰기 스피드 

    private Vector3 direction;

    private Rigidbody rigid;

    [SerializeField]
    private float walkTime; // 걷는 시간 
    [SerializeField]
    private float waitTime; // 대기 시간 
    private float currentTime; // 현재 시간 
    //상태변수 
    private bool isCounter;
    private bool isAction;
    private bool isWalking;
    private bool isSound;
    private BoxCollider boxcol;

    public bool isDead = false;

    [SerializeField]
    private GameObject Tutle_prefab; // 거북이 공격 

    public float attack_Time;
    private float currentCreatTime;

    [SerializeField]
    public float small_Red; // red 란 경계선 
    [SerializeField]
    public float big_Red;

    [SerializeField]
    private GameObject Red;

    // 오디오 소스 
    [SerializeField]
    public Audio theAudio;

    private AudioSource audio; // 오디오 소스

    //오디오 클립 
    [SerializeField]
    private AudioClip tutle_Die;
    [SerializeField]
    private AudioClip tutle_Get_Damage;

    private EnemyAI nevMesh;
   

    void Start()
    {
        nevMesh = GetComponent<EnemyAI>();
        rigid = GetComponent<Rigidbody>();
        boxcol = GetComponent<BoxCollider>();
        currentTime = waitTime;
        isAction = true;
        isSound = false;
        audio = GetComponent<AudioSource>();
        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            theTime();
            Rotation();
            BoundarySmall();
            currentCreatTime += Time.deltaTime;
            BoundaryBig();
        }

        
    }
    public void PlaySound(AudioClip _clip)
    {
        audio.clip = _clip;
        audio.Play();
    }

    public void BoundaryBig() // 경계
    {
        Collider[] col = Physics.OverlapSphere(transform.position, big_Red);
        if (col.Length > 0) // 하나 이상의 오브젝트가 있다 
        {
            for (int i = 0; i < col.Length; i++)
            {
                Transform tf_Target = col[i].transform; // 콜라이여 였던것들이 트랜스폼으로 들어감 .

                if (tf_Target.tag == "Player")
                {
                    Quaternion look = Quaternion.LookRotation(tf_Target.position - this.transform.position); // 플레이를 바라보도록 
                    transform.rotation = look;
                    Red.SetActive(true);
                    
                }        

            }
        }
    }

    private void BoundarySmall() // 전투
    {

        Collider[] col = Physics.OverlapSphere(transform.position, small_Red);//,1 << LayerMask.NameToLayer("Player"));
        if (col.Length > 0) // 하나 이상의 오브젝트가 있다 
        {
            for (int i = 0; i < col.Length; i++)
            {
                Transform tf_Target = col[i].transform; // 콜라이여 였던것들이 트랜스폼으로 들어감 .
                
                if (tf_Target.tag == "Player") // 태그 다 거르기

                {
                    Quaternion look = Quaternion.LookRotation(tf_Target.position - this.transform.position); // 플레이를 바라보도록 

                    transform.rotation = look;
                    
                    if (currentCreatTime >= attack_Time)
                    {
                    
                        var clone = Instantiate(Tutle_prefab, transform.position, look);
                        Destroy(clone, 3.0f);
                        currentCreatTime = 0;
                       
                    }
                }
            }
        }
    }

    private void Rotation() // 터틀, 슬라임 회전 
    {
        if(isWalking)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }
    private void theTime() // 액션을 취하기 위한 시간 체크
    {
        if(isAction)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                direction.Set(0f, Random.Range(0f, 360f), 0f);
       
            }

        }
    }

    public void Damage(int _dmg, Vector3 _targetPos) // 몬스터가 데미지를 입다. 
    {
        monster_Hp -= _dmg;
        Counter(_targetPos);
        PlaySound(tutle_Get_Damage); // 데미지 입는 소리 
        
        if (monster_Hp <= 0) // 피가 0 이하가 되버렸어. 
        {
            isDead = true;
            if(monster_name == "Slime")
            {
                nevMesh.SlimDead();
            }
                  
            PlaySound(tutle_Die); // 죽는소리 
            rigid.velocity = transform.up * 15.0f;
            Destroy(this.gameObject, 3f);

            return;
        }
        
    }

    private void Counter(Vector3 _targetPos) // 몬스터 반격
    {
        
        direction = Quaternion.LookRotation(transform.position + _targetPos).eulerAngles;
        rigid.MovePosition(transform.position + (transform.forward * Counter_Speed * Time.deltaTime));
        // 오일러는 쿼터니엄-> 벡터값으로
        // 플레이어쪽으로 바라본다. 
        Debug.Log("반격중");
    }
}
