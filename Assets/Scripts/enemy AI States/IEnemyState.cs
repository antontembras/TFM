using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void UpdateState();

    void GoToAttackState();
    void GoToRunState(Vector3 destination);
    void GoToPatrolState();

    void OnTriggerEnter(Collider col);
    void OnTriggerStay(Collider col);
    void OnTriggerExit(Collider col);

    void Impact();
}
