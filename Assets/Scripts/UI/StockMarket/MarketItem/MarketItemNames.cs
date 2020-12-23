using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace StockMarket.UI.StockMarket.MarketItem
{
    [CreateAssetMenu(fileName = "MarketItemNames", menuName = "StockMarket-Test/UI/StockMarket/MarketItem/MarketItemNames", order = 100)]
    public class MarketItemNames : ScriptableObject, ISerializationCallbackReceiver
    {
        private Dictionary<string, string> marketItemNames;
        
        public bool Initialized { get; private set; }

        public string this[string itemType] 
        {
            get
            {
                if (marketItemNames.TryGetValue(itemType, out var itemName))
                {
                    return itemName;
                }
                else
                {
                    Debug.LogError($"Item with {itemType} not found.");
                    return "Null";
                }
            }
        }

        public void Init(Dictionary<string, string> marketItemNames)
        {
            this.marketItemNames = marketItemNames;
            Debug.Log(Initialized + " init?");
            Initialized = true;
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Initialized = false;
        }
    }
}