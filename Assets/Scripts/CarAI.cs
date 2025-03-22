using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour
{
    [HideInInspector] public CarPlayerControllerState carPlayerControllerState;
    [HideInInspector] public CarNoFuelState carNoFuelState;
    [HideInInspector] public ICarState currentState;

    [HideInInspector] public GameObject player;
    [HideInInspector] public float startingFuel = 10.0f;




    // Start is called before the first frame update
    void Start()
    {
        carPlayerControllerState = new CarPlayerControllerState(this);
        carNoFuelState = new CarNoFuelState(this);


        currentState = carPlayerControllerState;

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();

        player = GameObject.FindGameObjectWithTag("Player");
        if (currentState.isPlayerDriving())
        {
           //if(startingFuel > 0)
           //{
           //    startingFuel -= Time.deltaTime;
           //    gameplayManager.gameData.fuel -= Time.deltaTime;
           //}
           //else
           //{
           //    gameplayManager.gameData.fuel -= Time.deltaTime;
           //    if (gameplayManager.gameData.fuel <= 0)
           //    {
           //        gameplayManager.gameData.fuel = 0;
           //        gameObject.tag = "Car";
           //        Instantiate(gameplayManager.playerObjectPrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
           //
           //        currentState.GoToNoFuelState();
           //    }
           //}
        }

        
    }

    private void OnTriggerEnter(Collider col)
    {
       // if (col.gameObject.tag == "Enemy"  && currentState.isPlayerDriving())
       // {
       //     col.gameObject.GetComponent<enemyAI>().explode();
       // }
    }

}
