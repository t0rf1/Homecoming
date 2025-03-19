using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    /// <summary>
    /// Apply damage to entity though this function
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage);
   
}

