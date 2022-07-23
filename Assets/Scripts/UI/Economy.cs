using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GoldText;
    [SerializeField] TextMeshProUGUI ScoreText;

    int currentCarID = 0;
    GameObject[,] ModificationsAndUpgrades;

    
    int x = 0, y = 0, cost; //ModificationsAndUpgrades Values
    /*public int whichOne
    {
        set
        {
            int number = value;
            
            int whichCar = number /= 100;;
            x = firstOne;
            int whichType = value /10
            y = value - firstOne;
            Market(x,y,cost);
            Debug.Log("Pressed: " + x + "  " + y);
        }
    }*/
    PlayerProgress playerProgress;
    private void Start()
    {
        playerProgress = GameObject.Find("PlayerProgress").GetComponent<PlayerProgress>();

        updateText();


    }

    public void Market(int x, int y, int z, int Cost)
    {
        if (playerProgress.gold >= Cost)
        {
            if (x < 4)
            {
                Modification(x, y, z);
            }
            else
            {
                Upgrade(x, y, z);
            }

            playerProgress.gold -= Cost;
            updateText();
        }
    }

    private void Modification(int x, int y, int z) //x Modification Type / Y whichOne
    {
        //playerProgress.carModificationsAndUpgrades[x, y] = z;
    }
    private void Upgrade(int x, int y, int z) //x Upgrade Type /Y level //
    {
        //playerProgress.carModificationsAndUpgrades[x, y]++;
    }

    private void updateText()
    {
        GoldText.text = playerProgress.gold.ToString();
        ScoreText.text = playerProgress.totalScore.ToString();

    }
}
