using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLook : MonoBehaviour
{
    public Transform headBone;
    public Transform targetObject;
    public Transform headForward;

    public float minAngle, maxAngle;

    public float lookSpeed;
    bool isLooking;
    Quaternion lastRotation;
    float headResetTimer;

    void LateUpdate()
    {
        if(targetObject != null)
        {
            Vector3 direction = (targetObject.position - headBone.position).normalized;
            float angle = Vector3.SignedAngle(direction, headForward.forward, headForward.up);

            if (angle < maxAngle && angle > minAngle)
            {
                if (!isLooking)
                {
                    isLooking = true;
                    lastRotation = headBone.rotation;
                }
                Quaternion targetRotation = Quaternion.LookRotation(targetObject.position - headBone.position);
                lastRotation = Quaternion.Slerp(lastRotation, targetRotation, lookSpeed * Time.deltaTime);

                headBone.rotation = lastRotation;
                headResetTimer = .5f;
            }
            else if (isLooking)
            {
                lastRotation = Quaternion.Slerp(lastRotation, headForward.rotation, lookSpeed * Time.deltaTime);
                headBone.rotation = lastRotation;
                headResetTimer -= Time.deltaTime;

                if (headResetTimer < 0.0f)
                {
                    headBone.rotation = headForward.rotation;
                    isLooking = false;
                }
            }
        }
    }
}
