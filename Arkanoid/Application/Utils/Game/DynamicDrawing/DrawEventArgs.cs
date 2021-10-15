using System;

namespace Arkanoid.Application.Utils.Game.DynamicDrawing
{
    public class DrawEventArgs : EventArgs
    {
        public IScenarioDraw Draw { get; set; }
    }
}
