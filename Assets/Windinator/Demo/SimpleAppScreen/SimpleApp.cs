using UnityEngine;
using Riten.Windinator;
using Riten.Windinator.LayoutBuilder;
using Riten.Windinator.Material;

using static Riten.Windinator.LayoutBuilder.Layout;

public class SimpleApp : LayoutBaker
{
    public LayoutBaker m_bottomBar;

    public override Element Bake()
    {
        var bottomBar = m_bottomBar.Bake();

        return new Expand(
            new Vertical(
            new Element[] {
                bottomBar,
                new FlexibleSpace(),
                bottomBar
            }
        ));
    }
}
