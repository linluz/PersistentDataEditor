﻿using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PersistentDataEditor
{
    public abstract class BaseControlItem
    {
        internal static bool ShouldRespond = true;
        private RectangleF _bounds;
        internal RectangleF Bounds
        {
            get => _bounds;
            set
            {
                if (Valid)
                {
                    _bounds = value;
                    LayoutObject(value);
                }
                else
                {
                    _bounds = RectangleF.Empty;
                }
            }
        }
        protected virtual bool Valid => true;
        internal abstract int Width { get; }
        internal abstract int Height { get; }
        protected virtual void LayoutObject(RectangleF bounds) { }
        internal virtual void ChangeControlItems() { }
        internal abstract void RenderObject(GH_Canvas canvas, Graphics graphics, GH_PaletteStyle style);
        internal abstract void Clicked(GH_Canvas sender, GH_CanvasMouseEvent e);
        protected static GraphicsPath RoundedRect(RectangleF bounds, float radius)
        {
            float diameter = radius * 2;
            SizeF size = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
