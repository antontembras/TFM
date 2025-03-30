using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalController : MonoBehaviour
{
    public GameStatus gamesStatus = null;
    // Start is called before the first frame update
    void Start()
    {
        gamesStatus.previousScene = SceneManager.GetActiveScene().name;
        gamesStatus.currentScene = SceneManager.GetActiveScene().name;
        gamesStatus.hasMoonSword = false;
        gamesStatus.hasBullets = false;
        gamesStatus.hasRevolver = false;
        gamesStatus.weaponEquipped = 1;
        SceneManager.LoadScene("Laberinto");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
