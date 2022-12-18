using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{

    //���� ������
    public float hp = 100.0f;
    //�ǰݽ� ����� ���� ȿ��
    GameObject bloodEffect;
    //���� ������ �������� ������ ����
    public GameObject hpBarPrefab;
    //���� ������ ��ġ�� ������ ������
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //�θ� �� canvas ��ü
    private Canvas uiCanvas;
    //���� ��ġ�� ���� fillAmount �Ӽ��� ������ image
    private Image hpBarImage;
    

    void Start()
    {
        //���� ȿ�� �������� �ε�
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
        //���� �������� ���� �� �ʱ�ȭ
        SetHpBar();
    }


    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag=="Bullet")
        {
            //���� ȿ���� �����ϴ� �Լ� ȣ��
            ShowBloodEffect(coll);
            //�Ѿ� ����
            Destroy(coll.gameObject);
           
            BulletCtrl bc=coll.gameObject.GetComponent<BulletCtrl>();
            if(bc!=null)
            {
                //���� ������ ����
                hp -= bc.damage;
                //���� �������� fillAmount �Ӽ��� ����
                hpBarImage.fillAmount = hp / 100.0f;
            }

            if(hp<=0.0f)
            {
                //�� ĳ������ ���¸� Die�� ����
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
                //�� ĳ���Ͱ� ����� ���� ���� �������� ���� ó��
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                Destroy(gameObject,5);

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

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        //Ui canvas ������ ���� �������� ����
        GameObject hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);
        //fillAmount �Ӽ��� ������ Image�� ����
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];
        //���� �������� ���󰡾� �� ���� ������ �� ����
        EnemyHpBar bar = hpBar.GetComponent<EnemyHpBar>();
        bar.targetTr = gameObject.transform;
        bar.offset = hpBarOffset;
    }

    //����ź ������,�������� ������ ����
    public void HitEnemy(int power)
    {
        hp -= power;
        //���� �������� fillAmount �Ӽ��� ����
        hpBarImage.fillAmount = hp / 100.0f;

        if (hp <= 0.0f)
        {
            //�� ĳ������ ���¸� Die�� ����
            GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            //�� ĳ���Ͱ� ����� ���� ���� �������� ���� ó��
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            Destroy(gameObject,5);
        }
    }

}
