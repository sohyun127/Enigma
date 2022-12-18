using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
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


    private void Start()
    {
        //firePos ������ �ִ� ������Ʈ ����
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        //AudioSource ������Ʈ ����
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //���콺 ���� ��ư�� Ŭ������ �� Fire �Լ� ȣ��
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        // ���콺 ������ ��ư�� ������ �ü��� �ٶ󺸴� �������� ����ź�� ������ �ʹ�.
        // ���콺 ������ ��ư�� �Է¹޴´�.
        if (Input.GetMouseButtonDown(1))
        {
            // ����ź ������Ʈ�� ������ �� ����ź�� ���� ��ġ�� �߻� ��ġ�� �Ѵ�.
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = bombPosition.transform.position;

            // ����ź ������Ʈ�� Rigidbody ������Ʈ�� �����´�.
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            // ī�޶��� ���� �������� ����ź�� �������� ���� ���Ѵ�.
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

    }

    void Fire()
    {
        //Bullet �������� �������� ����
        Instantiate(bullet, firePos.position, firePos.rotation);

        //��ƼŬ ����
        cartridge.Play();
        //�ѱ� ȭ�� ��ƼŬ ����
        muzzleFlash.Play();
        //���� �߻�
        _audio.PlayOneShot(fireSound, 1.0f);
    }
}
