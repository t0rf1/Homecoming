using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<Target> targets = new List<Target>();
    public Target currentTarget;
    // Add Target
    public void AddTarget(Target target)
    {
        
        targets.Add(target);
    }

    

    // Remove Target
    public void RemoveTarget(Target target)
    {
        targets.Remove(target);
    }

    // OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        Target newTarget = other.GetComponent<Target>();
        if (newTarget != null)
        {
            AddTarget(newTarget);
        }
    }

    // OnTriggerExit
    private void OnTriggerExit(Collider other)
    {
        Target newTarget = other.GetComponent<Target>();
        if (newTarget != null)
        {
            RemoveTarget(newTarget);
        }
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            Vector3 projectedVector = Vector3.ProjectOnPlane(currentTarget.transform.position - transform.position, transform.up);
            if (Vector3.SignedAngle(projectedVector, transform.forward, transform.forward) <= 100)
            {

            }
        }


        if (targets.Count > 1)
        {
            SortTargets();
        }
        else if(targets.Count == 1)
        {
            currentTarget = targets[0];
        }
        else
        {
            currentTarget=null;
        }
    }
    void SortTargets()
    {
        float closestDistance = 100;
        foreach (Target target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentTarget = target; 
            }
        }
    }
}

