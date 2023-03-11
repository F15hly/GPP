using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    InputManager inputManager;
    public Transform targetTransform;
    public Transform cameraPivot;
    private Transform cameraTransform;
    public LayerMask collisionLayers;
    private float defaultPos;
    public float cameraCollisionRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    public float minCollisionOffset = 0.2f;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 cameraVectorPos;

    public float followSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;

    public float lookAng;
    public float pivotAng;
    public float minPiv = -35f;
    public float maxPiv = 35f;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPos = cameraTransform.localPosition.z;
    }
    public void handleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        CamCollisions();
    }
    private void FollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraVelocity, followSpeed);
        transform.position = targetPos;
    }

    private void RotateCamera()
    {
        Vector3 rotation = Vector3.zero;

        lookAng = lookAng + (inputManager.cameraInpX * cameraLookSpeed);
        pivotAng = pivotAng - (inputManager.cameraInpY * cameraPivotSpeed);
        pivotAng = Mathf.Clamp(pivotAng, minPiv, maxPiv);

        rotation = Vector3.zero;
        rotation.y = lookAng;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAng;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void CamCollisions()
    {
        float targetPos = defaultPos;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();
        if (Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPos), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPos =- (distance - cameraCollisionOffset);
        }
        if(Mathf.Abs(targetPos) < minCollisionOffset)
        {
            targetPos = targetPos - minCollisionOffset;
        }

        cameraVectorPos.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPos, 0.2f);
        cameraTransform.localPosition = cameraVectorPos;
    }
}
