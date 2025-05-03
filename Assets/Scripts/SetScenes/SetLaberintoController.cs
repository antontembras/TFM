using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLaberintoController : MonoBehaviour
{


    public GameStatus gameStatus = null;
    public GameStatus savedGameStatus = null;
    public GameObject player;
    public List<GameObject> playerInstantiatePoints;
    public GameObject cameraHolder, camera;
    public Cinemachine.CinemachineFreeLook freeLookCam, combatCamera;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerInstantiated = null;

        gameStatus.currentScene = SceneManager.GetActiveScene().name;
        switch (gameStatus.previousScene)
        {
            case "MenuPrincipal":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[0].transform.position, Quaternion.identity);
                break;
            case "Desguace":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[1].transform.position, Quaternion.Euler(0, 90, 0));
                break;
            case "Desierto":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[2].transform.position, Quaternion.Euler(0, 180, 0));
                break;
            case "Castillo":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[3].transform.position, Quaternion.Euler(0, 180, 0));
                break;
            case "Volcan":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[4].transform.position, Quaternion.identity);
                break;
        }

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
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
