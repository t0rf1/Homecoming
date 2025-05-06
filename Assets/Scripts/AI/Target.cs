using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    TargetManager targetManager;

    private void Start()
    {
        targetManager = GameObject.Find("TargetManager").GetComponent<TargetManager>();
    }

    private void OnDestroy()
    {
        targetManager.RemoveTarget(this);
    }
}
