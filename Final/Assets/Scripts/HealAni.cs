using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAni : MonoBehaviour
{
     Animator animator;
     GameObject player;
    PlayerDamage playerDamage;

     void Start()
    {
        //Animator 컴포넌트 추출
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //playerdamage 컴포넌트 추출
        playerDamage = player.GetComponent<PlayerDamage>();
    }

    void OnCollisionEnter(Collision coll)
    {
        //player와 충돌하면
        if (coll.collider.tag == "Player")
        {
            if (playerDamage.currHp < 100f)
            {
                //상자 열리는 애니메이션 
                animator.SetBool("IsHeal", true);
            }
        }

    }
   
}
