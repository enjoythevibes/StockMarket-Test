using UnityEngine;
using UnityEngine.UI;

namespace StockMarket.UI.StockMarket.MarketItemsWindow
{
    public abstract class MarketItemsWindowEntityBase : MonoBehaviour, IMarketItemsWindowEntity
    {
        public abstract ScrollRect ScrollRect { get; }
        public abstract void Init();
        public abstract void CloseWindow();
    }
}