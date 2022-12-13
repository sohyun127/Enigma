using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBulletCtrl : MonoBehaviour
{
    //�Ѿ��� �ı���
    public float damage = 20.0f;
    //�Ѿ� �߻� �ӵ�
    public float speed = 1000.0f;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * speed);
    }
}
