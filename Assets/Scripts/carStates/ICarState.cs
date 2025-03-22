using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarState
{

    bool isPlayerDriving();
    bool isIADriving();

    void UpdateState();

    void GoToPlayerControllerState();
    void GoToNoFuelState();
    void OnTriggerStay(Collider col);
}
