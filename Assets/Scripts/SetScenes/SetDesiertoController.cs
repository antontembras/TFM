using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDesiertoController : MonoBehaviour
{
    public GameStatus gameStatus = null;
    public GameStatus savedGameStatus = null;
    public GameObject cameraHolder, camera;
    public Cinemachine.CinemachineFreeLook freeLookCam, combatCamera;
    public GameObject playerPrefab;
    public GameObject playerInstantiatePoint;

    public GameObject revolverPrefab;
    public GameObject revolverPosition;

    public GameObject fuelPrefab;
    public GameObject fuelPosition;


    public GameObject keyPrefab;
    public GameObject keyPosition;

    public EnemySpawnController enemySpawnController;
    // Start is called before the first frame update
    void Start()
    {

        gameStatus.currentScene = SceneManager.GetActiveScene().name;
        GameObject playerInstantiated = Instantiate(playerPrefab, playerInstantiatePoint.transform.position, Quaternion.Euler(0, 180, 0));


        enemySpawnController.player = playerInstantiated;

        freeLookCam.Follow = playerInstantiated.transform;
        freeLookCam.LookAt = playerInstantiated.transform;
        combatCamera.Follow = playerInstantiated.transform;
        combatCamera.LookAt = playerInstantiated.transform.GetChild(0).transform.GetChild(9).transform;



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

        if (!gameStatus.hasRevolver)
        {
            Instantiate(revolverPrefab, revolverPosition.transform.position, Quaternion.identity).transform.localScale = new Vector3(15, 15, 15);
        }

        if (!gameStatus.hasFuel)
        {
            Instantiate(fuelPrefab, fuelPosition.transform.position, Quaternion.Euler(270,0,0)).transform.localScale = new Vector3(1, 1, 1);
        }



        if (!gameStatus.hasDesiertoKey)
        {
            Instantiate(keyPrefab, keyPosition.transform.position, Quaternion.identity);
        }


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
