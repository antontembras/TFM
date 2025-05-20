using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class RunState : IEnemyState
{
    enemyAI myEnemy;

    public RunState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    
    public void UpdateState()
    {
        if (myEnemy.isDying)
        {
            return;
        }
    }
    public void Impact() { }

    
    public void GoToRunState(Vector3 destination) { }
    public void GoToPatrolState()
    {
        myEnemy.m_Anim.SetBool("attack", false);
        myEnemy.m_Anim.SetBool("run", false);
        myEnemy.m_Anim.SetBool("walk", true);
        myEnemy.navMeshAgent.speed = myEnemy.walkSpeed;
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
           
                if (distancia < myEnemy.navMeshAgent.stoppingDistance)
                {
                    GoToAttackState();
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

    public void GoToAttackState()
    {
       // Debug.Log(DateTime.Now + " RUN STATE  GoToAttackState");
        myEnemy.m_Anim.SetBool("attack", true);
        myEnemy.m_Anim.SetBool("run", false);
        myEnemy.m_Anim.SetBool("walk", false);
        myEnemy.navMeshAgent.speed =0;
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.actualTimeBetweenAttacks = myEnemy.timeBetweenAttacks + 1;
        myEnemy.currentState = myEnemy.attackState;
    }
}
