using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyController : MonoBehaviour
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
            switch (gamesStatus.currentScene)
            {
                case "Castillo":
                    gamesStatus.hasCastilloKey = true;
                    Destroy(this.gameObject);
                    break;
                case "Desguace":
                    gamesStatus.hasDesguaceKey = true;
                    Destroy(this.gameObject);
                    break;
                case "Desierto":
                    gamesStatus.hasDesiertoKey = true;
                    Destroy(this.gameObject);
                    break;
                case "Volcan":
                    gamesStatus.hasVolcanKey = true;
                    Destroy(this.gameObject);
                    break;
            }
        }
    }

}
