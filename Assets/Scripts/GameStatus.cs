using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
[System.Serializable]
public class GameStatus : ScriptableObject
{
    public int weaponEquipped = 1;
    public float playerLife = 100.0f;
    public bool hasMoonSword = false;
    public bool hasRevolver = false;
    public bool hasBullets = false;
    public string previousScene = "";
    public string currentScene = "";


}
