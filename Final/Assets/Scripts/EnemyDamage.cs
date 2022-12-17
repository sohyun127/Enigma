using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    //���� ������
    float hp = 100.0f;
    //�ǰݽ� ����� ���� ȿ��
    GameObject bloodEffect;

    void Start()
    {
        //���� ȿ�� �������� �ε�
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
    }


    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag=="Bullet")
        {
            //���� ȿ���� �����ϴ� �Լ� ȣ��
            ShowBloodEffect(coll);
            //�Ѿ� ����
            Destroy(coll.gameObject);
            //���� ������ ����
            hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;

            if(hp<=0.0f)
            {
                //�� ĳ������ ���¸� Die�� ����
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
                
            }
        }


        //���� ȿ���� �����ϴ� �Լ�
        void ShowBloodEffect(Collision coll)
        {
            //�Ѿ��� �浹�� ���� ����
            Vector3 pos = coll.contacts[0].point;
            //�Ѿ��� �浹���� ���� ���� ����
            Vector3 _normal = coll.contacts[0].normal;
            //�Ѿ��� �浹 �� ���� ������ ȸ���� ���
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
            //���� ȿ�� ����
            GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
            Destroy(blood, 1.0f);
        }
    }


}
