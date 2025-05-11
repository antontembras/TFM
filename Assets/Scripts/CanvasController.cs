using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{

    public GameStatus gameStatus;
    public Image healthBar;
    public Image crosshair;
    public Text textoPausa;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = gameStatus.playerLife / 100F;
        if(Time.timeScale != 0)
        {
            if (gameStatus.weaponEquipped == 1 || gameStatus.isPlayerDriving)
            {
                crosshair.color = Color.clear;
            }
            else
            {
                crosshair.color = Color.red;
            }
        }
        else
        {
            crosshair.color = Color.clear;
        }

        if (Time.timeScale == 0)
        {
            textoPausa.color = Color.black;
        }
        else
        {
            textoPausa.color = Color.clear;
        }

    }
}
