using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace StockMarket.UI.StockMarket.MarketItem
{
    [CreateAssetMenu(fileName = "UserAvatarsCache", menuName = "StockMarket-Test/UI/StockMarket/UserAvatarsCache", order = 101)]
    public class UserAvatarsCache : ScriptableObject
    {
        private Dictionary<string, Sprite> spriteByImageURL = new Dictionary<string, Sprite>();
        private HashSet<string> loadingSpriteByImageURL = new HashSet<string>();

        public Sprite this[string imageURL] => spriteByImageURL[imageURL];

        public bool Contains(string imageURL)
        {
            return spriteByImageURL.ContainsKey(imageURL);
        }

        public void Remove(string imageURL)
        {
            spriteByImageURL.Remove(imageURL);
        }

        public IEnumerator LoadImageAsync(string imageURL)
        {
            if (Contains(imageURL)) yield break;
            if (loadingSpriteByImageURL.Contains(imageURL))
            {
                yield return new WaitWhile(() => loadingSpriteByImageURL.Contains(imageURL));
                yield break;
            }
            loadingSpriteByImageURL.Add(imageURL);
            var webRequestUserAvatar = UnityWebRequestTexture.GetTexture(imageURL);
            yield return webRequestUserAvatar.SendWebRequest();
            if (webRequestUserAvatar.isNetworkError || webRequestUserAvatar.isHttpError)
            {
                Debug.LogError("Load User Avatar Error: " + webRequestUserAvatar.error);
            }
            else
            {
                var avatarTexture = (webRequestUserAvatar.downloadHandler as DownloadHandlerTexture).texture;
                var userAvatar = Sprite.Create(avatarTexture, new Rect(0f, 0f, avatarTexture.width, avatarTexture.height), new Vector2(0.5f, 0.5f), 100f);
                spriteByImageURL.Add(imageURL, userAvatar);
                loadingSpriteByImageURL.Remove(imageURL);
            }
        }
    }
}