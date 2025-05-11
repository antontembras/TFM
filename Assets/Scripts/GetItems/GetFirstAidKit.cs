using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetFirstAidKit : MonoBehaviour
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
            gamesStatus.playerLife += 10;
            GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().clip = itemSound;
            GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<AudioSource>().Play();
            if (gamesStatus.playerLife > 100) {
                gamesStatus.playerLife = 100;
            }
            Destroy(this.gameObject);
        }
    }
}
