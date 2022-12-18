using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperCtrl : MonoBehaviour
{
    //�Ѿ� ������
    public GameObject bullet;
    //ź�� ���� ��ƼŬ
    public ParticleSystem cartridge;
    //�Ѿ� �߻� ��ǥ
    public Transform firePos;
    //����� Ŭ���� ������ ����
    public AudioClip fireSound;


    //�ѱ� ȭ�� ��ƼŬ
    ParticleSystem muzzleFlash;
    //AudioSource ������Ʈ�� ������ ����
    AudioSource _audio;
    // ����ź �߻� ��ġ
    public GameObject bombPosition;
    // ����ź ���� ������Ʈ
    public GameObject bombFactory;
    // ����ź ��ô �Ŀ�
    public float throwPower = 15f;

    //�Ѿ��� �浹 ����
    private RaycastHit hitInfo;
    //���� ��� �ؽ�Ʈ
    public Text wModeText;
    GameObject enemy;
    //Enemy Damamge ������Ʈ ��������
    EnemyDamage EnemyDamage;
    //�������� ���� ������
    int power = 30;
    //�ǰݽ� ����� ���� ȿ��
    GameObject bloodEffect;
    //������ ����
    bool isReload = false;


    //���� ��� ����
    enum WeaponMode
    { 

        Normal,
        Sniper
    
    }

    WeaponMode wMode;

    //ī�޶� Ȯ�� Ȯ�ο� ����
    bool ZoomMode = false;

    private void Start()
    {
        //firePos ������ �ִ� ������Ʈ ����
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        //AudioSource ������Ʈ ����
        _audio = GetComponent<AudioSource>();

        //���� �⺻ ��带 ��� ���� �����Ѵ�
        wMode = WeaponMode.Normal;

        //enemydamage ������Ʈ �ҷ�����
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        EnemyDamage = enemy.GetComponent<EnemyDamage>();

        //���� ȿ�� �������� �ε�
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
    }

    private void Update()
    {
        //���콺 ���� ��ư�� Ŭ������ �� Fire �Լ� ȣ��
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        // ���� Ű������ ���� 1�� �Է��� ������, ���� ��带 �Ϲ� ���� �����Ѵ�.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            wMode = WeaponMode.Normal;
            // ī�޶��� ȭ���� �ٽ� ������� �����ش�.
            Camera.main.fieldOfView = 60f;

            //�Ϲ� ��� �ؽ�Ʈ ���
            wModeText.text = "Normal Mode";
        }
        // ���� Ű������ ���� 2�� �Է��� ������, ���� ��带 �������� ���� �����Ѵ�.
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            wMode = WeaponMode.Sniper;

            //�������� ��� �ؽ�Ʈ ���
            wModeText.text = "Sniper Mode";
        }

        // ���콺 ������ ��ư�� �Է¹޴´�.
        if (Input.GetMouseButtonDown(1))
        {
            switch (wMode)
            {
                //��� ���:���콺 ������ ��ư�� ������ �ü��� �ٶ󺸴� �������� ����ź�� ������ �ʹ�.
                case WeaponMode.Normal:
                    // ����ź ������Ʈ�� ������ �� ����ź�� ���� ��ġ�� �߻� ��ġ�� �Ѵ�.
                    GameObject bomb = Instantiate(bombFactory);
                    bomb.transform.position = bombPosition.transform.position;

                    // ����ź ������Ʈ�� Rigidbody ������Ʈ�� �����´�.
                    Rigidbody rb = bomb.GetComponent<Rigidbody>();
                    // ī�޶��� ���� �������� ����ź�� �������� ���� ���Ѵ�.
                    rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

                    break;
                //�������� ���: ���콺 ������ ��ư�� ������ ȭ���� Ȯ���ϰ� �ʹ�.
                case WeaponMode.Sniper:
                    //����, �� ��� ���°� �ƴ϶�� ī�޶� Ȯ���ϰ� �� ��� ���·� �����Ѵ�.
                    if(!ZoomMode)
                    {
                        Camera.main.fieldOfView = 15f;
                        ZoomMode = true;
                    }
                    //�׷��� �ʴٸ� ī�޶� ���� ���·� �ǵ����� �� ��� ���¸� �����Ѵ�.
                    else
                    {
                        Camera.main.fieldOfView = 60f;
                        ZoomMode=false;
                    }
                    break;
            }
        }

        //if(GameManager.gm.gState!=GamaManager.GameState.Run)
        //{
           // return;
      //  }



    }


   
    void Fire()
    {
        
        //��ƼŬ ����
        cartridge.Play();
        //�ѱ� ȭ�� ��ƼŬ ����
        muzzleFlash.Play();
        //���� �߻�
        _audio.PlayOneShot(fireSound, 1.0f);
        Hit();
    }


    private void Hit()
    {
        //ī�޶� ���� ��ǥ
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hitInfo))
        {
            Debug.Log(hitInfo.transform.name);

            if(hitInfo.collider.tag=="Enemy")
            {
                //enemy���� ������
                EnemyDamage.HitEnemy(power);

                //�Ѿ��� �浹�� ���� ����
                Vector3 pos = hitInfo.point;
                //�Ѿ��� �浹���� ���� ���� ����
                Vector3 _normal = hitInfo.normal;
                //�Ѿ��� �浹 �� ���� ������ ȸ���� ���
                Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
                //���� ȿ�� ����
                GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
                Destroy(blood, 1.0f);
            }
        }
    }
}
