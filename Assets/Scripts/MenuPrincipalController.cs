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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jugar()
    {
        gamesStatus.previousScene = SceneManager.GetActiveScene().name;
        gamesStatus.currentScene = SceneManager.GetActiveScene().name;
        gamesStatus.hasMoonSword = false;
        gamesStatus.hasBullets = false;
        gamesStatus.hasRevolver = false;
        gamesStatus.weaponEquipped = 1;
        SceneManager.LoadScene("Laberinto");
    }
    public void Instrucciones()
    {
        SceneManager.LoadScene("Instrucciones");
    }
    public void Opciones()
    {
        SceneManager.LoadScene("Options");
    }
    public void Salir()
    {
        Application.Quit();
    }


}
