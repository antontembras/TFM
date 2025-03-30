using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Cameras;

public class SetSceneController : MonoBehaviour
{

    public GameStatus gamesStatus = null;
    public GameObject cameraHolder, camera;
    public Cinemachine.CinemachineFreeLook freeLookCam, combatCamera;
    public GameObject playerPrefab;
    public GameObject playerInstantiatePoint;


    // Start is called before the first frame update
    void Start()
    {
        gamesStatus.currentScene = SceneManager.GetActiveScene().name;
        GameObject playerInstantiated = Instantiate(playerPrefab, playerInstantiatePoint.transform.position, Quaternion.identity);

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

        if(gamesStatus.weaponEquipped == 1)
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
