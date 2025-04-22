using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBoundsController : MonoBehaviour
{
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
            SceneManager.LoadScene("GameOver");
        }
        if (collider.CompareTag("Enemy") && SceneManager.GetActiveScene().name == "Desierto")
        {
            Debug.Log(DateTime.Now + " eliminando enemigo por fuera de escenario " + this.gameObject.name);
            Destroy(collider.gameObject);
        }
    }

}
