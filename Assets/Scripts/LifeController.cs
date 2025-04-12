using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{

    public GameStatus gameStatus;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = gameStatus.playerLife / 100F;
    }
}
