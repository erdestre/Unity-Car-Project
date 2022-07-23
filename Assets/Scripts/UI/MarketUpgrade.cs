using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarketUpgrade : EventTrigger
{
    [SerializeField] PlayerProgress playerprogress;
    [SerializeField] int whichCar, whichUpgrade, UpgradeLevel;
    public override void OnPointerDown(PointerEventData data)
    {
        
    }
}
