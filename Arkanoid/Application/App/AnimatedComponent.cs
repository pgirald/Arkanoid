using Arkanoid.Application.Utils;
using System;

namespace Arkanoid.Application.App
{
    public abstract class AnimatedComponent : TextureComponent, IAnimatedComponent
    {
        public float Speed { get; set; }

        public void Move(float ellasedTime)
        {
            Proceed(ellasedTime * Speed);
        }

        protected abstract void Proceed(float trueSpeed);
    }
}
