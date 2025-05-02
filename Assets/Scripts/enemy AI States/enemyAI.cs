using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class enemyAI : MonoBehaviour
{
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    public float life;
    public float timeBetweenAttacks = 3.0f;
    public float attackForce;
    public float timeToDie;
    public float attackRange;
    public int swordDamage = 1;
    public int moonSwordDamage = 1;
    public int gunDamage = 1;

    public int walkSpeed = 25;
    public int runSpeed = 50;



    [HideInInspector] public bool isDying = false;
    [HideInInspector] private float timeStartDying = 0;
    [HideInInspector] private float timeStartHurt = 0;

    public Animator m_Anim;

    public GameObject bloodPrefab;
    public AudioClip m_attackSound;
    public GameObject dropItem;
    public float dropItemPercentage;
    [HideInInspector] public AudioSource enemyAudioSource;

    public bool canDamageSword = true;
    public bool canDamageMoonSword = true;
    public bool canDamageGun = false;
    public bool canExplode = false;
    public GameObject explosionPrefab;

    void Start()
    {

        m_Anim = GetComponent<Animator>();
        // Creamos los estados de nuestra IA.
        patrolState = new PatrolState(this);
        attackState = new AttackState(this);

        currentState = patrolState;

        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAudioSource = GetComponent<AudioSource>();

        navMeshAgent.speed = walkSpeed;


        navMeshAgent.destination = RandomNavSphere(transform.position, UnityEngine.Random.Range(50.0f, 100.0f), -1);

        m_Anim.SetBool("walk", true);

    }

    void Update()
    {
        if (timeStartHurt > 0)
        {
            timeStartHurt -= Time.deltaTime;

            if (timeStartHurt <= 0)
            {
                m_Anim.SetBool("damage", false);
                m_Anim.SetBool("run", false);
            }
        }
        if (isDying)
        {
             m_Anim.SetBool("isDead", true);

            timeStartDying += Time.deltaTime;

            if(timeStartDying > timeToDie)
            {
                int dropNumber = UnityEngine.Random.Range(0, 100);
                if(dropNumber <= dropItemPercentage && dropItem != null) {
                    Instantiate(dropItem, transform.position + new Vector3(0, 5.0f, 0), Quaternion.identity);
                }
                Destroy(this.gameObject);
            }
            return;
        }
        else
        {
            currentState.UpdateState();
        }

        
    }

   

    public void HitGun()
    {
        if (canDamageGun)
        {
            timeStartHurt = 1.0f;
            life -= gunDamage;
            if (life <= 0)
            {

                navMeshAgent.speed = 0;
                navMeshAgent.isStopped = true;
                m_Anim.SetBool("run", false);
                m_Anim.SetBool("attack", false);
                m_Anim.SetBool("damage", false);
                m_Anim.SetBool("die", true);
                isDying = true;

                Destroy(gameObject.GetComponent<BoxCollider>());
                Destroy(gameObject.GetComponent<Rigidbody>());
                Destroy(gameObject.GetComponent<NavMeshAgent>());
            }
            else
            {
                m_Anim.SetBool("run", false);
                m_Anim.SetBool("attack", false);
                m_Anim.SetBool("damage", true);
                currentState.Impact();
            }
        }
    }
    public void HitMoonSword()
    {
        if (canDamageMoonSword)
        {
            timeStartHurt = 1.0f;
            life -= moonSwordDamage;
            if (life <= 0)
            {

                navMeshAgent.speed = 0;
                navMeshAgent.isStopped = true;
                m_Anim.SetBool("run", false);
                m_Anim.SetBool("attack", false);
                m_Anim.SetBool("damage", false);
                m_Anim.SetBool("die", true);
                isDying = true;

               // Destroy(gameObject.GetComponent<BoxCollider>());
               // Destroy(gameObject.GetComponent<Rigidbody>());
                Destroy(gameObject.GetComponent<NavMeshAgent>());
            }
            else
            {
                m_Anim.SetBool("run", false);
                m_Anim.SetBool("attack", false);
                m_Anim.SetBool("damage", true);
                currentState.Impact();
                if (bloodPrefab != null)
                {
                    Instantiate(bloodPrefab, transform.position + new Vector3(0, 7.0f, 0), transform.rotation);
                }
            }
        }
    }

    public void HitSword()
    {
        if (canDamageSword)
        {
            timeStartHurt = 1.0f;
            life-= swordDamage;
            if (life <= 0)
            {

                navMeshAgent.speed = 0;
                navMeshAgent.isStopped = true;
                m_Anim.SetBool("run", false);
                m_Anim.SetBool("attack", false);
                m_Anim.SetBool("damage", false);
                m_Anim.SetBool("die", true);
                isDying = true;

                //Destroy(gameObject.GetComponent<BoxCollider>());
                //Destroy(gameObject.GetComponent<Rigidbody>());
                Destroy(gameObject.GetComponent<NavMeshAgent>());
            }
            else
            {
                m_Anim.SetBool("run", false);
                m_Anim.SetBool("attack", false);
                m_Anim.SetBool("damage", true);
                currentState.Impact();
                if (bloodPrefab != null)
                {
                    Instantiate(bloodPrefab, transform.position + new Vector3(0, 4.0f, 0), transform.rotation);
                }
            }
        }
    }


    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;
        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);



        return navHit.position;
    }

    public void explode()
    {
        if (canExplode)
        {
            Instantiate(explosionPrefab, transform.position + new Vector3(0, 5.0f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    // Ya que nuestros states no heredan de 
    // MonoBehaviour, tendremos que avisarles
    // cuando algo entra, está o sale de nuestro
    // trigger.
    void OnTriggerEnter(Collider col)
    {
        if(currentState != null)
        {
            currentState.OnTriggerEnter(col);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (currentState != null)
        {
            currentState.OnTriggerStay(col);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (currentState != null)
        {
            currentState.OnTriggerExit(col);
        }
    }
}
