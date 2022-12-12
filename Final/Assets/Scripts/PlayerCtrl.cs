using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //이동 속도 변수
    public float moveSpeed = 10.0f;
    //회전 속도 변수
    public float rotSpeed = 90.0f;
    //캡슐콜라이더 변수
    CharacterController cc;
    //이동 거리
    Vector3 dis;

    void Start()
    {
        //캡슐 콜라이더 컴포넌트 얻기
        cc = GetComponent<CharacterController>();
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

        //전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //Translate(이동방향 * 속도 * 변위값 * Time.deltaTime, 기준좌표)
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        //Vector3.up 축을 기준으로 rotSpeed만큼의 속도로 회전
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
            //중력 적용
            dis.y -= 9.8f * Time.deltaTime;
        }
    }
}
