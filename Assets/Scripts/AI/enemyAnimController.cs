using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimController : MonoBehaviour
{
    public float speed;
    Animator anim;
   [SerializeField] EnemyAi enemyAi;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (speed > 0.1f)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }
    public void Attack()
    {
        anim.SetTrigger("attack");
    }
    public void Damage()
    {
        anim.SetTrigger("damage");
    }

    public void AnimationAttack()
    {
        //Debug.Log("Animation Attack Triggered");
        enemyAi.AnimationAttack();
    }

}
