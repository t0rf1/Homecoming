using UnityEngine;

public class Gun : MonoBehaviour
{

    TargetManager targetManager;
    RaycastHit hit;
    [SerializeField] LayerMask ground;

    bool canShoot = true;
    bool shootDelay = true;

    public float fieldOfView;
    public float stunnDuration, timeBetweenShots;
    public int damage;

    private void Start()
    {
        targetManager = GameObject.Find("TargetManager").GetComponent<TargetManager>();
    }
    private void Update()
    {
        if (targetManager.currentTarget != null)
        {
            if (targetManager.currentTarget != null)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    //Debug.Log("Gun Ready");
                    if (Input.GetKeyDown(KeyCode.Mouse0) && shootDelay)
                    {
                        shootDelay = false;

                        //Debug.Log("Shoot");

                        //Check angle
                        Vector3 projectedVector = Vector3.ProjectOnPlane(targetManager.currentTarget.transform.position - transform.position, transform.up);
                        if (Vector3.SignedAngle(projectedVector, transform.forward, transform.forward) <= fieldOfView)
                        {
                            if (!Physics.Raycast(transform.position, targetManager.currentTarget.transform.position - transform.position, out hit, Vector3.Distance(transform.position, targetManager.currentTarget.transform.position), ground))
                            {
                                Debug.Log("Shooted and dealt " + damage + " damage to obj: " + targetManager.currentTarget.name);
                                targetManager.currentTarget.GetComponent<IDamagable>().TakeDamage(damage, stunnDuration);
                            }
                            else
                            {
                                Debug.Log("CosPrzeszkadza :" + hit.collider.name);
                            }
                        }
                        else
                        {
                            Debug.Log("Poza zasiêgiem widzenia");
                        }
                        Invoke(nameof(ShootReset), timeBetweenShots);
                    }
                }
            }
        }
    }
    void ShootReset()
    {
        shootDelay = true;
    }
    private void OnDrawGizmos()
    {
       
    }
}