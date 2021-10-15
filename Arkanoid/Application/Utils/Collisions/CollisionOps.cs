using Arkanoid.Application.Utils.Components;
using System;

namespace Arkanoid.Application.Utils.Collisions
{
    public static class CollisionOps
    {
        public static int ContainerHit(Component texture)
        {
            if (texture.AbsoluteRight > texture.Container.AbsoluteRight)
            {
                return (int)Side.Right;
            }
            if (texture.AbsoluteLeft < texture.Container.AbsoluteLeft)
            {
                return (int)Side.Left;
            }
            if (texture.AbsoluteTop < texture.Container.AbsoluteTop)
            {
                return (int)Side.Top;
            }
            if (texture.AbsoluteBottom > texture.Container.AbsoluteBottom)
            {
                return (int)Side.Bottom;
            }
            return -1;
        }

        public static bool AreIntersected(Component comp1, Component comp2)
        {
            return comp1.AbsoluteRight >= comp2.AbsoluteLeft &&
                comp1.AbsoluteLeft <= comp2.AbsoluteRight &&
                comp1.AbsoluteTop <= comp2.AbsoluteBottom &&
                comp1.AbsoluteBottom >= comp2.AbsoluteTop;
        }

        public static CollisionInfo GetCollisionInfo(Component comp1, Component comp2)
        {
            float[] sidesDiff = new float[4];
            float[] sidesPos = new float[4];

            sidesPos[(int)Side.Top] = comp2.AbsoluteTop;
            sidesPos[(int)Side.Bottom] = comp2.AbsoluteBottom;
            sidesPos[(int)Side.Left] = comp2.AbsoluteLeft;
            sidesPos[(int)Side.Right] = comp2.AbsoluteRight;

            sidesDiff[(int)Side.Top] = Math.Abs(comp2.AbsoluteTop - comp1.AbsoluteBottom);
            sidesDiff[(int)Side.Bottom] = Math.Abs(comp2.AbsoluteBottom - comp1.AbsoluteTop);
            sidesDiff[(int)Side.Left] = Math.Abs(comp2.AbsoluteLeft - comp1.AbsoluteRight);
            sidesDiff[(int)Side.Right] = Math.Abs(comp2.AbsoluteRight - comp1.AbsoluteLeft);

            Side collisedSide = (Side)getMinIndex(sidesDiff);
            return new CollisionInfo { Side = collisedSide, CollisionSidePosition = sidesPos[(int)collisedSide] };
        }

        private static int getMinIndex(float[] array)
        {
            float minValue = array[0];
            int minIndex = 0;
            for (int i = 1; i < array.Length; i++)
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
