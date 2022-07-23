using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketButton : MonoBehaviour
{
    Items item;
    Market market;

    public void SetItem(Items item, Market market)
    {
        this.item = item;
        this.market = market;
        gameObject.GetComponent<Button>().onClick.AddListener(GetItem);
    }
    public void GetItem()
    {
        market.Purchase(item, gameObject);
    }
}
