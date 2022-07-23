using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] int maxCarNumber;
    
    [Header("Default Settings")]
    public int gold = 0;
    public int totalScore = 0;

    public int selectedCar;

    public Car currentCar;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("PlayerProgress"))
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            gameObject.tag = "PlayerProgress";
        } 
            
    }
    public void SaveProgress()
    {
        SaveSystem.SavePlayerProgress(this);
    }
    public void LoadProgress()
    {
        PlayerData data = SaveSystem.LoadPlayerProgress();

        gold = data.gold;
        totalScore = data.totalScore;

    }
}
