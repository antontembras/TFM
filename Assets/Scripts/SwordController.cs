using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    public bool isMoonSword;
    [HideInInspector] public float actualTimeBetweenAttacks = 0;
    [HideInInspector] public float timeBetweenAttacks = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(actualTimeBetweenAttacks <= timeBetweenAttacks)
        {
            actualTimeBetweenAttacks += Time.deltaTime;
        }
        if (actualTimeBetweenAttacks > timeBetweenAttacks)
        {
            if (Input.GetMouseButton(0))
            {
                actualTimeBetweenAttacks = 0;
            }
        }
    }


    void OnTriggerEnter(Collider collider)
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
            eai.currentState.Impact();
        }
    }


}
