using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LockTouchController : MonoBehaviour
{

    public string keyNameNeeded = "";
    public GameStatus gamesStatus = null;
    public LaberintoSceneStatusController laberintoSceneStatusController = null;
    public AudioClip unlockSound;

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
                        GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().clip = unlockSound;
                        GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().Play();
                        laberintoSceneStatusController.locks.Remove(gameObject);
                        Destroy(this.gameObject);
                    }
                    break;
                case "Desguace":
                    if (gamesStatus.hasDesguaceKey)
                    {
                        GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().clip = unlockSound;
                        GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().Play();
                        laberintoSceneStatusController.locks.Remove(gameObject);
                        Destroy(this.gameObject);
                    }
                    break;
                case "Desierto":
                    if (gamesStatus.hasDesiertoKey)
                    {
                        GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().clip = unlockSound;
                        GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().Play();
                        laberintoSceneStatusController.locks.Remove(gameObject);
                        Destroy(this.gameObject);
                    }
                    break;
                case "Volcan":
                    if (gamesStatus.hasVolcanKey)
                    {
                        GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().clip = unlockSound;
                        GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().Play();
                        laberintoSceneStatusController.locks.Remove(gameObject);
                        Destroy(this.gameObject);
                    }
                    break;
            }
        }
    }


}
