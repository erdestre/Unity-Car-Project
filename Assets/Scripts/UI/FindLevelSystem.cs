using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FindLevelSystem : EventTrigger
{
    public int LevelIndex;
    LoadingScreen LevelSystem;
    void Start()
    {
        LevelSystem = GameObject.Find("LevelSystem").GetComponent<LoadingScreen>();
    }
}
