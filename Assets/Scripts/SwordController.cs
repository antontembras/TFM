using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    public bool isMoonSword;
    public float actualTimeBetweenAttacks = 3.0f;
    public float timeBetweenAttacks = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (actualTimeBetweenAttacks <= timeBetweenAttacks)
            {
                actualTimeBetweenAttacks += Time.deltaTime;
            }
            if (actualTimeBetweenAttacks > timeBetweenAttacks)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<AudioSource>().Play();
                    actualTimeBetweenAttacks = 0;
                }
            }
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        if (Time.timeScale != 0)
        {
            if (collider.CompareTag("Enemy") && actualTimeBetweenAttacks <= timeBetweenAttacks)
            {
                enemyAI eai = collider.gameObject.GetComponent<enemyAI>();
                if (isMoonSword)
                {
                    eai.HitMoonSword();
                }
                else
                {
                    eai.HitSword();
                }
            }
        }
    }


}
