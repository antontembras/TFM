using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{

    public GameStatus gameStatus;
    public Image healthBar;
    public Image crosshair;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = gameStatus.playerLife / 100F;
        if(gameStatus.weaponEquipped == 1 || gameStatus.isPlayerDriving)
        {
            crosshair.color = Color.clear;
        }
        else
        {
            crosshair.color = Color.red;
        }
    }
}
