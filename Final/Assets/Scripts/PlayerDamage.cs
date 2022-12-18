using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public float currHp = 100;
    //BloodScreen �ؽ�ó�� �����ϱ� ���� ����
    public Image bloodScreen;
    //Hp Bar Image�� �����ϱ� ���� ����
    public Image hpBar;
    //���� �������� ó�� ����(���)
    Color initColor = new Vector4(0, 0.1f, 0.0f, 1.0f);
    Color currColor;


    void Start()
    {
        //���� �������� �ʱ� ������ ����
        hpBar.color = initColor;
        currColor = initColor;
    }

    void OnCollisionEnter(Collision coll)
    {
        //�浹�� Collider�� �±װ� Bullet�̸� Player�� currHp�� ����
        if(coll.collider.tag =="Bullet")
        {
            
            Destroy(coll.gameObject);
            currHp -= 5.0f;
            Debug.Log("Player Hp = " + currHp.ToString());
            //Player�� ������ 0�����̸� ��� ó��
            if(currHp <= 0.0f)
            {
                PlayerDie();
            }

            //���� ȿ���� ǥ���� �ڷ�ƾ �Լ� ȣ��
            StartCoroutine(ShowBloodScreen());
            //���� �������� ���� �� ũ�� �����Լ��� ȣ��
            DisplayHpbar(); 
        }
    }


    //Player�� ��� ó�� ��ƾ
    void PlayerDie()
    {
        Debug.Log("PlayerDie!!");
    }

   IEnumerator ShowBloodScreen()
    {
        //BloodScrren �ؽ�ó�� ���İ��� �ұ�Ģ�ϰ� ����
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.5f, 1.0f));
        yield return new WaitForSeconds(0.1f);
        //BloodScreen �ؽ�ó�� ������ ��� 0���� ����
        bloodScreen.color = Color.clear;
    }

    void DisplayHpbar()
    {
        float ratio = currHp / 100.0f;
        //���� ��ġ�� 50%�� �������� ������� ��������� ����
        if (ratio > 0.5f)
            currColor.r = (1 - ratio) * 2.0f;
        else//���� ��ġ�� 0%�϶������� ��������� ���������� ����
            currColor.g = ratio * 2.0f;
        //HpBar�� ���� ����
        hpBar.color = currColor;
        //HpBar�� ũ�� ����
        hpBar.fillAmount = ratio;
    }
}
