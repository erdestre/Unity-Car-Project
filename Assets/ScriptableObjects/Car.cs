using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]
public class Car : ScriptableObject
{
    public List<Items> items;
    [Header("Car Attributes")]
    public new string Name;
    public new string Id;
    public GameObject carObject; //comes with basic settings

    public int Score;
    public bool isAlreadyPurchased;

    [Header("CurrentModifications")] // each has 4 item
    public Color currentColor;
    public int currentTest2;
    public int currentTest3;
    public int currentTest4;
    [Header("CurrentUpgrades")] // each has 4 level (first levels are default)
    public int currentMaxSpeedLevel;
    public int currentDriftLevel;
    public int currentAccelerationLevel;
    public int currentTest4uLevel;
}


[System.Serializable]
public class Items
{
    [Header("0 = Color\t4 = Max Speed\n1 = Test 2\t5 = Acceleration \n2 = Test 3\t6 = Drift \n3 = Test 4\t7 = Test")]
    [Range(0,7)] public int itemTypeId;
    [TextArea] public string itemName;
    public int itemCost;
    public bool isAlreadyPurchased;

    [Header("Select just 1 according to itemtypeid")]
    public Color color;
    public int test2;
    public int test3;
    public int test4;

    public int maxspeed;
    public int acceleration;
    public int drift;
    public int upgradetest;

    public Items(string itemName, int itemCost, string itemFeature)
    {
        this.itemName = itemName;
        this.itemCost = itemCost;
        //this.itemFeature = itemFeature;
    }
}
