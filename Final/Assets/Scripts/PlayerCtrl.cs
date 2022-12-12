using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //이동 속도 변수
    private float moveSpeed;
    //걷기 속도 변수
    public float walkSpeed = 3.0f;
    //뛰기 속도 변수
    public float runSpeed = 5.0f;
    //회전 속도 변수
    public float rotSpeed = 90.0f;
    //점프 힘의 크기
    public float JumpPower = 5;
    //강체 인스턴스
    Rigidbody rb;
    //사망 여부를 판단할 변수
    public bool isDie = false;
    //Animator 컴포넌트를 저장할 변수
    Animator animator;

 

  
    void Start()
    {
        //강체 컴포넌트 얻기
        rb = GetComponent<Rigidbody>();

        //커서 감추기
        Cursor.visible = false;
        //커서 위치 고정
        Cursor.lockState = CursorLockMode.Locked;

      

        //Animator 컴포넌트 추출
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
        Move();
        Jump();


        while (!isDie)
        {
            //플레이어가 움직인다면
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                
                //플레이어가 shift키를 누른다면
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = runSpeed;//이동 속도를 달리기 속도로 변환
                    animator.SetBool("IsRun", true);//달리기
                    animator.SetBool("IsMove", false);
                    break;
                }
                else
                {
                    moveSpeed = walkSpeed;//이동 속도를 걷기 속도로 변환
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

        //전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //Translate(이동방향 * 속도 * 변위값 * Time.deltaTime, 기준좌표)
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        //Vector3.up 축을 기준으로 rotSpeed만큼의 속도로 회전
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

    }

    private void Jump()
    {
        //정지 상태이고, 점프 버튼(스페이스바)을 누르면
        if (Mathf.Abs(rb.velocity.y) < 0.001f && Input.GetButtonDown("Jump"))
            rb.AddForce(0, JumpPower, 0, ForceMode.Impulse); //Y축 방향으로 power의 힘을 가함
    }
}
