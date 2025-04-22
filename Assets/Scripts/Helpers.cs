using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Helpers
{
    public static void saveGameStatus(GameStatus gs1, GameStatus gs2)
    {
        gs2.previousScene = gs1.previousScene;
        gs2.currentScene = gs1.currentScene;
        gs2.hasMoonSword = gs1.hasMoonSword;
        gs2.hasBullets = gs1.hasBullets;
        gs2.hasRevolver = gs1.hasRevolver;
        gs2.weaponEquipped = gs1.weaponEquipped;
        gs2.playerLife = gs1.playerLife;
        gs2.hasCastilloKey = gs1.hasCastilloKey;
        gs2.hasDesguaceKey = gs1.hasDesguaceKey;
        gs2.hasDesiertoKey = gs1.hasDesiertoKey;
        gs2.hasVolcanKey = gs1.hasVolcanKey;
        gs2.hasFuel = gs1.hasFuel;
        gs2.hasCarKeys = gs1.hasCarKeys;
        gs2.hasJumpBoots = gs1.hasJumpBoots;
        gs2.allWolfsKilled = gs1.allWolfsKilled;
    }
}
