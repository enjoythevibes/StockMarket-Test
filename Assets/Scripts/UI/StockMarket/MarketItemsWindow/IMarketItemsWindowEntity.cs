using UnityEngine.UI;

namespace StockMarket.UI.StockMarket.MarketItemsWindow
{
    public interface IMarketItemsWindowEntity
    {
        ScrollRect ScrollRect { get; }
        void OpenWindow();
        void CloseWindow();
    }
}