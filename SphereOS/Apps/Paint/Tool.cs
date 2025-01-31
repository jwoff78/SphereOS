﻿using Cosmos.HAL.Drivers.PCI.Video;
using Cosmos.System;
using Cosmos.System.Graphics;

namespace SphereOS.Apps.Paint
{
    internal abstract class Tool
    {
        internal Tool(string name, Bitmap icon)
        {
            Name = name;
            Icon = icon;
        }

        internal string Name { get; private set; }

        internal Bitmap Icon { get; private set; }

        internal int ButtonX { get; set; }
        internal int ButtonY { get; set; }
        internal int ButtonWidth { get; set; }
        internal int ButtonHeight { get; set; }

        internal abstract void Run(MouseState mouseState,
            int mouseX,
            int mouseY,
            Document doc,
            Paint paint);

        internal virtual void RenderOverlay(Paint paint, VMWareSVGAII driver)
        {
        }

        internal virtual void Selected()
        {
        }

        internal virtual void Deselected()
        {
        }
    }
}
