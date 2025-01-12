using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsModel : MonoBehaviour
{
    Doors doors;

    private void Start()
    {
        doors = GetComponentInParent<Doors>();
    }

    public void AnimationFinished()
    {
        doors.StopAnimation();
    }
    
    public void CanStop()
    {
        doors.canStop_animation = true;
    }

    void OnDrawGizmos()
    {
        Vector3 center = new Vector3(transform.position.x - .5f, transform.position.y + 1f, transform.position.z);
        float maxDistance = 1f;
        RaycastHit hit;

        bool isHit = Physics.BoxCast(center, halfExtents: transform.lossyScale / 2, direction: transform.forward, out hit, transform.rotation, maxDistance);
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(center, direction: transform.forward * hit.distance);
            Gizmos.DrawWireCube(center + transform.forward * hit.distance, size: transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(center, direction: transform.forward * maxDistance);
        }
    }
}
