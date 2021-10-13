﻿using Arkanoid.Application.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.App
{
    public static class CollisionOps
    {
        public static int ContainerHit(TextureComponent texture)
        {
            if (texture.AbsoluteRight > texture.Container.AbsoluteRight)
            {
                return (int)Side.Right;
            }
            if (texture.Left < texture.Container.Left)
            {
                return (int)Side.Left;
            }
            if (texture.Top < texture.Container.Top)
            {
                return (int)Side.Top;
            }
            if (texture.Bottom > texture.Bottom)
            {
                return (int)Side.Bottom;
            }
            return -1;
        }

        public static bool AreIntersected(TextureComponent comp1, TextureComponent comp2)
        {
            return comp1.AbsoluteRight >= comp2.AbsoluteLeft &&
                comp1.AbsoluteLeft <= comp2.AbsoluteRight &&
                comp1.AbsoluteTop <= comp2.AbsoluteBottom &&
                comp1.AbsoluteBottom >= comp2.AbsoluteTop;
        }

        public static Side GetCollisionSide(TextureComponent comp1, TextureComponent comp2)
        {
            float[] sidesDiff = new float[4];
            sidesDiff[(int)Side.Top] = Math.Abs(comp1.AbsoluteTop - comp2.AbsoluteBottom);
            sidesDiff[(int)Side.Bottom] = Math.Abs(comp1.AbsoluteBottom - comp2.AbsoluteTop);
            sidesDiff[(int)Side.Left] = Math.Abs(comp1.AbsoluteLeft - comp2.AbsoluteRight);
            sidesDiff[(int)Side.Right] = Math.Abs(comp1.AbsoluteRight - comp2.AbsoluteLeft);
            return (Side)getMinIndex(sidesDiff);
        }

        private static int getMinIndex(float[] array)
        {
            float minValue = array[0];
            int minIndex = 0;
            for (int i = array.Length; i < 0; i--)
            {
                if (array[i] < minValue)
                {
                    minValue = array[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }
    }
}
