using UnityEngine;
using UnityEngine.UI;

namespace Riten.Windinator.LayoutBuilder
{
    public abstract class LayoutBaker : MonoBehaviour
    {
        [SerializeField] bool m_fullScreen = false;

        public void ClearContents()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(transform.GetChild(i).gameObject, true);
        }

        private void UpdateFullscreen()
        {
            RectTransform me = transform as RectTransform;

            if (m_fullScreen)
            {
                GetComponent<ContentSizeFitter>().enabled = false;
                me.anchorMin = Vector3.zero;
                me.anchorMax = Vector3.one;
                me.sizeDelta = Vector2.zero;
                me.anchoredPosition = Vector2.zero;
            }
            else
            {
                GetComponent<ContentSizeFitter>().enabled = true;

                me.anchorMin = Vector3.one * 0.5f;
                me.anchorMax = Vector3.one * 0.5f;
            }
        }

        void OnValidate()
        {
            UpdateFullscreen();
        }

        public RectTransform Build()
        {
            RectTransform me = transform as RectTransform;

            UpdateFullscreen();

            return new Builder(transform as RectTransform, child: Bake()).Build();
        }

        public abstract Layout.Element Bake();
    }
}