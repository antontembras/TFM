using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class AttackState : IEnemyState
{
    enemyAI myEnemy;
    public float actualTimeBetweenAttacks = 0;

    public AttackState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    
    public void UpdateState()
    {
        if (myEnemy.isDying)
        {
            return;
        }
        actualTimeBetweenAttacks += Time.deltaTime;

    }
    public void Impact() { }

    
    public void GoToAttackState(Vector3 destination) { }
    public void GoToPatrolState()
    {
        myEnemy.m_Anim.SetBool("attack", false);
        myEnemy.m_Anim.SetBool("run", false);
        myEnemy.m_Anim.SetBool("walk", true);
        myEnemy.navMeshAgent.speed = 50;
        myEnemy.navMeshAgent.isStopped = false;
        myEnemy.currentState = myEnemy.patrolState;
    }
    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerStay(Collider col)
    {
        if (myEnemy.isDying)
        {
            myEnemy.m_Anim.SetBool("run", false);
            myEnemy.m_Anim.SetBool("attack", false);
            return;
        }


        if (col.tag == "Player")
        {
            Vector3 lookDirection = col.transform.position -
                                myEnemy.transform.position;

            myEnemy.transform.rotation =
                Quaternion.FromToRotation(Vector3.forward,
                                            new Vector3(lookDirection.x, 0, lookDirection.z));
            float distancia = Vector3.Distance(col.gameObject.transform.position, myEnemy.transform.position);
            if (actualTimeBetweenAttacks > myEnemy.timeBetweenAttacks)
            {
                actualTimeBetweenAttacks = 0;

                myEnemy.m_Anim.SetBool("run", false);
                if (distancia < myEnemy.navMeshAgent.stoppingDistance)
                {
                    myEnemy.navMeshAgent.speed = 0;
                    myEnemy.navMeshAgent.isStopped = true;
                   // myEnemy.navMeshAgent.destination = myEnemy.transform.position;
                    myEnemy.m_Anim.SetBool("attack", true);


                    RaycastHit hit;
                    if (Physics.Raycast(new Ray(myEnemy.transform.position, myEnemy.transform.forward), out hit, myEnemy.attackRange)) {
                        hit.collider.gameObject.GetComponentInParent<PlayerMovement>().Hit(myEnemy.damageForce);
                    }

                }
                else
                {
                    myEnemy.m_Anim.SetBool("attack", false);

                    myEnemy.m_Anim.SetBool("run", true);

                    if (myEnemy.navMeshAgent.speed == 0)
                    {
                        myEnemy.navMeshAgent.speed = 50;
                        myEnemy.navMeshAgent.isStopped = false;
                    }

                     if (myEnemy.navMeshAgent.remainingDistance <= myEnemy.navMeshAgent.stoppingDistance)
                     {
                         myEnemy.navMeshAgent.destination = col.gameObject.transform.position;
                     }
                }
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (myEnemy.isDying)
        {
            return;
        }
        if (col.tag == "Player")
        {
            GoToPatrolState();
        }


    }
}
