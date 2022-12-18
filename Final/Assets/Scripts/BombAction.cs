using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{

    // ���� ����Ʈ ������ ����
    public GameObject bombEffect;
    // ����ź ������
    public int attackPower = 10;
    // ���� ȿ�� �ݰ�
    public float explosionRadius = 5f;
    //AudioSource ������Ʈ�� ������ ����
    AudioSource _audio;
    //���� ����� Ŭ���� ������ ����
    public AudioClip fireSound;



    void Start()
    {
        //AudioSource ������Ʈ ����
        _audio = GetComponent<AudioSource>();
    }

    // �浹���� ���� ó��
  void OnCollisionEnter(Collision collision)
    {
       

        // ���� ȿ�� �ݰ� ������ ���̾ ��Enemy���� ��� ���� ������Ʈ���� Collider ������Ʈ�� �迭�� �����Ѵ�.
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 10);

        // ����� Collider �迭�� �ִ� ��� ���ʹ̿��� ����ź �������� �����Ѵ�.
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].GetComponent<EnemyDamage>().HitEnemy(attackPower);
        }

        // ����Ʈ �������� �����Ѵ�.
        GameObject eff = Instantiate(bombEffect);
        //���� �߻�
        _audio.PlayOneShot(fireSound,0.5f);
        // ����Ʈ �������� ��ġ�� ����ź ������Ʈ �ڽ��� ��ġ�� �����ϴ�.
        eff.transform.position = transform.position;
        // �ڱ� �ڽ��� �����Ѵ�.
        Destroy(gameObject);

    }
}

