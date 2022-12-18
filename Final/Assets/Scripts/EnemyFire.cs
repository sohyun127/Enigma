using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    //�� �߻� ���θ� �Ǵ��� ����
    public bool isFire = false;
    //�� �߻� ���带 ������ ����
    public AudioClip fireSfx;
    //�� ĳ������ �Ѿ� ������
    public GameObject Bullet;
    //�Ѿ��� �߻� ��ġ ����
    public Transform firePos;

    //AudioSource ������Ʈ�� ������ ����
    AudioSource _audio;
    //Animator ������Ʈ�� ������ ����
    Animator animator;

    //���ΰ� ĳ������ Transform ������Ʈ
    Transform playerTr;
    //�� ĳ������ Transform ������Ʈ
    Transform enemyTr;
    //�ѱ� ȭ�� ȿ��
    ParticleSystem muzzleFlash;
    //���� �߻��� �ð� ���� ����
    float nextFire = 0.0f;
    //�Ѿ� �߻� ����
    float fireRate = 1.5f;
    //���ΰ��� ���� ȸ���� �ӵ� ���
    float damping = 10.0f;
    //�Ѿ� �߻� �ӵ�
    public float speed = 1000.0f;

    void Start()
    {
        //������Ʈ ���� �� ���� ����
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTr=GetComponent<Transform>();
        animator=GetComponent<Animator>();
        _audio=GetComponent<AudioSource>();
        muzzleFlash=firePos.GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
     if(isFire)
        {
            //���� �ð��� ���� �߻� �ð����� ū���� Ȯ��
            if(Time.time>=nextFire)
            {
                Fire();
                //���� �߻� �ð� ���
                nextFire = Time.time + fireRate + Random.Range(0.0f, 0.5f);
            }

            //���ΰ��� �ִ� ��ġ������ ȸ�� ���� ���
             Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            //���� �Լ��� �̿��� ���������� ȸ����Ŵ
             enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
         
        }
    }


    void Fire()
    {
       
        if (!animator.GetBool("IsMove"))
        {
            animator.SetTrigger("Fire");
        }
        _audio.PlayOneShot(fireSfx, 0.5f);
        muzzleFlash.Play();

        //�Ѿ��� ����
         GameObject _bullet = Instantiate(Bullet, firePos.position, firePos.rotation);
         Rigidbody rigidBullet = _bullet.GetComponent<Rigidbody>();
         //enemy������ �߻�
         rigidBullet.AddForce(enemyTr.forward*speed);


        //���� �ð��� ���� �� ����
        Destroy(_bullet, 3.0f);
    }
}
