using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseCondition : MonoBehaviour
{
    [SerializeField] Text timerText, currentScoreText, needScoreText;
    [SerializeField] int needScore;
    [SerializeField] float Minute, Second;
    [Header("Endings")]
    [SerializeField] GameObject WinEnding, LoseEnding, InGame, GeneralEnding, AndroidCanvas;
    [SerializeField] Text GETotalScore, GEDriftScore, GETimerScore;
    int Score;


    public CarController CarObject;
    [SerializeField] SpawnPoint spawnPoint;

    PlayerProgress playerprogress;

    bool gameEnded = false;
    public int _Score
    {
        set => Score = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        CarObject = spawnPoint._car.GetComponent<CarController>();
        playerprogress = GameObject.Find("PlayerProgress").GetComponent<PlayerProgress>();
        SetNeedScore();
        InGame.SetActive(true);
        WinEnding.SetActive(false);
        LoseEnding.SetActive(false);
        GeneralEnding.SetActive(false);
    }


    private void SetNeedScore()
    {
        needScoreText.text = "Reach: " + needScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnded)
        {
            SetTimer();

        }
    }
    private void FixedUpdate()
    {
        if (CarObject != null)
        {
            SetCurrentScore();
        }
    }

    public void SetCurrentScore()
    {
        if (CarObject._isDrifting)
        {
            Score += 5;
            currentScoreText.text = Score + "";
        }
    }

    private void SetTimer()
    {
        Second -= Time.deltaTime;
        if (Second <= 0)
        {
            Minute--;
            Second = 60;
            if (Minute == -1)
            {
                GameEnding();
            }
        }
        timerText.text = Minute + ":";
        if ((int)Second < 10) timerText.text += "0";
        timerText.text += (int)Second;
    }

    public void GameEnding()
    {
        gameEnded = true;
        
        AndroidCanvas.SetActive(false);
        GEDriftScore.text = "Drift Score: " + Score;
        GETimerScore.text = "Timer Score: " + (int)(Second + Minute * 60) * 5;
        Score += (int)(Second + Minute * 60) * 2;
        GETotalScore.text = "Total Score: " + Score + " / " + needScore;
        InGame.SetActive(false);
        GeneralEnding.SetActive(true);
        playerprogress.totalScore += Score;

        if (Score > needScore && Minute + Second >= 0)
        {
            WinEnding.SetActive(true);
            //Win
        }
        else
        {
            LoseEnding.SetActive(true);
            //Lose
        }
    }
}
