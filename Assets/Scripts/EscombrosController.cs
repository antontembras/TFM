using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscombrosController : MonoBehaviour
{

    public GameObject llavePrefab;
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
        if (collider.CompareTag("Car"))
        {
            if (llavePrefab != null && GameObject.FindGameObjectsWithTag("Key").Count() == 0)
            {
                Instantiate(llavePrefab, transform.position-new Vector3(0,6,0), Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
}
