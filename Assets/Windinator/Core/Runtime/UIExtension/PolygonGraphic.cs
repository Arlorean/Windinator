using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StaticArray<T>
{
    public T[] Array;

    public int Length;

    public StaticArray(int count)
    {
        Array = new T[count];
        Length = 0;
    }

    public T this[int i]
    {
        get { return Array[i]; }
        set { Array[i] = value; }
    }

    public void SetLength(int len)
    {
        Length = len;
    }

    public void Add(T data)
    {
        Array[Length++] = data;
    }

    public void Clear()
    {
        Length = 0;
    }
}

public class PolygonGraphic : SignedDistanceFieldGraphic
{
    [SerializeField] float m_roudness;
    [SerializeField] StaticArray<Vector4> points = new StaticArray<Vector4>(100);

    Material m_poly_material;
    
    public override Material defaultMaterial
    {
        get
        {
            if (m_poly_material == null)
                m_poly_material = new Material(Shader.Find("UI/Windinator/PolygonRenderer"));
            return m_poly_material;
        }
    } 

    public void SetRoundness(float roundness)
    {
        m_roudness = roundness;
        SetMaterialDirty();
    }

    override protected void OnEnable()
    {
        onMaterialUpdate += UpdateShaderRoundness;
        base.OnEnable();
    }

    override protected void OnDisable()
    {
        onMaterialUpdate -= UpdateShaderRoundness;
        base.OnDisable();
    }

    void UpdateShaderRoundness(float width, float height)
    {
        defaultMaterial.SetVector("_Roundness", new Vector4(m_roudness, 0, 0, 0));

        defaultMaterial.SetVectorArray("_Points", points.Array);
        defaultMaterial.SetInt("_PointsCount", points.Length);
    }
}