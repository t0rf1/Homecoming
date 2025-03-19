using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour, IDamagable
{
    public float maxHp, hp;

    void Start()
    {
        hp = maxHp;
    }
    public void TakeDamage(int damage)
    {
        hp = -damage;
    }
}
