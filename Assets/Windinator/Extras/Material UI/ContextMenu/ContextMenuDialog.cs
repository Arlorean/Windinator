using System;
using System.Collections.Generic;
using UnityEngine;

namespace Riten.Windinator.Material
{
    public struct ContextMenuItem
    {
        public string Value;

        public Action Action;

        public ContextMenuItem[] Children;

        public ContextMenuItem(string value, Action action)
        {
            Value = value;
            Action = action;
            Children = Array.Empty<ContextMenuItem>();
        }

        public ContextMenuItem(string value, params ContextMenuItem[] children)
        {
            Value = value;
            Action = null;
            Children = children;
        }
    }

    public class ContextMenuDialog : WindinatorBehaviour
    {
        struct RawContextMenuItem
        {
            public string Value;

            public Action Action;

            public ContextMenuItem[] Children;
        }

        [SerializeField] RectTransform m_contextMenuRoot;

        [SerializeField] GameObject m_itemPreset;

        [SerializeField] ContextMenuListPreset m_list;


        List<GameObject> m_spawnedObjects = new List<GameObject>();

        private float m_width = 200f;

        private int m_prefabOffset = 0;

        public void Setup(float width, params ContextMenuItem[] items)
        {
            m_width = width;
            m_prefabOffset = 0;

            foreach (var go in m_spawnedObjects) go.SetActive(false);

            m_contextMenuRoot.anchoredPosition = Canvas.ScreenToCanvasPosition(Input.mousePosition);

            SetupChildren(m_list, m_contextMenuRoot, items);

            for (int i = m_prefabOffset; i < m_spawnedObjects.Count; ++i)
            {
                var go = m_spawnedObjects[i];

                if (go.activeSelf)
                    go.SetActive(false);
            }
        }

        public void Setup(params ContextMenuItem[] items)
        {
            Setup(200f, items);
        }

        public void SetupChildren(ContextMenuListPreset listComponent, RectTransform list, ContextMenuItem[] items)
        {
            list.sizeDelta = new Vector2(m_width, 0);
            for (int i = 0; i < items.Length; i++)
            {
                if (m_spawnedObjects.Count <= m_prefabOffset)
                    m_spawnedObjects.Add(Instantiate(m_itemPreset, list, false));

                var go = m_spawnedObjects[m_prefabOffset++];

                if (go.transform.parent != list)
                    go.transform.SetParent(list, false);

                go.transform.SetAsLastSibling();

                var item = go.GetComponentInChildren<ContextMenuItemPreset>(true);
                item.Setup(listComponent, this, items[i]);

                if (!go.activeSelf)
                    go.SetActive(true);
            }
        }
    }
}