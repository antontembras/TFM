using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTouchController : MonoBehaviour
{

    public string keyNameNeeded = "";
    public GameStatus gamesStatus = null;
    public LaberintoSceneStatusController laberintoSceneStatusController = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            switch (keyNameNeeded)
            {
                case "Castillo":
                    if (gamesStatus.hasCastilloKey)
                    {
                        laberintoSceneStatusController.locksOpened++;
                        Destroy(this.gameObject);
                    }
                    break;
                case "Desguace":
                    if (gamesStatus.hasDesguaceKey)
                    {
                        laberintoSceneStatusController.locksOpened++;
                        Destroy(this.gameObject);
                    }
                    break;
                case "Desierto":
                    if (gamesStatus.hasDesiertoKey)
                    {
                        laberintoSceneStatusController.locksOpened++;
                        Destroy(this.gameObject);
                    }
                    break;
                case "Volcan":
                    if (gamesStatus.hasVolcanKey)
                    {
                        laberintoSceneStatusController.locksOpened++;
                        Destroy(this.gameObject);
                    }
                    break;
            }
        }
    }


}
