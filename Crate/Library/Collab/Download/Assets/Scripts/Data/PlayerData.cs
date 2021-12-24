using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int highscore;

    public PlayerData(Player_Control player)
    {
        money = player.money;
        highscore = player.highscore;
    }
}
