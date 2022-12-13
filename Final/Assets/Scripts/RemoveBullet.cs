using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    //����ũ �������� ������ ����
    public GameObject sparkEffect;

    //�浹�� ������ �� �߻��ϴ� �̺�Ʈ
    void OnCollisionEnter(Collision coll)
    {
       //�浹�� ���� ������Ʈ�� �±װ� ��
       if(coll.collider.tag=="Bullet")
        {
            //����ũ ȿ�� �Լ� ȣ��
            ShowEffect(coll);
            //�浹�� ���ӿ�����Ʈ ����
            Destroy(coll.gameObject);
        }
    }

    void ShowEffect(Collision coll)
    {
        //�浹�� ������ ������ ����
        ContactPoint contact = coll.contacts[0];
        //���� ���Ͱ� �̷�� ȸ�������� ����
        Quaternion rot = Quaternion.FromToRotation(Vector3.back, contact.normal);
        //����ũ ȿ���� ����
        GameObject spark = Instantiate(sparkEffect, contact.point - (contact.normal * 0.05f), rot);
        //����ũ ȿ���� �θ� �浹�� ��ü�� ����
        spark.transform.SetParent(this.transform);
    }
}
