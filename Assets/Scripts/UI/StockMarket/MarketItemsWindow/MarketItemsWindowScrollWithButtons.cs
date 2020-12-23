using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StockMarket.UI.StockMarket.MarketItemsWindow
{
    public class MarketItemsWindowScrollWithButtons : MonoBehaviour
    {
        private IMarketItemsWindowEntity marketItemsWindowEntity;
        [SerializeField] private float scrollSpeed = 500f;
        private bool scrollDown;
        private bool scrollUp;

        private void Awake()
        {
            marketItemsWindowEntity = GetComponent<IMarketItemsWindowEntity>();    
        }

        public void ScrollDown(bool press)
        {
            scrollDown = press;
        }

        public void ScrollUp(bool press)
        {
            scrollUp = press;
        }

        private void Update()
        {
            var scrollRect = marketItemsWindowEntity.ScrollRect;
            if (scrollDown)
            {
                scrollRect.verticalNormalizedPosition -= Time.deltaTime * (1f / scrollRect.content.rect.height) * scrollSpeed;
            }
            else
            if (scrollUp)
            {
                scrollRect.verticalNormalizedPosition += Time.deltaTime * (1f / scrollRect.content.rect.height) * scrollSpeed;
            }
        }
    }
}