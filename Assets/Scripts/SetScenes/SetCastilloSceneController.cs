using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Cameras;

public class SetCastilloSceneController : MonoBehaviour
{

    public GameStatus gameStatus = null;
    public GameStatus savedGameStatus = null;
    public GameObject cameraHolder, camera;
    public Cinemachine.CinemachineFreeLook freeLookCam, combatCamera;
    public GameObject playerPrefab;
    public GameObject playerInstantiatePoint;

    public GameObject porton;
    public GameObject dragonPrefab;
    public GameObject dragonPosition;
    public GameObject wolfPrefab;
    public List<GameObject> wolfsList;
    public List<GameObject> wolfsPosition;

    public GameObject bootsPrefab;
    public GameObject bootsPosition;


    // Start is called before the first frame update
    void Start()
    {
        if (gameStatus.allWolfsKilled)
        {
            RaiseGate();
        }
        else
        {
            foreach (GameObject g in wolfsPosition)
            {
                wolfsList.Add(Instantiate(wolfPrefab, g.transform.position, Quaternion.identity));
            }
        }
        gameStatus.currentScene = SceneManager.GetActiveScene().name;
        GameObject playerInstantiated = Instantiate(playerPrefab, playerInstantiatePoint.transform.position, Quaternion.identity);

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

        if (!gameStatus.hasCastilloKey)
        {
            Instantiate(dragonPrefab, dragonPosition.transform.position, Quaternion.identity);
        }


        if (!gameStatus.hasJumpBoots)
        {
            Instantiate(bootsPrefab, bootsPosition.transform.position, Quaternion.identity);
        }


    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject go in wolfsList)
        {
            if(go == null)
            {
                wolfsList.Remove(go);
            }
        }

        if(!gameStatus.allWolfsKilled && wolfsList.Count == 0)
        {
            gameStatus.allWolfsKilled = true;
            RaiseGate();
        }

    }

    public void RaiseGate()
    {
        //sonido
        porton.GetComponent<AudioSource>().Play();
        porton.transform.position = Vector3.Lerp(porton.transform.position, porton.transform.position + new Vector3(0, 15, 0), 25);
    }

}
