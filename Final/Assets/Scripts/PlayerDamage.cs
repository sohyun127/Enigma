using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public float currHp = 100;
    //BloodScreen 텍스처를 저장하기 위한 변수
    public Image bloodScreen;
    //Hp Bar Image를 저장하기 위한 변수
    public Image hpBar;
    //생명 게이지의 처음 색상(녹색)
    Color initColor = new Vector4(0, 1.0f, 0.0f, 1.0f);
    Color currColor;
    

    void Start()
    {
        //생명 게이지의 초기 색상을 설정
        hpBar.color = initColor;
        currColor = initColor;
    }

    void OnCollisionEnter(Collision coll)
    {
        //충돌한 Collider의 태그가 Bullet이면 Player의 currHp를 차감
        if(coll.collider.tag =="Bullet")
        {
            
            Destroy(coll.gameObject);
            currHp -= 5.0f;
            Debug.Log("Player Hp = " + currHp.ToString());
            //Player의 생명이 0이하이면 사망 처리
            if(currHp <= 0.0f)
            {
                PlayerDie();
            }

            //혈흔 효과를 표현할 코루틴 함수 호출
            StartCoroutine(ShowBloodScreen());
            //생명 게이지의 색상 및 크기 변경함수를 호출
            DisplayHpbar(); 
        }
        
        if(coll.collider.tag == "Heal")
        {
            if(currHp<100f)
            {
                Destroy(coll.gameObject,2.0f);
                if ((currHp + 10f) <= 100)
                    currHp += 10.0f;
                else if ((currHp + 10f) > 100)
                    currHp = 100;

                Debug.Log("구급 상자로 치료되었습니다");
            } 
        }
    }


    //Player의 사망 처리 루틴
    void PlayerDie()
    {
        Debug.Log("PlayerDie!!");
    }

   IEnumerator ShowBloodScreen()
    {
        //BloodScrren 텍스처의 알파값을 불규칙하게 변경
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.5f, 1.0f));
        yield return new WaitForSeconds(0.1f);
        //BloodScreen 텍스처의 색상을 모두 0으로 변경
        bloodScreen.color = Color.clear;
    }

    void DisplayHpbar()
    {
        float ratio = currHp / 100.0f;
        //생명 수치가 50%일 때까지는 녹색에서 노란색으로 변경
        if (ratio > 0.5f)
            currColor.r = (1 - ratio) * 2.0f;
        else//생명 수치가 0%일때까지는 노란색에서 빨간색으로 변경
            currColor.g = ratio * 2.0f;
        //HpBar의 색상 변경
        hpBar.color = currColor;
        //HpBar의 크기 변경
        hpBar.fillAmount = ratio;
    }
}
