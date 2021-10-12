using System;

namespace Arkanoid.Application.Utils
{
    public class ParentContainer : Container
    {
        public override Container Container
        {
            get => null;
            set => throw new Exception("A parent container never has a container ");
        }

        public override float X
        {
            get => AbsoluteX;
            set => base.X = value;
        }

        public override float Y
        {
            get => AbsoluteY;
            set => base.X = value;
        }
    }
}
