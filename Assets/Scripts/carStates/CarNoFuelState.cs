using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Cameras;

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
       if (car.gameStatus.hasFuel && car.gameStatus.hasCarKeys && car.player != null)
       {
           if (Vector3.Distance(car.gameObject.transform.position, car.player.transform.position) < 10 &&  (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button2)))
           {
               GameObject player = GameObject.FindGameObjectsWithTag("Player").First();
                car.freeLookCam.gameObject.SetActive(false);
                car.combatCamera.gameObject.SetActive(false);

                car.tpc.currentStyle = ThirdPersonCam.CameraStyle.Basic;


                car.tpc.player = car.transform;
                car.tpc.playerObj = car.transform;
                car.tpc.rb = car.GetComponent<Rigidbody>();
                car.freeLookCam.Follow = car.transform.GetChild(0).transform;
                car.freeLookCam.LookAt = car.transform.GetChild(0).transform;

                car.gameStatus.isPlayerDriving = true;

                car.freeLookCam.gameObject.SetActive(true);
                GameObject.Destroy(player);
                GoToPlayerControllerState();
            }
       
       }
    }
}
