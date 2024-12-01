using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static List<GameObject> cameras = new List<GameObject>();

    private void Awake()
    {
        cameras.AddRange(GameObject.FindGameObjectsWithTag("Camera"));
    }

    public static void ActivateCamera(GameObject activatedCamera)
    {
        foreach (GameObject cam in cameras)
        {
            cam.SetActive(cam == activatedCamera);
        }
    }
}