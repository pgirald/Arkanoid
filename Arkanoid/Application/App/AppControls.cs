using Microsoft.Xna.Framework.Input;

namespace Arkanoid.Application.App
{
    public static class AppControls
    {
        public static KeyboardState State => Keyboard.GetState();

        public static Keys Left => Keys.Left;

        public static Keys Right => Keys.Right;

        public static Keys Action => Keys.Space;

        public static Keys Exit => Keys.Back;
    }
}
