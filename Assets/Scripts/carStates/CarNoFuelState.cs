using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarNoFuelState : ICarState
{
    CarAI car;
    public CarNoFuelState(CarAI carAi)
    {
        this.car = carAi;
    }

    public bool isPlayerDriving()
    {
        return false;
    }

    public bool isIADriving()
    {
        return false;
    }

    public void GoToNoFuelState()
    {
    }

    public void GoToPlayerControllerState()
    {
        car.currentState = car.carPlayerControllerState;
    }

    public void OnTriggerStay(Collider col)
    {
    }

    public void UpdateState()
    {
       //if (car.gameplayManager.gameData.fuel > 0 && car.player != null)
       //{
       //    if (Vector3.Distance(car.gameObject.transform.position, car.player.transform.position) < 10 &&  Input.GetKey(KeyCode.F))
       //    {
       //        GameObject player = GameObject.FindGameObjectsWithTag("Player").First();
       //        GameObject.Destroy(player);
       //        car.gameObject.tag = "PlayerCar";
       //        GoToPlayerControllerState();
       //    }
       //
       //}
    }
}
