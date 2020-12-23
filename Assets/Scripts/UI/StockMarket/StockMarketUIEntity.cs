using System.Collections;
using System.Collections.Generic;
using StockMarket.UI.StockMarket.MarketItemsWindow;
using UnityEngine;

namespace StockMarket.UI.StockMarket
{
    public class StockMarketUIEntity : MonoBehaviour
    {
        [SerializeField] private MarketItemsWindowEntityBase marketItemsWindowEntity;

        private void Awake()
        {
            marketItemsWindowEntity.Init();    
        }

        public void CloseStockMarketUI()
        {
            marketItemsWindowEntity.CloseWindow();
            gameObject.SetActive(false);
        }
    }
}