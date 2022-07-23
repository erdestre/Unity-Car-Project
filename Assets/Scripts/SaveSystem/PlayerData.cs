using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int gold;
    public int totalScore;

    public int[,,] Cars;

    public PlayerData(PlayerProgress playerProgress)
    {
        gold = playerProgress.gold;
        totalScore = playerProgress.totalScore;

    }
}
