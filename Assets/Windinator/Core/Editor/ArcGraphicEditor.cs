using UnityEditor;

[CustomEditor(typeof(ArcGraphic), true)]

public class ArcGraphicEditor : Editor {
    public override void OnInspectorGUI() {
        SDFGraphicEditor.DrawSDFGUI(target as SignedDistanceFieldGraphic);

        SDFGraphicEditor.BeginGroup("Arc Settings");

        var arc = target as ArcGraphic;
        arc.Angle = arc.Slider(nameof(ArcGraphic.Angle), arc.Angle, -180, 180);
        arc.Aperture = arc.Slider(nameof(ArcGraphic.Aperture), arc.Aperture, 0, 360);
        arc.Thickness = arc.FloatField(nameof(ArcGraphic.Thickness), arc.Thickness);

        SDFGraphicEditor.EndGroup();
    }

    public void OnSceneGUI() {
        SDFGraphicEditor.DrawSDFScene(target as SignedDistanceFieldGraphic);
    }
}
