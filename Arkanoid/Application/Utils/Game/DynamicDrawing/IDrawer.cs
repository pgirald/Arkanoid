using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Game.DynamicDrawing
{
    public interface IDrawer
    {
        public LinkedListNode<IDrawer> ScenarioKey { get; set; }

        public EventHandler<DrawEventArgs> DrawComponent { get; set; }
    }
}
