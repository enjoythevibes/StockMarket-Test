using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace StockMarket.UI.StockMarket.MarketItem
{
    public class MarketItemEntity : MonoBehaviour
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

        public IEnumerator SetMarketItemDataAsync(MarketItemData marketItemData) // Изменить на обычный метод который возвращает корутину
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