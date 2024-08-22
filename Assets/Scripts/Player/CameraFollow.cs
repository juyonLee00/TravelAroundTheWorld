using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 8f;

    private Transform cameraTransform;
    private bool isFollowing = true;

    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // 메인 카메라를 찾아서 할당
        cameraTransform = mainCamera.transform;
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            Vector3 desiredPosition = transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            cameraTransform.position = smoothedPosition;
        }
    }

    public void SetCameraFollow(bool follow)
    {
        isFollowing = follow;
    }
}
