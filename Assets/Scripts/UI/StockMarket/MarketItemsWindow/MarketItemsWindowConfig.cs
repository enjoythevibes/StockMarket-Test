using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StockMarket.UI.StockMarket.MarketItemsWindow
{
    [CreateAssetMenu(fileName = "MarketItemsWindowConfig", menuName = "StockMarket-Test/UI/MarketItemsWindow/MarketItemsWindowConfig", order = 103)]
    public class MarketItemsWindowConfig : ScriptableObject
    {
        [TextArea]
        [SerializeField] private string marketItemsDataRemoteURL = "https://raw.githubusercontent.com/enjoythevibes/StockMarket-Test/main/Data.json";
        [SerializeField] private GameObject marketItemEntityPrefab = default;

        public string MarketItemsDataRemoteURL => marketItemsDataRemoteURL;
        public GameObject MarketItemEntityPrefab => marketItemEntityPrefab;
    }
}