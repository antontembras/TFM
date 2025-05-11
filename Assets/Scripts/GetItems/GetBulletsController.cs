using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetBulletsController : MonoBehaviour
{

    public GameStatus gamesStatus = null;
    public AudioClip itemSound;
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
            gamesStatus.hasBullets = true;
            GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().clip = itemSound;
            GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().Play();
            if (gamesStatus.hasRevolver)
            {
                gamesStatus.weaponEquipped = 2;
                GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<PlayerMovement>().NewWeapon();
            }
            Destroy(this.gameObject);
        }
    }

}
