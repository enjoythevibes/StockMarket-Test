using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StockMarket.UI.StockMarket.MarketItem;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace StockMarket.UI.StockMarket.MarketItemNamesLoader
{
    public class MarketItemNamesLoaderEntity : MonoBehaviour
    {
        [System.Serializable]
        private struct MarketItemNamesData
        {
            public string[] MarketItemNames;
        }
        [SerializeField] private MarketItemNames marketItemNames = default;
        [SerializeField] private AssetReference marketItemNamesData = default;

        private void Awake()
        {
            marketItemNamesData.LoadAssetAsync<TextAsset>().Completed +=
            (obj) =>
            {
                var marketItemNamesData = JsonUtility.FromJson<MarketItemNamesData>(obj.Result.text);
                var marketItemNamesDictionary = new Dictionary<string, string>();
                foreach (var line in marketItemNamesData.MarketItemNames)
                {
                    var data = line.Split('=').Select(x => x.Trim()).ToArray();
                    var itemType = data[0];
                    var itemName = data[1];
                    marketItemNamesDictionary.Add(itemType, itemName);
                }                
                marketItemNames.Init(marketItemNamesDictionary);
                Addressables.Release(obj);
                Destroy(gameObject);                
            };
        }
    }
}