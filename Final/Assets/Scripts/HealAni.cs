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
        //Animator ������Ʈ ����
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //playerdamage ������Ʈ ����
        playerDamage = player.GetComponent<PlayerDamage>();
    }

    void OnCollisionEnter(Collision coll)
    {
        //player�� �浹�ϸ�
        if (coll.collider.tag == "Player")
        {
            if (playerDamage.currHp < 100f)
            {
                //���� ������ �ִϸ��̼� 
                animator.SetBool("IsHeal", true);
            }
        }

    }
   
}
