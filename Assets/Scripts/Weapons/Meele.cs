using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meele : MonoBehaviour
{
    PlayerAudioManager playerAudio;
    public int damage;
    public float stunnDuration, timeBetweenAttacks = 0.5f;
    public bool canCombo, canAttack = true;
    int swingInRow = 0;
    
    public List <Collider> targets;

    private void Start()
    {
        playerAudio = GetComponentInParent<PlayerAudioManager>();
        canAttack = true;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        //{
        //    canAttack = false;
        //    if (canCombo)
        //    {
        //        swingInRow++;
        //    }
        //    //enemy in range? do zmiany póŸniej w przypadku trafiania coliderem w animacjii
        //    if (targets.Count > 0)
        //    {
        //        DoDamageToEnemy();
        //    }

        //    //Animacja uderzania
        //    Invoke(nameof(AttackReset), timeBetweenAttacks);
        //}
    }


    void AttackReset()
    {
        canAttack = true;   
    }
    public void DoDamageToEnemy()
    {
        foreach(var target in targets)
        {
           
            playerAudio.DoDamageSound();
            target.GetComponent<IDamagable>().TakeDamage(damage, stunnDuration);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Add(other);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Remove(other);
            
        }
    }
}
