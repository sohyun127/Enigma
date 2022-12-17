using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    //�� ĳ������ ���¸� ǥ���ϱ� ���� ������ ���� ����
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    //���¸� ������ ����
    public State state= State.PATROL;
    //���� �����Ÿ�
    public float attackDist = 5.0f;
    //���� �����Ÿ�
    public float traceDist = 10.0f;
    //��� ���θ� �Ǵ��� ����
    public bool isDie = false;
    //���ΰ��� ��ġ�� ������ ����
    Transform playerTr;
    //�� ĳ������ ��ġ�� ������ ����
    Transform enemyTr;
    //�ڷ�ƾ���� ����� �����ð� ����
    WaitForSeconds ws;
    //�̵��� �����ϴ� MoveAgent Ŭ������ ������ ����
    MoveAgent moveAgent;
    //Animator ������Ʈ�� ������ ����
    Animator animator;
    //�� �߻縦 �����ϴ� EnemyFire Ŭ������ ������ ����
    EnemyFire enemyFire;


    void Awake()
    {
        //���ΰ����ӿ�����Ʈ ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //���ΰ��� Transform ������Ʈ ����
        if(player!=null)
        {
            playerTr = player.transform;
        }
        //�� ĳ������ Transform ������Ʈ ����
        enemyTr=GetComponent<Transform>();
        //�̵��� �����ϴ� MoveAgent Ŭ������ ����
        moveAgent = GetComponent<MoveAgent>();
        //Animator ������Ʈ ����
        animator = GetComponent<Animator>();
        //�� �߻縦 �����ϴ� EnemyFire Ŭ������ ����
        enemyFire=GetComponent<EnemyFire>();
        //Cycle Offset ���� �ұ�Ģ�ϰ� ����
        animator.SetFloat("Offset", Random.Range(0.0f, 1.0f));
        //Speed ���� �ұ�Ģ�ϰ� ����
        animator.SetFloat("WalkSpeed",Random.Range(1.0f, 1.2f));
        //�ڷ�ƾ�� �����ð� ����
        ws = new WaitForSeconds(0.3f);
    }

    void Update()
    {
        //Speed �Ķ���Ϳ� �̵� �ӵ��� ����
        animator.SetFloat("Speed", moveAgent.GetSpeed());
    }
    void OnEnable()
    {
        //CheckState �ڷ�ƾ �Լ� ����
        StartCoroutine(CheckState());
        //Action �ڷ�ư �Լ� ����
        StartCoroutine(Action());
    }

    //�� ĳ������ ���¸� �˻��ϴ� �ڷ�ƾ �Լ�
    IEnumerator CheckState()
    {
        //�� ĳ���Ͱ� ����ϱ� ������ ���� ���ѷ���
        while(!isDie)
        {
            //���°� ����̸� �ڷ�ƾ �Լ��� �����Ŵ
            if (state == State.DIE) yield break;
            //���ΰ��� �� ĳ���� ���� �Ÿ��� ���
            float dist=Vector3.Distance(playerTr.position, enemyTr.position);
            if(dist<=attackDist)
                state= State.ATTACK;
            else if(dist<=traceDist)
                state= State.TRACE;
            else
                state= State.PATROL;
            //0.3�� ���� ����ϴ� ���� ������� �纸
            yield return ws;
        }
    }

    //���¿� ���� ��ĳ������ �ൿ�� ó���ϴ� �ڷ�ƾ �Լ�
    IEnumerator Action()
    {
        while(!isDie)
        {
            yield return ws;

            switch(state) //���¿� ���� �б� ó��
            {
                case State.PATROL:
                    //���� ��带 Ȱ��ȭ
                    enemyFire.isFire = false;
                    moveAgent.SetPatrolling(true);
                    animator.SetBool("IsMove", true);
                    break;
                case State.TRACE:
                    //���ΰ��� ��ġ�� �Ѱ� ���� ���� ����
                    moveAgent.SetTraceTarget(playerTr.position);
                    animator.SetBool("IsMove", true);
                    if(enemyFire.isFire == false)
                        enemyFire.isFire = true;
                    break;
                case State.ATTACK:
                    moveAgent.Stop(); //���� �� ������ ����
                    animator.SetBool("IsMove", false);
                    if (enemyFire.isFire == false)
                        enemyFire.isFire = true;
                    break;
                case State.DIE:
                    isDie = true;
                    enemyFire.isFire = false;
                    moveAgent.Stop(); //���� �� ������ ����
                    animator.SetTrigger("Die");//��� �ִϸ��̼� ����
                    GetComponent<CapsuleCollider>().enabled = false; //Capsule Collider ������Ʈ�� ��Ȱ��ȭ
                    break;


            }
        }
    }

}
