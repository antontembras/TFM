using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;
using static UnityEngine.UI.Image;

public class Shooter : MonoBehaviour
{
    public GameObject decalPrefab, bloodPrefab;
    public AudioClip m_fireSound;
    private AudioSource fireSound;
    Animator m_Animator;
    [HideInInspector] public float actualTimeBetweenShoots = 0;
    [HideInInspector] public float timeBetweenShoots = 0.1f;

    [HideInInspector] private GameplayManager gameplayManager;


    [HideInInspector] float timeStartHurt = 0;

    [HideInInspector] private bool isDying = false;
    [HideInInspector] float timeStartDying = 0;


    // Start is called before the first frame update
    void Start()
    {
      // gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
      // fireSound = GetComponent<AudioSource>();
      // timeBetweenShoots = gameplayManager.gameData.timeBetweenShoots;
        m_Animator = GetComponent<Animator>();
    }

    public void SetTimeBetweenShoots(float value)
    {
        timeBetweenShoots = value;
    }

    // Update is called once per frame
    void Update()
    {
        actualTimeBetweenShoots += Time.deltaTime;
        if (actualTimeBetweenShoots > timeBetweenShoots)
        {
            m_Animator.SetBool("isShooting", false);
           // if (Input.GetMouseButton(0) && gameplayManager.gameData.weaponAmmo > 0)
           // {
           //     fireSound.clip = m_fireSound;
           //     fireSound.Play(); 
           //     actualTimeBetweenShoots = 0;
           //
           //     m_Animator.SetBool("isShooting", true);
           //
           //     gameplayManager.gameData.weaponAmmo--;
           //
           //    // Instantiate(decalPrefab, transform.position, Quaternion.identity);
           //
           //     RaycastHit hit;
           //     //  if (Physics.Raycast(new Ray(transform.position, Vector3.forward), out hit, gameplayManager.gameData.weaponShootDistance))
           //     Vector3 direction = new Vector3(transform.forward.x, transform.forward.y + 1.0f, transform.forward.z);
           //     Vector3 position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
           //
           //     if (Physics.Raycast(new Ray(position, direction * 100f), out hit))
           //     {
           //         if (hit.collider.tag == "Enemy")
           //         {
           //        //   enemyAI eai = hit.collider.gameObject.GetComponent<enemyAI>();
           //        //   eai.Hit();
           //        //   eai.currentState.Impact();
           //         }
           //        else
           //        {
           //            Instantiate(decalPrefab, hit.point + hit.normal * 0.01f,
           //            Quaternion.FromToRotation(Vector3.forward, -hit.normal));
           //
           //             GameObject[] bulletDecalList = GameObject.FindGameObjectsWithTag("BulletQuad");
           //
           //             if (bulletDecalList.Length > 10)
           //             {
           //                 Destroy(bulletDecalList[0]);
           //             }
           //        }
           //     }
           // }
        }

        if (timeStartHurt > 0)
        {
            timeStartHurt -= Time.deltaTime;

            if (timeStartHurt <= 1.0f)
            {
                m_Animator.SetBool("isHurt", false);
            }
        }


        if (isDying)
        {
            timeStartDying -= Time.deltaTime;

            if (timeStartDying <= 0)
            {

                SceneManager.LoadScene("GameOver");
            }
        }
    }


    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Car")
        {
            if (Input.GetKey(KeyCode.F))
            {
                float nearestDistance = nearestCar();
                float carDistance = Vector3.Distance(col.gameObject.transform.position, transform.position);
                if (nearestDistance < carDistance)
                {
                    return;
                }

              //  if (col.gameObject.GetComponent<CarAI>().currentState.isIADriving())
              //  {
              //      gameplayManager.gameData.fuel += 10;
              //  }
              //  col.gameObject.GetComponent<CarAI>().navMeshAgent.speed = 0;
              //  col.gameObject.GetComponent<CarAI>().navMeshAgent.isStopped = true;
              //  Component.Destroy(col.gameObject.GetComponent<CarAI>().navMeshAgent);
              //  GameObject player = GameObject.FindGameObjectsWithTag("Player").First();
              //  GameObject.Destroy(gameObject);
              //  if (GameObject.FindGameObjectsWithTag("PlayerCar").FirstOrDefault() == null)
              //  {
              //      col.gameObject.tag = "PlayerCar";
              //      col.gameObject.GetComponent<CarAI>().currentState.GoToPlayerControllerState();
              //
              //  }

            }
         }
    }


    public float nearestCar()
    {
        if(carCollidersInTrigger.Count == 0 )
        {
            return 0;
        }
        float nearestDistance = Vector3.Distance(carCollidersInTrigger[0].gameObject.transform.position, transform.position);

        foreach (Collider c in carCollidersInTrigger){

            float newDistance = Vector3.Distance(c.gameObject.transform.position, transform.position);

            if(newDistance < nearestDistance)
            {
                nearestDistance = newDistance;
            }
        }
        return nearestDistance;
    }



    private List<Collider> carCollidersInTrigger = new List<Collider>();

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Car")
        {
            if (!carCollidersInTrigger.Contains(col))
            {
                carCollidersInTrigger.Add(col);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Car")
        {
            if (carCollidersInTrigger.Contains(col))
            {
                carCollidersInTrigger.Remove(col);
            }
        }
    }

    public void Hit(float damage)
    {
        if(timeStartHurt <= 0 && !isDying)
        {
            m_Animator.SetBool("isHurt", true);
            timeStartHurt = 1.5f;
            gameplayManager.gameData.playerLife -= damage * gameplayManager.gameData.damageMultiplier;

            if (gameplayManager.gameData.playerLife <= 0)
            {
                gameplayManager.gameData.playerLife = 0;
                Die();
            }
            else
            {
                Instantiate(bloodPrefab, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
            }
        }
    }

    public void Die()
    {
        isDying = true;
        timeStartDying = 2.0f;
        m_Animator.SetBool("isDead", true);
        Destroy(gameObject.GetComponent<BoxCollider>());
        Destroy(gameObject.GetComponent<ThirdPersonUserControl>());
        Destroy(gameObject.GetComponent<ThirdPersonCharacter>());
        Destroy(gameObject.GetComponent<Rigidbody>());
    }



}
