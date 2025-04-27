using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Cameras;

public class SetDesguaceSceneController : MonoBehaviour
{

    public GameStatus gameStatus = null;
    public GameStatus savedGameStatus = null;
    public GameObject cameraHolder, camera;
    public Cinemachine.CinemachineFreeLook freeLookCam, combatCamera;
    public GameObject playerPrefab;
    public GameObject playerInstantiatePoint;

    public GameObject escombrosConLlavePrefab, escombrosSinLlavePrefab;
    public GameObject escombrosPosition;

    public GameObject robotBossPrefab;
    public GameObject robotBossPosition;


    // Start is called before the first frame update
    void Start()
    {
        
        gameStatus.currentScene = SceneManager.GetActiveScene().name;
        GameObject playerInstantiated = Instantiate(playerPrefab, playerInstantiatePoint.transform.position, Quaternion.identity);

        freeLookCam.Follow = playerInstantiated.transform;
        freeLookCam.LookAt = playerInstantiated.transform;
        combatCamera.Follow = playerInstantiated.transform;
        combatCamera.LookAt = playerInstantiated.transform.GetChild(0).transform.GetChild(9).transform;


        MoveCamera mc = cameraHolder.GetComponent<MoveCamera>();
        mc.cameraPosition = playerInstantiated.transform.GetChild(2).transform;

        ThirdPersonCam tpc = camera.GetComponent<ThirdPersonCam>();
        tpc.orientation = playerInstantiated.transform.GetChild(1).transform;
        tpc.player = playerInstantiated.transform;
        tpc.playerObj = playerInstantiated.transform.GetChild(0).transform;
        tpc.rb = playerInstantiated.GetComponent<Rigidbody>();
        tpc.combatLookAt = playerInstantiated.transform.GetChild(0).transform.GetChild(9).transform;
        tpc.thirdPersonCam = freeLookCam.gameObject;
        tpc.combatCam = combatCamera.gameObject;


        freeLookCam.gameObject.SetActive(false);
        combatCamera.gameObject.SetActive(false);
        if (gameStatus.weaponEquipped == 1)
        {
            tpc.currentStyle = ThirdPersonCam.CameraStyle.Basic;
            freeLookCam.gameObject.SetActive(true);
        }
        else
        {
            tpc.currentStyle = ThirdPersonCam.CameraStyle.Shoot;
            combatCamera.gameObject.SetActive(true);
        }
        playerInstantiated.GetComponent<PlayerMovement>().SetWeapon();
        Helpers.saveGameStatus(gameStatus, savedGameStatus);

        if (!gameStatus.hasDesguaceKey)
        {
            Instantiate(escombrosConLlavePrefab, escombrosPosition.transform.position, Quaternion.identity);
        }
        else
        {
            GameObject escombroInstanciado = Instantiate(escombrosSinLlavePrefab, escombrosPosition.transform.position, Quaternion.identity);
            escombroInstanciado.transform.localScale = new Vector3(10,10,10);
        }

        if (!gameStatus.hasBullets)
        {
            Instantiate(robotBossPrefab, robotBossPosition.transform.position, Quaternion.identity);
        }



    }

    // Update is called once per frame
    void Update()
    {
    }
}
