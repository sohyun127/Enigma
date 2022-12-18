using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperCtrl : MonoBehaviour
{
    //총알 프리팹
    public GameObject bullet;
    //탄피 추출 파티클
    public ParticleSystem cartridge;
    //총알 발사 좌표
    public Transform firePos;
    //오디오 클립을 저장할 변수
    public AudioClip fireSound;


    //총구 화염 파티클
    ParticleSystem muzzleFlash;
    //AudioSource 컴포넌트를 저장할 변수
    AudioSource _audio;
    // 수류탄 발사 위치
    public GameObject bombPosition;
    // 수류탄 무기 오브젝트
    public GameObject bombFactory;
    // 수류탄 투척 파워
    public float throwPower = 15f;

    //총알의 충돌 정보
    private RaycastHit hitInfo;
    //무기 모드 텍스트
    public Text wModeText;
    GameObject enemy;
    //Enemy Damamge 컴포넌트 가져오기
    EnemyDamage EnemyDamage;
    //스나이퍼 공격 데미지
    int power = 30;
    //피격시 사용할 혈흔 효과
    GameObject bloodEffect;
    //재장전 여부
    bool isReload = false;


    //무기 모드 변수
    enum WeaponMode
    { 

        Normal,
        Sniper
    
    }

    WeaponMode wMode;

    //카메라 확대 확인용 변수
    bool ZoomMode = false;

    private void Start()
    {
        //firePos 하위에 있는 컴포넌트 추출
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        //AudioSource 컴포넌트 추출
        _audio = GetComponent<AudioSource>();

        //무기 기본 모드를 노멀 모드로 설정한다
        wMode = WeaponMode.Normal;

        //enemydamage 컴포넌트 불러오기
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        EnemyDamage = enemy.GetComponent<EnemyDamage>();

        //혈흔 효과 프리팹을 로드
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
    }

    private void Update()
    {
        //마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        // 만일 키보드의 숫자 1번 입력을 받으면, 무기 모드를 일반 모드로 변경한다.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            wMode = WeaponMode.Normal;
            // 카메라의 화면을 다시 원래대로 돌려준다.
            Camera.main.fieldOfView = 60f;

            //일반 모드 텍스트 출력
            wModeText.text = "Normal Mode";
        }
        // 만일 키보드의 숫자 2번 입력을 받으면, 무기 모드를 스나이퍼 모드로 변경한다.
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            wMode = WeaponMode.Sniper;

            //스나이퍼 모드 텍스트 출력
            wModeText.text = "Sniper Mode";
        }

        // 마우스 오른쪽 버튼을 입력받는다.
        if (Input.GetMouseButtonDown(1))
        {
            switch (wMode)
            {
                //노멀 모드:마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 수류탄을 던지고 싶다.
                case WeaponMode.Normal:
                    // 수류탄 오브젝트를 생성한 후 수류탄의 생성 위치를 발사 위치로 한다.
                    GameObject bomb = Instantiate(bombFactory);
                    bomb.transform.position = bombPosition.transform.position;

                    // 수류탄 오브젝트의 Rigidbody 컴포넌트를 가져온다.
                    Rigidbody rb = bomb.GetComponent<Rigidbody>();
                    // 카메라의 정면 방향으로 수류탄에 물리적인 힘을 가한다.
                    rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

                    break;
                //스나이퍼 모드: 마우스 오른쪽 버튼을 누르면 화면을 확대하고 싶다.
                case WeaponMode.Sniper:
                    //만일, 줌 모드 상태가 아니라면 카메라를 확대하고 줌 모드 상태로 변경한다.
                    if(!ZoomMode)
                    {
                        Camera.main.fieldOfView = 15f;
                        ZoomMode = true;
                    }
                    //그렇지 않다면 카메라를 원래 상태로 되돌리고 줌 모드 상태를 해제한다.
                    else
                    {
                        Camera.main.fieldOfView = 60f;
                        ZoomMode=false;
                    }
                    break;
            }
        }

        //if(GameManager.gm.gState!=GamaManager.GameState.Run)
        //{
           // return;
      //  }



    }


   
    void Fire()
    {
        
        //파티클 실행
        cartridge.Play();
        //총구 화염 파티클 실행
        muzzleFlash.Play();
        //사운드 발생
        _audio.PlayOneShot(fireSound, 1.0f);
        Hit();
    }


    private void Hit()
    {
        //카메라 월드 좌표
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hitInfo))
        {
            Debug.Log(hitInfo.transform.name);

            if(hitInfo.collider.tag=="Enemy")
            {
                //enemy에게 데미지
                EnemyDamage.HitEnemy(power);

                //총알이 충돌한 지점 산출
                Vector3 pos = hitInfo.point;
                //총알의 충돌했을 때의 법선 벡터
                Vector3 _normal = hitInfo.normal;
                //총알의 충돌 시 방향 벡터의 회전값 계산
                Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
                //혈흔 효과 생성
                GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
                Destroy(blood, 1.0f);
            }
        }
    }
}
