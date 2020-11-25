using FPSControllerLPFP;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed; // 걷는속도
    [SerializeField]
    private float runSpeed; // 달리는 속도
    private float applySpeed; // 적용 속도

    private bool isRun = false; // 플래그 상태변수
    private bool isGround = true;
    private bool isCrouch = false;

    [SerializeField]
    private float jumpForce; // 뛰는 힘 

    private CapsuleCollider capsulecollider; // 2단 점프 방지

    [SerializeField]
    private float crouchSpeed; // 앉은속도
    

    [SerializeField]
    private float crouchPosY; // 앉았을때 카메라 위치
    private float originPosY; // 앉기전 카메라 Original 위치
    private float applyCrouchPosY; // 적용 위치

    [SerializeField]
    private float Sensitivity; // 카메라 상하 민감도

    [SerializeField]
    private float cameraRotationLimit; // 카메라 상하 한계값
    private float currentCameraRotationX; // 현재 카메라 위치 

    [SerializeField]
    private Camera theCamara; // 카메라 연결 

    private Rigidbody Rigid; // 리지드 바디

    private StatusControler thsSatatusController; // 상태 클래스 호출 (Hp , 스태미너 조절) 

    [SerializeField]
    private Gun gun;

    [SerializeField]
    private GunController guncontroller;

    [SerializeField]
    private AudioClip run_Sound;

     void Start()
    {
        Rigid = GetComponent<Rigidbody>(); // 리지드 바디 연결
        applySpeed = walkSpeed; // 시작 스피드 는 걷는 스피드. 
        capsulecollider = GetComponent<CapsuleCollider>(); // 이중 방지를 위한 콜라이더 받기 
        originPosY = theCamara.transform.localPosition.y; // 카메라 로컬 포지션 y를 받기 
        applyCrouchPosY = originPosY; // 시작할땐 서있는 상태여야 하니까 
        thsSatatusController = FindObjectOfType<StatusControler>();
    }

    
    void Update()
    {
        
        if(GameManager.canPlayerMove)
        {
            TryRun(); // 달리기 
            Move(); // 움직임 함수
            CameraRotation(); // X 값 회전 ( 카메라 상하 ) 
            CharacterRotation(); // Y 값 회전 ( 캐릭터 좌우)
            TryJump(); // 점프 시도
            IsGround(); // 땅에 붙어있는가?
            TryCrouch(); // 앉기 시도 
        }

    }

    //외부 충돌에 의해 리지드바디의 회전 속력이 발생한 것 수리 
    void FixedUpdate()
    {
        FreezeRotation();
    }
    void FreezeRotation()
    {
        Rigid.angularVelocity = Vector3.zero;
    }


    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch(); // 앉기 
        }
    }

    private void Crouch() // 앉기 
    {
        isCrouch = !isCrouch; // 스위치역할 

        if(isCrouch) // 앉고 있냐 ? 
        {
            applySpeed = crouchSpeed; // 그럼 앉은 속도로 바꿔라
            applyCrouchPosY = crouchPosY; // 적용 카메라를 앉은 카메라로 바꿔라.
            
        }
        else
        {
            applySpeed = walkSpeed; 
            applyCrouchPosY = originPosY;
           
        }

        StartCoroutine(CrouchCroutine()); 
    }
    
    IEnumerator CrouchCroutine() // 앉기 코루틴 
    {
        float _PosY = theCamara.transform.localPosition.y; 
        int count = 0;  
        
        while(_PosY != applyCrouchPosY) // 완전히 앉기 까지 돌아라
        {
            count++;
            _PosY = Mathf.Lerp(_PosY, applyCrouchPosY, 0.3f); // 기본 거리에서 앉은 거리까지 0.3f 의 속도로 내려간다. 부드럽게 
            theCamara.transform.localPosition = new Vector3(theCamara.transform.localPosition.x, _PosY, theCamara.transform.localPosition.z);
            if (count > 15)
                break;
            yield return null;
        }

    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && thsSatatusController.GetCurrentSP() > 0 && !isCrouch) // 왼쪽 쉬프트를 누르고 있으면 실행 스태미나가 0 이상일때
        {     
            Run();
            thsSatatusController.DecreaseStamina(10);
            
        }    
      
        if(Input.GetKeyUp(KeyCode.LeftShift) || thsSatatusController.GetCurrentSP() <= 0) // 키가 올라왔을때나 스태미나가 0 보다 작을때 
        {
            
            RunningCancel(); //뛰는거 그만
        }
    }

    private void IsGround() // 땅에 붙어있냐 
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsulecollider.bounds.extents.y + 0.1f);
    }    

    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround && thsSatatusController.GetCurrentSP() > 0 && !guncontroller.isFineSightMode)
        {
            Jump();
        }
    }
    
    private void Jump()
    {
        //앉은 상태
        if (isCrouch)
        {
            Crouch();
        }
        thsSatatusController.DecreaseStamina(500); // 점프하면 500 씩 깎아라 
        Rigid.velocity = transform.up * jumpForce; // 벨로시티로 위쪽으로 힘을 준다 . 
    }

    private void Run() // 달린다
    {

        isRun = true; // 상태변수 변환 
        applySpeed = runSpeed; // 적용스피드를 달리기로 
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    private void Move()
    {
        if(!guncontroller.isFineSightMode)
        {
            float _moveDirX = Input.GetAxisRaw("Horizontal"); // X축 받아오기
            float _moveDirZ = Input.GetAxisRaw("Vertical"); // Z축 받아오기

            Vector3 _moveHorizontal = transform.right * _moveDirX;
            Vector3 _moveVertical = transform.forward * _moveDirZ;

            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed; // X Z 값 저장

            Rigid.MovePosition(transform.position + _velocity * Time.deltaTime); // MovePosition 사용 
                                                                                 // 리지드. 벨로시티 사용 X 물리적 작용때문에 
        }

          
    }

    private void CameraRotation() // 상하 
    {
        float _xRotation = Input.GetAxis("Mouse Y");
        float _cameraRotationX = _xRotation * Sensitivity;
        currentCameraRotationX += _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit); 

        theCamara.transform.localEulerAngles = new Vector3 ( -currentCameraRotationX , 0f, 0f);
    }
    private void CharacterRotation() // 좌우
    {
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _characterRotionY = new Vector3(0f, _yRotation, 0f) * Sensitivity; // Vecter 값
        Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(_characterRotionY)); // Vecter 값을 Quaternion.Euler 값으로 변환 MoveRotation 은 Quaternion 값 이기때문에.
    }


}
