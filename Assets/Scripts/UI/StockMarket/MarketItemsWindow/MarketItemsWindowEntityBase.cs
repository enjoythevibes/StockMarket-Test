using UnityEngine;
using UnityEngine.UI;

namespace StockMarket.UI.StockMarket.MarketItemsWindow
{
    public abstract class MarketItemsWindowEntityBase : MonoBehaviour, IMarketItemsWindowEntity
    {
        public abstract ScrollRect ScrollRect { get; }
        public abstract void OpenWindow();
        public abstract void CloseWindow();
    }
}