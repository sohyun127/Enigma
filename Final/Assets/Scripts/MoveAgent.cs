using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    //���� �������� �����ϱ� ���� ListŸ���� ����
    public List<Transform> wayPoints;
    //���� ���� ������ �迭�� Index
    public int nextIdx;
    //NavMeshAgent agent;
    NavMeshAgent agent;

    float patrolSpeed = 1.5f; //���� �ӵ�
    float traceSpeed = 4.0f; //���� �ӵ�

    //���� ���θ� �Ǵ��ϴ� ����
    bool patrolling;
    //���� ����� ��ġ�� �����ϴ� ����
    Vector3 traceTarget;

    void Start()
    {
        //NavMeshAgent ������Ʈ�� ������ �� ������ ����
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        //���̷�Ű ���� WayPointGroup ���ӿ�����Ʈ�� ����
        GameObject group = GameObject.Find("WayPointGroup");
        if(group != null)
        {
            //WayPointGroup ������ �ִ� ��� Transform ������Ʈ�� ��������
            //List Ÿ���� wayPoints �迭�� �߰�
            group.GetComponentsInChildren<Transform>(wayPoints);
            //�迭�� ù ��° �׸� ����
            wayPoints.RemoveAt(0);
        }

        SetPatrolling(true);
    }

    //���� ���������� �̵� ����� ������ �Լ�
    void MoveWayPoint()
    {
        //�ִܰŸ� ��� ����� ������ �ʾ����� ������ �������� ����
        if (agent.isPathStale) return;
        //���� �������� wayPoints �迭���� ������ ��ġ�� ���� �������� ����
        agent.destination = wayPoints[nextIdx].position;
        //������̼� ����� Ȱ��ȭ�ؼ� �̵��� ������
        agent.isStopped = false;
    }

    void Update()
    {
        //���� ��尡 �ƴ� ��� ���� ������ �������� ����
        if (!patrolling)
            return;

        //NavMeshAgent�� �̵��ϰ� �ְ� �������� �����ߴ��� ���θ� ���
        if(agent.velocity.magnitude>=0.2f&&agent.remainingDistance<=0.5f)
        {
            //���� �������� �迭 ÷�ڸ� ���
            nextIdx = ++nextIdx % wayPoints.Count;
            //���� �������� �̵� ����� ����
            MoveWayPoint();
        }
    }

    public void SetPatrolling(bool patrol)
    {
        patrolling = patrol;
        agent.speed = patrolSpeed;
        agent.angularSpeed = 120;
        MoveWayPoint();
    }

    public void SetTraceTarget(Vector3 pos)
    {
        traceTarget = pos;
        agent.speed = traceSpeed;
        agent.angularSpeed = 360;
        TraceTarget(traceTarget);
    }

    //���ΰ��� ������ �� �̵���Ű�� �Լ�
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
            return;
        agent.destination = pos;
        agent.isStopped=false;
    }

    //���� �� ������ ������Ű�� �Լ�
    public void Stop()
    {
        agent.isStopped=true;
        //�ٷ� �����ϱ� ���� �ӵ��� 0���� ����
        agent.velocity = Vector3.zero;
        patrolling = false;
    }

    //NavMeshAgent�� �̵� �ӵ� ���
    public float GetSpeed()
    {
        return agent.velocity.magnitude;
    }
}
