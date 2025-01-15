using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsModel : MonoBehaviour
{
    Doors doors;

    [SerializeField] Transform boxOrigin;
    public float boxMaxDistance; // Maksymalna odleg³oœæ castu
    RaycastHit hit;
    bool isHit;

    void Update()
    {
        // Przeprowadzenie BoxCast
        isHit = Physics.BoxCast(center: boxOrigin.position, halfExtents: transform.lossyScale / 2, direction: transform.forward, out hit, transform.rotation, boxMaxDistance);
        if (isHit)
        {
            // Sprawdzenie, czy obiekt ma tag "Player"
            if (hit.collider.CompareTag("Player"))
            {
                OnObjectHit();
            }
        }
    }

    private void Start()
    {
        doors = GetComponentInParent<Doors>();
    }

    public void AnimationFinished()
    {
        if (doors.canStop_animation)
        {
            doors.StopAnimation();
        }
    }

    public void CanStop()
    {
        doors.canStop_animation = true;
    }

    void OnObjectHit()
    {
        doors.StopAnimation();
    }

    private void OnDrawGizmos()
    {
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(from: boxOrigin.position, direction: transform.forward * hit.distance);
            Gizmos.DrawWireCube(center: boxOrigin.position + transform.forward * hit.distance, size: transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(from: boxOrigin.position, direction: transform.forward * boxMaxDistance);
        }
    }
}
