using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StockMarket.UI.StockMarket.MarketItem;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace StockMarket.UI.StockMarket.MarketItemsWindow
{
    public class MarketItemsWindowEntity : MarketItemsWindowEntityBase
    {
        [SerializeField] private MarketItemsWindowConfig marketItemsWindowConfig;
        [SerializeField] private Transform marketItemsWindowContent = default;
        [SerializeField] private GameObject loadingField = default;
        private ScrollRect scrollRect;
        private MarketItemsData marketItemsData;
        private List<IMarketItemEntity> marketItemEntities = new List<IMarketItemEntity>();

        public override ScrollRect ScrollRect => scrollRect;
        
        public override void OpenWindow()
        {
            StartCoroutine(LoadJsonDataFromServer(marketItemsWindowConfig.MarketItemsDataRemoteURL, 
            (data) => 
            {
                marketItemsData = JsonUtility.FromJson<MarketItemsData>(data);
                StartCoroutine(SpawnMarketItems());
            }));
            scrollRect = GetComponent<ScrollRect>();
        }

        private IEnumerator SpawnMarketItems()
        {
            var count = marketItemsData.MarketItems.Length;
            if (count > marketItemsWindowConfig.RowsPreloaded * 2) count = marketItemsWindowConfig.RowsPreloaded * 2;
            var coroutines = new Coroutine[count];
            for (int i = 0; i < count; i++)
            {
                coroutines[i] = StartCoroutine(SpawnMarketItemAsync(i));
            }
            for (int i = 0; i < count; i++)
            {
                yield return coroutines[i];
            }
            for (int i = 0; i < count; i++)
            {
                marketItemEntities[i].MarketItemTransform.localScale = Vector2.one;
            }
            loadingField.SetActive(false);
            yield return null;
            for (int i = count - 1; i < marketItemsData.MarketItems.Length; i++)
            {
                StartCoroutine(SpawnMarketItemAsync(i, false));
            }
        }

        private IEnumerator SpawnMarketItemAsync(int index, bool async = true)
        {
            var marketItemEntity = Instantiate(marketItemsWindowConfig.MarketItemEntityPrefab, Vector2.zero, Quaternion.identity, marketItemsWindowContent).GetComponent<MarketItemEntity>();
            marketItemEntities.Add(marketItemEntity);
            if (async)
                marketItemEntity.transform.localScale = Vector3.zero;
            yield return marketItemEntity.SetMarketItemDataAsync(marketItemsData.MarketItems[index]);
        }
        
        private IEnumerator LoadJsonDataFromServer(string url, Action<string> executeAfterLoad)
        {
            var webRequest = UnityWebRequest.Get(url);
            webRequest.useHttpContinue = false;
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Load Json Data Error: " + webRequest.error);
            }
            else
            {
                var data = webRequest.downloadHandler.text;
                Debug.Log("Json data: " + data);
                executeAfterLoad.Invoke(data);
            }
            webRequest.Dispose();
        }

        public override void CloseWindow()
        {
            for (int i = marketItemEntities.Count - 1; i >= 0 ; i--)
            {
                marketItemEntities[i].DestroyMarketItem();
            }
            marketItemEntities.Clear();
            marketItemsData = null;
        }
    }
}