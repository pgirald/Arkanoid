using Microsoft.Xna.Framework;
using System;

namespace Arkanoid.Application.Utils.Components
{
    public static class ComponentOps
    {
        public static void Align(this Component _comp, Alignment alignment)
        {
            _comp.Align(_comp.Container, alignment);
        }

        public static void Align(this Component _comp, Component comp, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TopLeft:
                    _comp.AbsoluteTop = comp.AbsoluteTop;
                    _comp.AbsoluteLeft = comp.AbsoluteLeft;
                    return;
                case Alignment.TopCenter:
                    _comp.AbsoluteTop = comp.AbsoluteTop;
                    _comp.AbsoluteHorizontalCenter = comp.AbsoluteHorizontalCenter;
                    return;
                case Alignment.TopRight:
                    _comp.AbsoluteTop = comp.AbsoluteTop;
                    _comp.AbsoluteRight = comp.AbsoluteRight;
                    return;
                case Alignment.MiddleLeft:
                    _comp.AbsoluteVerticalCenter = comp.AbsoluteVerticalCenter;
                    _comp.AbsoluteLeft = comp.AbsoluteLeft;
                    return;
                case Alignment.MiddleCenter:
                    _comp.AbsoluteVerticalCenter = comp.AbsoluteVerticalCenter;
                    _comp.AbsoluteHorizontalCenter = comp.AbsoluteHorizontalCenter;
                    return;
                case Alignment.MiddleRight:
                    _comp.AbsoluteVerticalCenter = comp.AbsoluteVerticalCenter;
                    _comp.AbsoluteRight = comp.AbsoluteRight;
                    return;
                case Alignment.BottomLeft:
                    _comp.AbsoluteBottom = comp.AbsoluteBottom;
                    _comp.AbsoluteLeft = comp.AbsoluteLeft;
                    return;
                case Alignment.BottomCenter:
                    _comp.AbsoluteBottom = comp.AbsoluteBottom;
                    _comp.AbsoluteHorizontalCenter = comp.AbsoluteHorizontalCenter;
                    return;
                case Alignment.BottomRight:
                    _comp.AbsoluteBottom = comp.AbsoluteBottom;
                    _comp.AbsoluteRight = comp.AbsoluteRight;
                    return;
            }
        }

        public static void PutOn(this Component _comp, Component comp, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TopLeft:
                    _comp.AbsoluteLeft = comp.AbsoluteLeft;
                    _comp.AbsoluteBottom = comp.AbsoluteTop;
                    return;
                case Alignment.TopCenter:
                    _comp.AbsoluteHorizontalCenter = comp.AbsoluteHorizontalCenter;
                    _comp.AbsoluteBottom = comp.AbsoluteTop;
                    return;
                case Alignment.TopRight:
                    _comp.AbsoluteRight = comp.AbsoluteRight;
                    _comp.AbsoluteBottom = comp.AbsoluteTop;
                    return;
                case Alignment.MiddleLeft:
                    _comp.AbsoluteRight = comp.AbsoluteLeft;
                    _comp.AbsoluteVerticalCenter = comp.AbsoluteVerticalCenter;
                    return;
                case Alignment.MiddleCenter:
                    _comp.AbsoluteHorizontalCenter = comp.AbsoluteHorizontalCenter;
                    _comp.AbsoluteVerticalCenter = comp.AbsoluteVerticalCenter;
                    return;
                case Alignment.MiddleRight:
                    _comp.AbsoluteLeft = comp.AbsoluteRight;
                    _comp.AbsoluteVerticalCenter = comp.AbsoluteVerticalCenter;
                    return;
                case Alignment.BottomLeft:
                    _comp.AbsoluteLeft = comp.AbsoluteLeft;
                    _comp.AbsoluteTop = comp.AbsoluteBottom;
                    return;
                case Alignment.BottomCenter:
                    _comp.AbsoluteHorizontalCenter = comp.AbsoluteHorizontalCenter;
                    _comp.AbsoluteTop = comp.AbsoluteBottom;
                    return;
                case Alignment.BottomRight:
                    _comp.AbsoluteRight = comp.AbsoluteRight;
                    _comp.AbsoluteTop = comp.AbsoluteBottom;
                    return;
            }
        }

        public static void ComputeLocation(Alignment mode, float width, float height, float zeroX, float zeroY, ref Vector2 vector)
        {
            switch (mode)
            {
                case Alignment.TopLeft:
                    vector.X = zeroX;
                    vector.Y = zeroY;
                    return;
                case Alignment.TopCenter:
                    vector.X = zeroX + width / 2;
                    vector.Y = zeroY;
                    return;
                case Alignment.TopRight:
                    vector.X = zeroX + width;
                    vector.Y = zeroY;
                    return;
                case Alignment.MiddleLeft:
                    vector.X = zeroX;
                    vector.Y = zeroY + height / 2;
                    return;
                case Alignment.MiddleCenter:
                    vector.X = zeroX + width / 2;
                    vector.Y = zeroY + height / 2;
                    return;
                case Alignment.MiddleRight:
                    vector.X = zeroX + width;
                    vector.Y = zeroY + height / 2;
                    return;
                case Alignment.BottomLeft:
                    vector.X = zeroX;
                    vector.Y = zeroY + height;
                    return;
                case Alignment.BottomCenter:
                    vector.X = zeroX + width / 2;
                    vector.Y = zeroY + height;
                    return;
                case Alignment.BottomRight:
                    vector.X = zeroX + width;
                    vector.Y = zeroY + height;
                    return;
            }

            throw new Exception("Non-valid argument given to computeOrigin");
        }
    }
}
