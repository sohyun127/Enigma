using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    //ī�޶� �ΰ��� ����
    public float lookSensitivity = 1;
    //ī�޶� ���� ���� ����
    public float cameraRotationLimit = 45;
    //ī�޶� ���� ����
    private float currentCameraRotationX = 0;

    void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float XRotation = Input.GetAxis("Mouse Y");
        float CameraRotationX= XRotation * lookSensitivity;
        //ī�޶� ���� �������� ȸ�� ���� ��ŭ ���ϱ�
        currentCameraRotationX += CameraRotationX;
        //ī�޶� ���� ����
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit,cameraRotationLimit);
        //ī�޶� ȸ�� 
        transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

}
