using System;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(DateTime.Now + " GoToPatrolState ");
        myEnemy.m_Anim.SetBool("attack", false);
        myEnemy.m_Anim.SetBool("run", false);
        myEnemy.m_Anim.SetBool("walk", true);
        myEnemy.navMeshAgent.speed = 50;
        myEnemy.navMeshAgent.Resume();
        myEnemy.currentState = myEnemy.patrolState;
    }
    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerStay(Collider col)
    {
        if (myEnemy.isDying)
        {
            return;
        }


        if (col.tag == "Player")
        {
            Vector3 lookDirection = col.transform.position -
                                myEnemy.transform.position;

            myEnemy.transform.rotation =
                Quaternion.FromToRotation(Vector3.forward,
                                            new Vector3(lookDirection.x, 0, lookDirection.z));
            if (actualTimeBetweenAttacks > myEnemy.timeBetweenAttacks)
            {
                actualTimeBetweenAttacks = 0;
                float distancia = Vector3.Distance(col.gameObject.transform.position, myEnemy.transform.position);
                if (distancia < 10f)
                {
                    myEnemy.navMeshAgent.speed = 0;
                    myEnemy.navMeshAgent.isStopped = true;
                    myEnemy.navMeshAgent.destination = myEnemy.transform.position;
                    myEnemy.m_Anim.SetBool("run", false);
                    myEnemy.m_Anim.SetBool("attack", true);
                    //if (myEnemy.m_Anim.GetBool("run"))
                    //{
                    //    myEnemy.m_Anim.SetBool("run", false);
                    //}
                    //if (!myEnemy.m_Anim.GetBool("attack"))
                    //{
                    //    myEnemy.m_Anim.SetBool("attack", true);
                    //}


                    RaycastHit hit;
                    Vector3 direction = new Vector3(myEnemy.transform.forward.x, myEnemy.transform.forward.y + 1.0f, myEnemy.transform.forward.z);
                    Vector3 position = new Vector3(myEnemy.transform.position.x, col.gameObject.transform.position.y + 1.0f, myEnemy.transform.position.z);
                    if (Physics.Raycast(new Ray(position, direction * 100f), out hit)) {
                        if (hit.collider.gameObject.tag == "Player")
                        {
                            hit.collider.gameObject.GetComponent<Shooter>().Hit(myEnemy.damageForce);
                        }
                    }

                }
                else
                {
                    if (!myEnemy.m_Anim.GetBool("attack"))
                    {
                        myEnemy.m_Anim.SetBool("attack", false);
                    }

                    if (myEnemy.m_Anim.GetBool("run"))
                    {
                        myEnemy.m_Anim.SetBool("run", true);
                    }

                    if(myEnemy.navMeshAgent.speed == 0)
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
