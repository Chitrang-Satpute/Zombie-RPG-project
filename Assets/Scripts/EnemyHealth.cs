using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 100;
    bool isDead = false;
    RandomDropItem randomDrop;
    public GameObject drops;


    private void Start()
    {
        randomDrop = GetComponent<RandomDropItem>();
       
    }
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
        randomDrop.DropItems();
        Instantiate(drops, transform.position, Quaternion.identity);
        isDead = true;
        Destroy(gameObject, 5f);
    }
}
