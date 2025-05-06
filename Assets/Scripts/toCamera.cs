using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toCamera : MonoBehaviour
{
    Camera cam;
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = cam.transform.rotation;
    }
}
