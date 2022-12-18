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
        playerDamage = player.GetComponent<PlayerDamage>();
    }

    void OnCollisionEnter(Collision coll)
    {

        if (coll.collider.tag == "Player")
        {
            if (playerDamage.currHp < 100f)
            {
                animator.SetBool("IsHeal", true);
            }
        }

    }
   
}
