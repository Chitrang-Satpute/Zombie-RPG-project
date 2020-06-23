using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRang = 5f;
    [SerializeField] float turningSpeed = 5f;
    EnemyHealth enemyHealth;


    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;

    bool isProvoke = false;
    Animator animator ;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
   
    void Update()
    {
        if(enemyHealth.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoke)
        {
            EngageTarget();           
        }
         else if(distanceToTarget <= chaseRang)
        {
            isProvoke = true;    
        }
        
    }

    public void OnDamageTaken()
    {
        animator.SetTrigger("Taking Damage");
        isProvoke = true;
    }

    private void EngageTarget()
    {
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
            RotateTowardPlayer();          
        }
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
        animator.SetTrigger("Move");
        animator.ResetTrigger("Attack");
    }

    private void AttackTarget()
    {
        animator.SetTrigger("Attack");
    }

    private void RotateTowardPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRoatation = Quaternion.LookRotation(new Vector3 (direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoatation, Time.deltaTime * turningSpeed);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, chaseRang);
    }
}
