using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

namespace StockMarket.UI.StockMarket.MarketItem
{
    public class MarketItemEntity : MonoBehaviour, IMarketItemEntity
    {
        [SerializeField] private MarketItemNames marketItemNames = default;
        [SerializeField] private UserAvatarsCache userAvatarsCache = default;
        [SerializeField] private TextMeshProUGUI itemLabelNameField = default;
        [SerializeField] private TextMeshProUGUI usernameField = default;
        [SerializeField] private TextMeshProUGUI itemPriceField = default;
        [SerializeField] private TextMeshProUGUI itemCountField = default;
        [SerializeField] private Image userAvatarImage = default;
        [SerializeField] private Image itemImage = default;
        private string userAvatarImageURL;

        public Transform MarketItemTransform => transform;

        public Coroutine SetMarketItemDataAsync(MarketItemData marketItemData)
        {
            return StartCoroutine(LoadAndSetItemDataAsync(marketItemData));
        }

        private IEnumerator LoadAndSetItemDataAsync(MarketItemData marketItemData)
        {
            var loadItemImageAsync = Addressables.LoadAssetAsync<Sprite>(marketItemData.ItemType);
            userAvatarImageURL = marketItemData.UserAvatarURL;
            if (userAvatarsCache.Contains(userAvatarImageURL) == false)
            {
                var loadAvatarAsync = StartCoroutine(userAvatarsCache.LoadImageAsync(userAvatarImageURL));                
                yield return loadAvatarAsync;
            }
            yield return loadItemImageAsync;
            yield return new WaitUntil(() => marketItemNames.Initialized);

            itemLabelNameField.SetText(marketItemNames[marketItemData.ItemType]);
            usernameField.SetText(marketItemData.Username);
            itemPriceField.SetText(marketItemData.ItemPrice.ToString());
            itemCountField.SetText("x{0}", marketItemData.ItemCount);
            itemImage.sprite = loadItemImageAsync.Result;
            userAvatarImage.sprite = userAvatarsCache[userAvatarImageURL];
            userAvatarImage.type = Image.Type.Simple;
            userAvatarImage.preserveAspect = true;
        }

        public void DestroyMarketItem()
        {
            Addressables.Release<Sprite>(itemImage.sprite);
            if (userAvatarsCache.Contains(userAvatarImageURL))
            {
                userAvatarsCache.Remove(userAvatarImageURL);
            }
            Destroy(gameObject);
        }
    }
}