using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLaberintoController : MonoBehaviour
{


    public GameStatus gamesStatus = null;
    public GameObject player;
    public List<GameObject> playerInstantiatePoints;
    public GameObject cameraHolder, camera;
    public Cinemachine.CinemachineFreeLook freeLookCam, combatCamera;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerInstantiated = null;
        switch (gamesStatus.previousScene)
        {
            case "MenuPrincipal":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[0].transform.position, Quaternion.identity);
                break;
            case "Desguace":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[1].transform.position, Quaternion.identity);
                break;
            case "Desierto":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[2].transform.position, Quaternion.Euler(0, 180, 0));
                break;
            case "Castillo":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[3].transform.position, Quaternion.identity);
                break;
            case "Plataformas":
                playerInstantiated = Instantiate(player, playerInstantiatePoints[4].transform.position, Quaternion.identity);
                break;
        }
        gamesStatus.currentScene = SceneManager.GetActiveScene().name;

        freeLookCam.Follow = playerInstantiated.transform;
        freeLookCam.LookAt = playerInstantiated.transform;
        combatCamera.Follow = playerInstantiated.transform;
        combatCamera.LookAt = playerInstantiated.transform.GetChild(3).transform;


        MoveCamera mc = cameraHolder.GetComponent<MoveCamera>();
        mc.cameraPosition = playerInstantiated.transform.GetChild(2).transform;

        ThirdPersonCam tpc = camera.GetComponent<ThirdPersonCam>();
        tpc.orientation = playerInstantiated.transform.GetChild(1).transform;
        tpc.player = playerInstantiated.transform;
        tpc.playerObj = playerInstantiated.transform.GetChild(0).transform;
        tpc.rb = playerInstantiated.GetComponent<Rigidbody>();
        tpc.combatLookAt = playerInstantiated.transform.GetChild(3).transform;
        tpc.thirdPersonCam = freeLookCam.gameObject;
        tpc.combatCam = combatCamera.gameObject;

        if (gamesStatus.weaponEquipped == 1)
        {
            tpc.currentStyle = ThirdPersonCam.CameraStyle.Basic;
        }
        else
        {
            tpc.currentStyle = ThirdPersonCam.CameraStyle.Shoot;
        }

        playerInstantiated.GetComponent<PlayerMovement>().SetWeapon();



    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
