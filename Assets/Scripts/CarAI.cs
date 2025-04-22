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

    public GameStatus gameStatus;
    public GameObject cameraHolder, camera;
    public Cinemachine.CinemachineFreeLook freeLookCam, combatCamera;
    public ThirdPersonCam tpc;
    public GameObject playerObjectPrefab;


    // Start is called before the first frame update
    void Start()
    {
        carPlayerControllerState = new CarPlayerControllerState(this);
        carNoFuelState = new CarNoFuelState(this);


        currentState = carNoFuelState;
        tpc = camera.GetComponent<ThirdPersonCam>();

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null && Input.GetKeyDown(KeyCode.F))
        {
            GameObject playerInstantiated = Instantiate(playerObjectPrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity);

            freeLookCam.Follow = playerInstantiated.transform;
            freeLookCam.LookAt = playerInstantiated.transform;
            combatCamera.Follow = playerInstantiated.transform;
            combatCamera.LookAt = playerInstantiated.transform.GetChild(3).transform;

            freeLookCam.gameObject.SetActive(false);
            combatCamera.gameObject.SetActive(false);
            gameStatus.isPlayerDriving = false;
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
            currentState.GoToNoFuelState();

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
