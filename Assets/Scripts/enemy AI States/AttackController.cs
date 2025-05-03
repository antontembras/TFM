using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // Start is called before the first frame update
    public enemyAI myEnemyAI;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && myEnemyAI.m_Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.EndsWith("Attack") && myEnemyAI.currentState == myEnemyAI.attackState)
        {
            enemyAI eai = collider.gameObject.GetComponent<enemyAI>();
            collider.gameObject.GetComponentInParent<PlayerMovement>().Hit(myEnemyAI.attackForce);
        }
    }
}
