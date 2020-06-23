using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 100;
    bool isDead = false;
   
    public void ReduceEnemyHealth(int damage)
    {
        BroadcastMessage("OnDamageTaken");
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            Die();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Die()
    {
        if (isDead) return;  
        GetComponent<Animator>().SetTrigger("Die");
        isDead = true;

    }
}
