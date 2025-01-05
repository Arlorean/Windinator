using UnityEngine;

[ExecuteAlways]
public class ArcGraphic : SignedDistanceFieldGraphic
{
    [Header("Shape")]
    [SerializeField, Range(-180,180)] float m_angle = 0;
    [SerializeField, Range(0, 360)] float m_aperture = 90;
    [SerializeField, Min(0)] float m_thickness = 1;

    public float Angle {
        get => m_angle; set {
            m_angle = value;
            SetMaterialDirty();
        }
    }

    public float Aperture {
        get => m_aperture; set {
            m_aperture = value;
            SetMaterialDirty();
        }
    }

    public float Thickness {
        get => m_thickness; set {
            m_thickness = value;
            SetVerticesDirty();
        }
    }


    Material m_arc_material;

    public override Material defaultMaterial {
        get {
            if (m_arc_material == null)
                m_arc_material = new Material(Shader.Find("UI/Windinator/ArcRenderer"));
            return m_arc_material;
        }
    }

    public override float ExtraMargin => m_thickness;

    public override void SetMaterialDirty() {
        base.SetMaterialDirty();

        // The sdArc function expects the value to be half the aperture
        var aperture = (m_aperture/2) * Mathf.Deg2Rad;
        defaultMaterial.SetVector("_ApertureSinCos", new (Mathf.Sin(aperture), Mathf.Cos(aperture)));
        defaultMaterial.SetFloat("_ArcAngle", m_angle*Mathf.Deg2Rad);
        defaultMaterial.SetFloat("_LineThickness", m_thickness);

        // float2x2 rotationMatrix = float2x2(cosX, -sinX, sinX, cosX);
        var angle = (m_angle * Mathf.Deg2Rad);
        var rotation = Matrix4x4.identity;
        rotation.m00 = Mathf.Cos(angle);
        rotation.m01 = -Mathf.Sin(angle);
        rotation.m10 = Mathf.Sin(angle);
        rotation.m11 = Mathf.Cos(angle);
        defaultMaterial.SetMatrix("_ArcRotation", rotation);
    }
}
