using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    //총 발사 여부를 판단할 변수
    public bool isFire = false;
    //총 발사 사운드를 저장할 변수
    public AudioClip fireSfx;
    //적 캐릭터의 총알 프리팹
    public GameObject Bullet;
    //총알의 발사 위치 정보
    public Transform firePos;

    //AudioSource 컴포넌트를 저장할 변수
    AudioSource _audio;
    //Animator 컴포넌트를 저장할 변수
    Animator animator;

    //주인공 캐릭터의 Transform 컴포넌트
    Transform playerTr;
    //적 캐릭터의 Transform 컴포넌트
    Transform enemyTr;
    //총구 화염 효과
    ParticleSystem muzzleFlash;
    //다음 발사할 시간 계산용 변수
    float nextFire = 0.0f;
    //총알 발사 간격
    float fireRate = 1.5f;
    //주인공을 향해 회전할 속도 계수
    float damping = 10.0f;
    //총알 발사 속도
    public float speed = 1000.0f;
    //투척

    void Start()
    {
        //컴포넌트 추출 및 변수 저장
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTr=GetComponent<Transform>();
        animator=GetComponent<Animator>();
        _audio=GetComponent<AudioSource>();
        muzzleFlash=firePos.GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
     if(isFire)
        {
            //현재 시간이 다음 발사 시간보다 큰지를 확인
            if(Time.time>=nextFire)
            {
                Fire();
                //다음 발사 시간 계산
                nextFire = Time.time + fireRate + Random.Range(0.0f, 0.5f);
            }

            //주인공이 있는 위치까지의 회전 각도 계산
             Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            //보간 함수를 이용해 점진적으로 회전시킴
             enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
         
        }
    }


    void Fire()
    {
       
        if (!animator.GetBool("IsMove"))
        {
            animator.SetTrigger("Fire");
        }
        _audio.PlayOneShot(fireSfx, 0.5f);
        muzzleFlash.Play();

        //총알을 생성
         GameObject _bullet = Instantiate(Bullet, firePos.position, firePos.rotation);
         Rigidbody rigidBullet = _bullet.GetComponent<Rigidbody>();
         //enemy앞으로 발사
         rigidBullet.AddForce(enemyTr.forward*speed);


        //일정 시간이 지난 후 삭제
        Destroy(_bullet, 3.0f);
    }
}
