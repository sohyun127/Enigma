using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
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


    private void Start()
    {
        //firePos 하위에 있는 컴포넌트 추출
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        //AudioSource 컴포넌트 추출
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        // 마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 수류탄을 던지고 싶다.
        // 마우스 오른쪽 버튼을 입력받는다.
        if (Input.GetMouseButtonDown(1))
        {
            // 수류탄 오브젝트를 생성한 후 수류탄의 생성 위치를 발사 위치로 한다.
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = bombPosition.transform.position;

            // 수류탄 오브젝트의 Rigidbody 컴포넌트를 가져온다.
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            // 카메라의 정면 방향으로 수류탄에 물리적인 힘을 가한다.
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

    }

    void Fire()
    {
        //Bullet 프리팹을 동적으로 생성
        Instantiate(bullet, firePos.position, firePos.rotation);

        //파티클 실행
        cartridge.Play();
        //총구 화염 파티클 실행
        muzzleFlash.Play();
        //사운드 발생
        _audio.PlayOneShot(fireSound, 1.0f);
    }
}
