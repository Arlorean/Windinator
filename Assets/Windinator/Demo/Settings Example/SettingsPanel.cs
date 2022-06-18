using UnityEngine;
using Riten.Windinator;
using Riten.Windinator.LayoutBuilder;
using Riten.Windinator.Material;

using static Riten.Windinator.LayoutBuilder.Layout;

public class SettingsPanel : LayoutBaker
{
    public override Element Bake()
    {
        return new Rectangle(
            new Vertical(
                new Element[]
                {
                    new MaterialUI.Label("General Settings", style: MaterialLabelStyle.Label),
                    new MaterialUI.LabeledSwitch(
                        "Wi-Fi", false, MaterialIcons.wifi,
                        MaterialIcons.wifi, MaterialIcons.wifi_off,
                        "Public campus Wi-Fi", true
                    ),
                    new MaterialUI.LabeledSwitch("Bluetooth", true, MaterialIcons.bluetooth, MaterialIcons.check),
                    new MaterialUI.LabeledSwitch("Airplane Mode", true, MaterialIcons.airplane),
                    new MaterialUI.LabeledSwitch("Do not disturb", false, MaterialIcons.volume_mute),

                    new MaterialUI.Separator(false),

                    new MaterialUI.SegmentedButton(new string[]
                    {
                        "Dark Mode", "Light Mode", "Invisible Mode"
                    }, startSelectedIndex: 1),

                    new MaterialUI.Label("Volume Settings", style: MaterialLabelStyle.Label),
                    new Spacer(40f)
                },
                spacing: 20f
            ),
            size: new Vector2(400f, -1f),
            padding: Vector4.one * 20f,
            shape: new ShapeProperties
            {
                Color = Colors.Surface.ToColor(),
                Roundness = Vector4.one * 40,
                Shadow = new ShadowProperties
                {
                    Blur = 30f,
                    Size = 20f
                }
            }
        );
    }

}
