using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootstepSystem : MonoBehaviour
{
    [Range(0, 20f)]
    public float frequency = 10.0f;

    public UnityEvent onFootStep;

    float Sin;
    bool isTriggered = false;

    Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float transformMagnitude = (transform.position - lastPosition).magnitude;

        if (transformMagnitude > 0)
        {
            StartFootsteps();
        }

        lastPosition = transform.position;
    }

    private void StartFootsteps()
    {
        Sin = Mathf.Sin(Time.time * frequency);

        if (Sin > 0.97f && isTriggered == false)
        {
            isTriggered = true;
            onFootStep.Invoke();
        }
        else if (isTriggered == true && Sin < -0.97f)
        {
            isTriggered = false;
        }
    }
}