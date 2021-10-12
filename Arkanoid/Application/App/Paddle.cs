using Arkanoid.Application.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.App
{
    public class Paddle : AnimatedTexture
    {
        public override string ParentPath => "Paddles/";

        public override string TexturePath => "Paddle";
    }
}
