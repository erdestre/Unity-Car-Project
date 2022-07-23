using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointRace : MonoBehaviour
{
    GameObject[] CheckPoints;
    int whichCheckPoint = 0;

    GameObject car;
    private void Start()
    {
        CheckPoints = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            CheckPoints[i] = gameObject.transform.GetChild(i).gameObject;
        }

        for (int i = 1; i < CheckPoints.Length; i++)
        {
            CheckPoints[i].SetActive(false);
        }

        car = GameObject.Find("PlayerCar").gameObject;
    }
    public void Next()
    {
        CheckPoints[whichCheckPoint].SetActive(false);
        whichCheckPoint++;
        if (whichCheckPoint == CheckPoints.Length - 1)
        {
            FindObjectOfType<WinLoseCondition>().GameEnding();
        }
        else CheckPoints[whichCheckPoint].SetActive(true);
    }
    public void Resetcar()
    {
        car.transform.position = CheckPoints[whichCheckPoint - 1].transform.position;
        car.transform.rotation = CheckPoints[whichCheckPoint - 1].transform.rotation;
        car.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        car.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
    }
}
