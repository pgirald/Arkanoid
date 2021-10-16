using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public class ItemDroppedEventArgs : EventArgs
    {
        public EffectItem Effect { get; set; }
    }
}
