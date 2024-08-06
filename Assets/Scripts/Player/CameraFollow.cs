using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, -10); // 카메라와 플레이어 사이의 거리 조정
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움 조정

    private Transform cameraTransform;

    void Start()
    {
        // 메인 카메라를 찾아서 할당
        cameraTransform = Camera.main.transform;
        Debug.Log(cameraTransform);
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            Vector3 desiredPosition = transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed);
            cameraTransform.position = smoothedPosition;
        }
    }
}
