using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //�̵� �ӵ� ����
    public float moveSpeed = 10.0f;
    //ȸ�� �ӵ� ����
    public float rotSpeed = 90.0f;
    //ĸ���ݶ��̴� ����
    CharacterController cc;
    //�̵� �Ÿ�
    Vector3 dis;

    void Start()
    {
        //ĸ�� �ݶ��̴� ������Ʈ ���
        cc = GetComponent<CharacterController>();

        //Ŀ�� ���߱�
        Cursor.visible = false;
        //Ŀ�� ��ġ ����
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Move();
        Jump();
    }


    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        //�����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //Translate(�̵����� * �ӵ� * ������ * Time.deltaTime, ������ǥ)
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        //Vector3.up ���� �������� rotSpeed��ŭ�� �ӵ��� ȸ��
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

    }

    private void Jump()
    {
        if(cc.isGrounded)
        {
            if (Input.GetButton("Jump"))
                dis.y = 7;
            else
                dis.y = 0;
        }
        else
        {
            //�߷� ����
            dis.y -= 9.8f * Time.deltaTime;
        }
    }
}
