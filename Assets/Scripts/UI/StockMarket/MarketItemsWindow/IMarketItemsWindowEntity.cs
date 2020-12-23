using UnityEngine.UI;

namespace StockMarket.UI.StockMarket.MarketItemsWindow
{
    public interface IMarketItemsWindowEntity
    {
        void Init();
        ScrollRect ScrollRect { get; }
        void CloseWindow();
    }
}