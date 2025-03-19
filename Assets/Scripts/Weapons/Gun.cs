using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject currentTarget;
    public List<GameObject> targets;
    RaycastHit hitBox, hitRay;

    bool canShoot = true;
    bool shootDelay = true;
    public float damage, timeBetweenShots;

    private void Update()
    {
        SearchForTarget();
        if (targets.Count > 0)
        {
            SortTargets();
        }
        else
        {
            currentTarget = null;
        }

        if (currentTarget != null)
        {
            if (Physics.Raycast(transform.position, currentTarget.transform.position, out hitRay))
            {
                
                if (hitRay.collider != null)
                {

                    
                    
                }
                else
                {
                    //canShoot = true;
                   // Debug.Log("hit" + hitRay.collider.name);
                    
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.P) && shootDelay)
                {
                    shootDelay = false;
                    Debug.Log("Shooted and dealt");
                    Invoke(nameof(ShootReset), timeBetweenShots);
                }
            }
            
        }
        
    }
    void SearchForTarget()
    {
        Physics.BoxCast(transform.position, new Vector3(1, 3, 1), transform.forward, out hitBox);
        if (hitBox.collider != null)
        {
            if (hitBox.collider.CompareTag("Enemy"))
            {
                if (targets.Contains(hitBox.collider.gameObject) == false)
                {
                    targets.Add(hitBox.collider.gameObject);
                    //Debug.Log(hitBox.collider.name);
                }
            }
            else
            {
                targets.Clear();
            }

        }
        else
        {
            targets.Clear();
        }
    }

    void SortTargets()
    {
        float closestDistance = 100;
        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentTarget = target;
            }
        }
    }

    void ShootReset()
    {
        shootDelay = true;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawCube(transform.position, new Vector3(1, 3, 1));
        if(currentTarget != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, currentTarget.transform.position);
        }
        
    }
}
