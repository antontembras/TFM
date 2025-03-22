using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class PatrolState : IEnemyState
{
    enemyAI myEnemy;
    private int nextWayPoint = 0;

    public PatrolState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        if (myEnemy.isDying)
        {
            return;
        }
        if (myEnemy.navMeshAgent.remainingDistance <= myEnemy.navMeshAgent.stoppingDistance)
        {
            myEnemy.navMeshAgent.destination = RandomNavSphere(myEnemy.transform.position, UnityEngine.Random.Range(50.0f, 100.0f), -1);
        }
    }

    public void Impact()
    {
       GoToAttackState(GameObject.FindGameObjectWithTag("Player").gameObject.transform.position);
    }
    public void GoToAttackState(Vector3 destination)
    {
        Debug.Log(DateTime.Now + " GoToAttackState ");
        myEnemy.m_Anim.SetBool("run", true);
        myEnemy.currentState = myEnemy.attackState;
        myEnemy.navMeshAgent.destination = destination;
    }

    public void GoToPatrolState() { }


    public void OnTriggerEnter(Collider col)
    {
        if (myEnemy.isDying)
        {
            return;
        }
        if (col.gameObject.tag == "Player" )
           GoToAttackState(col.gameObject.transform.position);
    }

    public void OnTriggerStay(Collider col)
    {
        if (myEnemy.isDying)
        {
            return;
        }
        if (col.gameObject.tag == "Player")
            GoToAttackState(col.gameObject.transform.position);
    }
    public void OnTriggerExit(Collider col) { }


    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;
        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);



        return navHit.position;
    }


}
