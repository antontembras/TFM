using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalController : MonoBehaviour
{
    public GameStatus gameStatus = null;
    public GameStatus savedGameStatus = null;
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
        InitializeGameStatus(gameStatus);
        InitializeGameStatus(savedGameStatus);
        SceneManager.LoadScene("Laberinto");
    }

    public void InitializeGameStatus(GameStatus gs)
    {
        gs.previousScene = SceneManager.GetActiveScene().name;
        gs.currentScene = SceneManager.GetActiveScene().name;
        gs.hasMoonSword = false;
        gs.hasBullets = false;
        gs.hasRevolver = false;
        gs.weaponEquipped = 1;
        gs.playerLife = 100;
        gs.hasCastilloKey = false;
        gs.hasDesguaceKey = false;
        gs.hasDesiertoKey = false;
        gs.hasVolcanKey = false;
        gs.hasJumpBoots = false;
        gs.isPlayerDriving = false;
        gs.allWolfsKilled = false;
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
