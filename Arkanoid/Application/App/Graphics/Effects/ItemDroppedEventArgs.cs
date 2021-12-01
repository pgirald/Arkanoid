using System;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public class ItemDroppedEventArgs : EventArgs
    {
        public EffectItem Effect { get; set; }
    }
}
