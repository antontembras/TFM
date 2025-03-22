using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class GameplayManager : MonoBehaviour
{

    public GameData gameData = null;

    public GameObject playerObjectPrefab, boxAmmoPrefab, boxLifePrefab;

    public TextMeshProUGUI textoVida, textoMunicion, textoTiempo, textoPuntos, textoCombustible;

    public float timeToSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameData.playerLife = 100;
        gameData.weaponAmmo = 100;
        gameData.fuel = 30;
        gameData.points = 0;
        gameData.timePlaying = 0;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        textoVida.text = gameData.playerLife.ToString("000");
        textoMunicion.text = gameData.weaponAmmo.ToString("000");
        textoCombustible.text = gameData.fuel.ToString("000");
        textoPuntos.text = gameData.points.ToString("00000");


        if (GameObject.FindGameObjectsWithTag("Player").Count() > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Player").FirstOrDefault());
        }

        if (GameObject.FindGameObjectsWithTag("PlayerCar").Count() > 1)
        {
            GameObject carRepeat = GameObject.FindGameObjectsWithTag("PlayerCar").FirstOrDefault();
            carRepeat.tag = "Car";

           //CarAI carAI = carRepeat.GetComponent<CarAI>();
           //
           //if(carAI.startingFuel > 0)
           //{
           //    carAI.currentState.GoToIANavigationState();
           //}
           //else
           //{
           //    carAI.currentState.GoToNoFuelState();
           //}

        }


        GameObject currentPlayerObject = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
       // if(currentPlayerObject != null)
       // {
       //     GameObject.FindGameObjectsWithTag("FreeLookCamera").FirstOrDefault().GetComponent<FreeLookCam>().Target = currentPlayerObject.transform;
       // }
       // else
       // {
       //     GameObject currentPlayerCarObject = GameObject.FindGameObjectsWithTag("PlayerCar").FirstOrDefault();
       //     if (currentPlayerCarObject != null)
       //     {
       //         GameObject.FindGameObjectsWithTag("FreeLookCamera").FirstOrDefault().GetComponent<FreeLookCam>().Target = currentPlayerCarObject.transform;
       //     }
       //
       // }

        timeToSpawn += Time.deltaTime;
        if(timeToSpawn >= gameData.timeToSpawn)
        {
            timeToSpawn = 0;
            Spawn();
        }
        gameData.timePlaying += Time.deltaTime;

        TimeSpan time = TimeSpan.FromSeconds(gameData.timePlaying);
        textoTiempo.text = time.ToString(@"hh\:mm\:ss");

    }

    void Spawn()
    {
       // GameObject p = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
       // GameObject currentPlayerObject = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
       // if (currentPlayerObject == null)
       // {
       //     currentPlayerObject = GameObject.FindGameObjectsWithTag("PlayerCar").FirstOrDefault();
       // }
       // if (currentPlayerObject != null)
       // { 
       //     List<GameObject>  listSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();
       //     foreach(GameObject g in listSpawnPoints)
       //     {
       //         float distancia = Vector3.Distance(g.transform.position, currentPlayerObject.transform.position);
       //         if (distancia >= 100)
       //         {
       //             g.GetComponent<SpawnPointController>().Spawn();
       //         }
       //     }
       // }
    }
}
