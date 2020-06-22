using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damageToPlayer = 50f;
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void EnemyAttackEvent()
    {
        
        if (target == null) 
        {
            return;
        }
        else
        {
            
            target.ReducePlayerHealth(damageToPlayer);
        }

    }
}
