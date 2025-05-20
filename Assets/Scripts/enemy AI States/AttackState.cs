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
        myEnemy.actualTimeBetweenAttacks += Time.deltaTime;

    }
    public void Impact() { }

    
    public void GoToRunState(Vector3 destination)
    {
        //Debug.Log(DateTime.Now + " ATTACK STATE  GoToRunState");
        myEnemy.m_Anim.SetBool("run", true);
        myEnemy.m_Anim.SetBool("attack", false);
        //myEnemy.m_Anim.SetBool("walk", false);

        myEnemy.navMeshAgent.speed = myEnemy.runSpeed;
        myEnemy.currentState = myEnemy.runState;
        myEnemy.navMeshAgent.destination = destination;
    }
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
           // Debug.Log(DateTime.Now + " ATTACK STATE distancia = " + distancia);
            if(distancia > myEnemy.navMeshAgent.stoppingDistance + 5)
            {
                GoToRunState(col.transform.position);
            }else if (myEnemy.actualTimeBetweenAttacks > myEnemy.timeBetweenAttacks)
            {
                myEnemy.actualTimeBetweenAttacks = 0;

                myEnemy.m_Anim.SetBool("run", false);
                    //Debug.Log(DateTime.Now + " ataca ");
                    myEnemy.navMeshAgent.speed = 0;
                    myEnemy.navMeshAgent.isStopped = true;
                    // myEnemy.navMeshAgent.destination = myEnemy.transform.position;
                    myEnemy.m_Anim.SetBool("attack", true);

                    if (myEnemy.attackSound != null)
                    {
                        myEnemy.enemyAudioSource.clip = myEnemy.attackSound;
                        myEnemy.enemyAudioSource.time = myEnemy.attackSoundStartSecond;
                        myEnemy.enemyAudioSource.Play();
                    }


                // RaycastHit hit;
                // if (Physics.Raycast(new Ray(myEnemy.transform.position, myEnemy.transform.forward), out hit, myEnemy.attackRange)) {
                //     hit.collider.gameObject.GetComponentInParent<PlayerMovement>().Hit(myEnemy.attackForce);
                // }
            }
            else
            {
             //   Debug.Log(DateTime.Now + " ATTACK STATE myEnemy.actualTimeBetweenAttacks = " + myEnemy.actualTimeBetweenAttacks + " myEnemy.timeBetweenAttacks = " + myEnemy.timeBetweenAttacks);
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
            GoToRunState(col.transform.position);
        }


    }

    public void GoToAttackState()
    {
    }
}
