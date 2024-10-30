using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    private GameObject childCamera;

    private void Start()
    {
        childCamera = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            CameraManager.ActivateCamera(childCamera);
        }
    }
}
