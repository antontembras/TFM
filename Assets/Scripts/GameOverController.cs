using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public GameStatus gameStatus;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(gameStatus.currentScene);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }

    }
}
