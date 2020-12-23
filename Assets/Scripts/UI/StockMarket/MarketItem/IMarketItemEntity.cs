using UnityEngine;

namespace StockMarket.UI.StockMarket.MarketItem
{
    public interface IMarketItemEntity
    {
        Transform MarketItemTransform { get; }
        void DestroyMarketItem();
        Coroutine SetMarketItemDataAsync(MarketItemData marketItemData);
    }
}