using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
[System.Serializable]
public class GameData : ScriptableObject
{
    public int weaponAmmo = 10;
    public float playerLife = 100.0f;
    public float weaponShootDistance = 100.0f;
    public float timeBetweenShoots = 1.0f;
    public float fuel = 30.0f;
    public int points = 0;
    public float timePlaying = 0;
    public int timeToSpawn = 30;
    public float damageMultiplier = 1f;
    public int randomDropItemMaxValue = 5;
}
