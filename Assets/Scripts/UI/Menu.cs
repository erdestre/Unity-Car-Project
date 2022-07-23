/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GoldText;
    [SerializeField] TextMeshProUGUI ScoreText;

    int currentCarID = 0;

    int whichType = 0, whichOne = 0, cost; //ModificationsAndUpgrades Values
    public int _cost
    {
        set => cost = value;
    }
    public int _whichOne
    {
        set
        {
            int number = value;
            whichType = number / 10;
            whichOne = number - whichType;
            
            Market(currentCarID, whichType, whichOne, cost);
        }
    }
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
        //playerProgress.Cars[x, y, z] = 1;
    }
    private void Upgrade(int x, int y, int z) //x Upgrade Type /Y level //
    {
        //playerProgress.Cars[x, y, z]++;
    }

    private void updateText()
    {
        GoldText.text = playerProgress.gold.ToString();
        ScoreText.text = playerProgress.totalScore.ToString();

    }
}
*/