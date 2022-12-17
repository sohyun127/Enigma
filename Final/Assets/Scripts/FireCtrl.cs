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
