using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{

    //생명 게이지
    public float hp = 100.0f;
    //피격시 사용할 혈흔 효과
    GameObject bloodEffect;
    //생명 게이지 프리팹을 저장할 변수
    public GameObject hpBarPrefab;
    //생명 게이지 위치를 보정할 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //부모가 될 canvas 객체
    private Canvas uiCanvas;
    //생명 수치에 따라 fillAmount 속성을 변경할 image
    private Image hpBarImage;

    void Start()
    {
        //혈흔 효과 프리팹을 로드
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
        //생명 게이지의 생성 및 초기화
        SetHpBar();
    }


    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag=="Bullet")
        {
            //혈흔 효과를 생성하는 함수 호출
            ShowBloodEffect(coll);
            //총알 삭제
            Destroy(coll.gameObject);
           
            BulletCtrl bc=coll.gameObject.GetComponent<BulletCtrl>();
            if(bc!=null)
            {
                //생명 게이지 차감
                hp -= bc.damage;
                //생명 게이지의 fillAmount 속성을 변경
                hpBarImage.fillAmount = hp / 100.0f;
            }

            if(hp<=0.0f)
            {
                //적 캐릭터의 상태를 Die로 변경
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
                //적 캐릭터가 사망한 이후 생명 게이지를 투명 처리
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                
            }
        }


        //혈흔 효과를 생성하는 함수
        void ShowBloodEffect(Collision coll)
        {
            //총알이 충돌한 지점 산출
            Vector3 pos = coll.contacts[0].point;
            //총알의 충돌했을 때의 법선 벡터
            Vector3 _normal = coll.contacts[0].normal;
            //총알의 충돌 시 방향 벡터의 회전값 계산
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
            //혈흔 효과 생성
            GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
            Destroy(blood, 1.0f);
        }
    }

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        //Ui canvas 하위로 생명 게이지를 생성
        GameObject hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);
        //fillAmount 속성을 변경할 Image를 추출
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];
        //생명 게이지가 따라가야 할 대상과 오프셋 값 설정
        EnemyHpBar bar = hpBar.GetComponent<EnemyHpBar>();
        bar.targetTr = gameObject.transform;
        bar.offset = hpBarOffset;
    }

    //수류탄 데미지 적용
    public void HitEnemy(int power)
    {
        hp -= power;
        //생명 게이지의 fillAmount 속성을 변경
        hpBarImage.fillAmount = hp / 100.0f;

        if (hp <= 0.0f)
        {
            //적 캐릭터의 상태를 Die로 변경
            GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            //적 캐릭터가 사망한 이후 생명 게이지를 투명 처리
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;

        }
    }

}
