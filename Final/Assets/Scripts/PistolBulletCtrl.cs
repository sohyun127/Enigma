using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBulletCtrl : MonoBehaviour
{
    public float damage = 20.0f;
    public float speed = 1000.0f;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }
}
