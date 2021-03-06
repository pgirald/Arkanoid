using Microsoft.Xna.Framework.Input;

namespace Arkanoid.Application.Utils.Game
{
    public struct GameInfo
    {
        public double ElapsedFrameTime { get; set; }

        public float ComputedSpeed { get; set; }

        public KeyboardState KeyboardState { get; set; }
    }
}
