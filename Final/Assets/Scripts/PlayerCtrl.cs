using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //�̵� �ӵ� ����
    private float moveSpeed;
    //�ȱ� �ӵ� ����
    public float walkSpeed = 3.0f;
    //�ٱ� �ӵ� ����
    public float runSpeed = 5.0f;
    //ȸ�� �ӵ� ����
    public float rotSpeed = 90.0f;
    //���� ���� ũ��
    public float JumpPower = 5;
    //��ü �ν��Ͻ�
    Rigidbody rb;
    //��� ���θ� �Ǵ��� ����
    public bool isDie = false;
    //Animator ������Ʈ�� ������ ����
    Animator animator;

 

  
    void Start()
    {
        //��ü ������Ʈ ���
        rb = GetComponent<Rigidbody>();

        //Ŀ�� ���߱�
        Cursor.visible = false;
        //Ŀ�� ��ġ ����
        Cursor.lockState = CursorLockMode.Locked;

      

        //Animator ������Ʈ ����
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
        Move();
        Jump();


        while (!isDie)
        {
            //�÷��̾ �����δٸ�
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                
                //�÷��̾ shiftŰ�� �����ٸ�
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = runSpeed;//�̵� �ӵ��� �޸��� �ӵ��� ��ȯ
                    animator.SetBool("IsRun", true);//�޸���
                    animator.SetBool("IsMove", false);
                    break;
                }
                else
                {
                    moveSpeed = walkSpeed;//�̵� �ӵ��� �ȱ� �ӵ��� ��ȯ
                    animator.SetBool("IsRun", false);
                    animator.SetBool("IsMove", true);
                    break;
                }
            }
            else
            {
                animator.SetBool("IsRun", false);
                animator.SetBool("IsMove", false);
                break;
            }

        }


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
        //���� �����̰�, ���� ��ư(�����̽���)�� ������
        if (Mathf.Abs(rb.velocity.y) < 0.001f && Input.GetButtonDown("Jump"))
            rb.AddForce(0, JumpPower, 0, ForceMode.Impulse); //Y�� �������� power�� ���� ����
    }
}
