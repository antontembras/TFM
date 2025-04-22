using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFirstAidKit : MonoBehaviour
{
    public GameStatus gamesStatus = null;
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
            if (gamesStatus.playerLife > 100) {
                gamesStatus.playerLife = 100;
            }
            Destroy(this.gameObject);
        }
    }
}
