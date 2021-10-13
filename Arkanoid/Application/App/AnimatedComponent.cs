using Arkanoid.Application.Utils;
using System;

namespace Arkanoid.Application.App
{
    public abstract class AnimatedComponent : TextureComponent, IAnimatedComponent
    {
        public float Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Move(float ellasedTime)
        {
            Move(ellasedTime * Speed);
        }

        protected abstract void Proceed(float trueSpeed);
    }
}
