using System.Collections.Generic;
using UnityEngine;

namespace Riten.Windinator.Shapes
{
    [System.Serializable]
    public class CircleDrawer : ShapeDrawer
    {
        List<StaticArray<Vector4>> m_batchedData;

        public CircleDrawer(CanvasGraphic canvas) : base(canvas)
        {
            m_batchedData = new List<StaticArray<Vector4>>();
        }

        public override string MaterialName => "UI/Windinator/DrawCircle";

        protected override void DrawBatches()
        {
            if (m_batchedData.Count == 0) return;

            foreach(var batch in m_batchedData)
            {
                Material.SetVectorArray("_Points", batch.Array);
                Material.SetInt("_PointsCount", batch.Length);

                Dispatch();

                batch.Length = 0;
                ArrayPool.Free(batch);
            }

            m_batchedData.Clear();
        }

        public void Draw(Vector2 center, float radius, float blend = 0f, DrawOperation operation = DrawOperation.Union)
        {
            m_tmp.x = center.x;
            m_tmp.y = center.y;
            m_tmp.z = radius;
            m_tmp.w = blend;

            var array = ArrayPool.Allocate();
            array.Add(m_tmp);

            SetupMaterial(blend, operation);

            Material.SetVectorArray("_Points", array.Array);
            Material.SetInt("_PointsCount", array.Length);

            array.Length = 0;
            ArrayPool.Free(array);

            Dispatch();
        }

        Vector4 m_tmp;

        public void AddBatch(Vector2 center, float radius, float blend = 0f)
        {
            #if UNITY_EDITOR
            if (m_batchedData == null)
                m_batchedData = new List<StaticArray<Vector4>>();
            #endif

            if (m_batchedData.Count == 0 || m_batchedData[^1].Length >= m_batchedData.Capacity)
                m_batchedData.Add(ArrayPool.Allocate());

            m_tmp.x = center.x;
            m_tmp.y = center.y;
            m_tmp.z = radius;
            m_tmp.w = blend;

            m_batchedData[^1].Add(m_tmp);
        }

        public void DrawBatch(DrawOperation operation = DrawOperation.Union)
        {
            DrawBatchInternal(0, operation);
        }
    }
}