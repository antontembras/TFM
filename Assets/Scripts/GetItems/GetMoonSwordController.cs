using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetMoonSwordController : MonoBehaviour
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
            gamesStatus.hasMoonSword = true;
            gamesStatus.weaponEquipped = 1;
            GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<PlayerMovement>().NewWeapon();
            Destroy(this.gameObject);
        }
    }

}
