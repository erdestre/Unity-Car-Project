using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    PlayerProgress playerProgress;

    [Header("Market")]
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] GameObject PurchaseCarButtonPrefab;
    [SerializeField] GameObject[] ModificationsAndUpgrades;
    [SerializeField] Transform SpawnPoint;
    public GameObject[] Slot;
    public List<Car> Cars;
    [HideInInspector]
    public List<GameObject> CurrentButtons;
    private GameObject currentPurchaseCarButton;
    GameObject currentCar;
    int currentCarId = 0;

    [Header("Player Economy")]
    [SerializeField] TextMeshProUGUI ScoreText;

    private void Start()
    {
        playerProgress = GameObject.Find("PlayerProgress").GetComponent<PlayerProgress>();

        updateText();
        UpdateMarket(currentCarId);
        SpawnCar();
    }
    void ApplyColortoCar(Color color)
    {
        GameObject.Find("Body").GetComponent<MeshRenderer>().material.color = playerProgress.currentCar.currentColor;
    }
    public void Purchase(Items item, GameObject button)
    {
        Debug.Log("basýldý");
        if (!item.isAlreadyPurchased)
        {
            if (playerProgress.totalScore >= item.itemCost)
            {
                playerProgress.totalScore -= item.itemCost;
                updateText();
                item.isAlreadyPurchased = true;
                button.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName + "\nPurchased";
            }
            else
            {
                //Popup
                Debug.Log("You don't have enough money");
            }
        }
        if (item.isAlreadyPurchased)
        {
            switch (item.itemTypeId)
            {
                case 0:
                    Cars[currentCarId].currentColor = item.color;
                    GameObject.Find("Body").GetComponent<MeshRenderer>().material.color = item.color;
                    ApplyColortoCar(item.color);
                    break;
                case 1:
                    Cars[currentCarId].currentTest2 = item.test2;
                    break;
                case 2:
                    Cars[currentCarId].currentTest3 = item.test3;
                    break;
                case 3:
                    Cars[currentCarId].currentTest4 = item.test4;
                    break;
                case 4:
                    button.SetActive(false);
                    Cars[currentCarId].currentMaxSpeedLevel = item.maxspeed;
                    break;
                case 5:
                    button.SetActive(false);
                    Cars[currentCarId].currentAccelerationLevel = item.acceleration;
                    break;
                case 6:
                    button.SetActive(false);
                    Cars[currentCarId].currentDriftLevel = item.drift;
                    break;
                case 7:
                    button.SetActive(false);
                    Cars[currentCarId].currentTest4uLevel = item.upgradetest;
                    break;
            }
            //SpawnCar(Cars[currentCarId].carObject);
        }
    }

    public void ChangeCar(bool isForward)
    {
        if (isForward && currentCarId < Cars.Count - 1)
        {
            Debug.Log("Öncesi: " + currentCarId);
            currentCarId++;
            Debug.Log("Sonrasý: "+currentCarId);
            UpdateMarket(currentCarId);
            SpawnCar();
            CheckCarIsPurchased(Cars[currentCarId].isAlreadyPurchased);
        }
        else if (!isForward && currentCarId > 0)
        {
            currentCarId--;
            UpdateMarket(currentCarId);
            SpawnCar();
            CheckCarIsPurchased(Cars[currentCarId].isAlreadyPurchased);
        }
    }

    private void CheckCarIsPurchased(bool isAlreadyPurchased)
    {
        if (isAlreadyPurchased)
        {
            Destroy(currentPurchaseCarButton);
            ModificationsAndUpgrades[0].SetActive(true);
            ModificationsAndUpgrades[1].SetActive(true);
        }
        else
        {
            Destroy(currentPurchaseCarButton);
            currentPurchaseCarButton = Instantiate(PurchaseCarButtonPrefab, ModificationsAndUpgrades[0].transform.parent);
            currentPurchaseCarButton.transform.SetAsFirstSibling();
            currentPurchaseCarButton.GetComponentInChildren<TextMeshProUGUI>().text = Cars[currentCarId].Score.ToString();
            currentPurchaseCarButton.GetComponentInChildren<Button>().onClick.AddListener(PurchaseCar);
            ModificationsAndUpgrades[0].SetActive(false);
            ModificationsAndUpgrades[1].SetActive(false);
        }
    }
    public void PurchaseCar()
    {
        if (playerProgress.totalScore >= Cars[currentCarId].Score)
        {
            playerProgress.totalScore -= Cars[currentCarId].Score;
            updateText();
            Cars[currentCarId].isAlreadyPurchased = true;
            CheckCarIsPurchased(true);
        }
    }
    private void SpawnCar()
    {
        //Destroy(currentCar);
        if (!currentCar)
        {
            currentCar = Instantiate(Cars[currentCarId].carObject);
        }
        SetSelectedCar();
        currentCar.GetComponent<CarSound>().enabled = false;
        currentCar.GetComponent<CarController>().enabled = false;
        currentCar.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        currentCar.transform.position = SpawnPoint.transform.position;
        currentCar.transform.rotation = SpawnPoint.transform.rotation;

        GameObject.Find("Body").GetComponent<MeshRenderer>().material.color = Cars[currentCarId].currentColor;
    }


    private void UpdateMarket(int currentCarId)
    {
        for (int i = 0; i < CurrentButtons.Count; i++)
        {
            Destroy(CurrentButtons[i]);
        }
        Debug.Log(Cars[currentCarId].items.Count);
        for (int i = 0; i < Cars[currentCarId].items.Count; i++)
        {
            GameObject Button = Instantiate(ButtonPrefab, Slot[Cars[currentCarId].items[i].itemTypeId].transform);
            Button.AddComponent<MarketButton>();
            Button.GetComponent<MarketButton>().SetItem(Cars[currentCarId].items[i], gameObject.GetComponent<Market>());
            Button.name = Cars[currentCarId].items[i].itemName;

            if (Cars[currentCarId].items[i].isAlreadyPurchased)
            {
                Button.GetComponentInChildren<TextMeshProUGUI>().text = Cars[currentCarId].items[i].itemName + "\n" + "Purchased";
            }
            else
            {
                Button.GetComponentInChildren<TextMeshProUGUI>().text = Cars[currentCarId].items[i].itemName + "\n" + Cars[currentCarId].items[i].itemCost.ToString() + " Score";
            }
            CurrentButtons.Add(Button);
            if (Cars[currentCarId].items[i].itemTypeId > 3)
            {
                if (Cars[currentCarId].items[i].isAlreadyPurchased)
                {
                    Destroy(Button);
                }
                Button.transform.SetAsFirstSibling();
            }
            //Button.GetComponent<Button>().onClick.AddListener(() => Purchase(Cars[currentCarId].items[i].itemTypeId,
            //                                                               Cars[currentCarId].items[i].itemFeature,
            //                                                             Cars[currentCarId].items[i].itemCost,
            //                                                           Cars[currentCarId].items[i].isAlreadyPurchased,
            //                                                         Button));
        }
    }
    public void ResetCarStats()
    {
        for (int i = 0; i < Cars[currentCarId].items.Count; i++)
        {
            Cars[currentCarId].items[i].isAlreadyPurchased = false;
        }
        Cars[currentCarId].currentAccelerationLevel = 0;
        Cars[currentCarId].currentDriftLevel = 0;
        Cars[currentCarId].currentMaxSpeedLevel = 0;
        Cars[currentCarId].currentTest4uLevel = 0;

        Cars[currentCarId].isAlreadyPurchased = false;
        UpdateMarket(currentCarId);
        SpawnCar();
        CheckCarIsPurchased(Cars[currentCarId].isAlreadyPurchased);
    }

    public void AddScore()
    {
        playerProgress.totalScore += 10000;
        updateText();
    }

    private void SetSelectedCar()
    {
        playerProgress.currentCar = Cars[currentCarId];
    }
    private void updateText()
    {
        //GoldText.text = playerProgress.gold.ToString();
        ScoreText.text = playerProgress.totalScore.ToString();
    }
}