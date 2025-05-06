using UnityEngine;

public class Gun : MonoBehaviour
{

    TargetManager targetManager;
    RaycastHit hit;
    [SerializeField] LayerMask ground;

    bool canShoot = true;
    bool shootDelay = true;
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
            if (Input.GetKey(KeyCode.Mouse1))
            {
                //Debug.Log("Gun Ready");
                if (Input.GetKeyDown(KeyCode.Mouse0) && shootDelay)
                {
                    shootDelay = false;
                    //Debug.Log("Shoot");

                    if(!Physics.Raycast(transform.position, targetManager.currentTarget.transform.position, out hit, ground))
                    {
                        Debug.Log("Shooted and dealt " + damage + " damage to obj: " + targetManager.currentTarget.name);
                        targetManager.currentTarget.GetComponent<IDamagable>().TakeDamage(damage, stunnDuration);
                    }
                    else
                    {
                        Debug.Log("CosPrzeszkadza :" + hit.collider.name);
                    }


                    Invoke(nameof(ShootReset), timeBetweenShots);
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
        if(targetManager.currentTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, targetManager.currentTarget.transform.position);
        }
        

    }
}