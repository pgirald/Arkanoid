using Arkanoid.Application.Utils;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid.Application.App.Components.Textures.Paddles
{
    public class Paddle : CollideableComponent, IAnimatedComponent
    {
        public Paddle()
        {
            Speed = 15f;
        }

        protected override void Proceed(float trueSpeed)
        {
            KeyboardState state = AppControls.State;
            if (state.IsKeyDown(AppControls.Right))
            {
                X += trueSpeed;
            }
            if (state.IsKeyDown(AppControls.Left))
            {
                X -= trueSpeed;
            }
        }

        public override string ParentPath => "Paddles/";

        public override string TexturePath => "Paddle";

        public override void OnContainerHit(Side side)
        {
            switch (side)
            {
                case Side.Left:
                    Left = Container.Left;
                    break;
                case Side.Right:
                    Right = Container.Right;
                    break;
            }
        }
    }
}
