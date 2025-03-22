using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CarPlayerControllerState : ICarState
{
    CarAI car;
    public CarPlayerControllerState(CarAI carAi)
    {
        this.car = carAi;
    }

    public bool isPlayerDriving()
    {
        return true;
    }

    public bool isIADriving()
    {
        return false;
    }

    
    public void GoToNoFuelState()
    {
        car.currentState = car.carNoFuelState;
    }

    public void GoToPlayerControllerState()
    {
    }

    public void OnTriggerStay(Collider col)
    { 
    }

    public void UpdateState()
    {
    }
}
