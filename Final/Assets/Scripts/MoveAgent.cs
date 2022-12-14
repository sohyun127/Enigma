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

    void Start()
    {
        //NavMeshAgent ������Ʈ�� ������ �� ������ ����
        agent = GetComponent<NavMeshAgent>();
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

        MoveWayPoint();
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
        //NavMeshAgent�� �̵��ϰ� �ְ� �������� �����ߴ��� ���θ� ���
        if(agent.velocity.magnitude>=0.2f&&agent.remainingDistance<=0.5f)
        {
            //���� �������� �迭 ÷�ڸ� ���
            nextIdx = ++nextIdx % wayPoints.Count;
            //���� �������� �̵� ����� ����
            MoveWayPoint();
        }
    }
}
