using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    //카메라 민감도 변수
    public float lookSensitivity = 1;
    //카메라 각도 제한 변수
    public float cameraRotationLimit = 45;
    //카메라 현재 각도
    private float currentCameraRotationX = 0;

    void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float XRotation = Input.GetAxis("Mouse Y");
        float CameraRotationX= XRotation * lookSensitivity;
        //카메라 현재 각도에서 회전 각도 만큼 더하기
        currentCameraRotationX += CameraRotationX;
        //카메라 각도 제한
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit,cameraRotationLimit);
        //카메라 회전 
        transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

}
