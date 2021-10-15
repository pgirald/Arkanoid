using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Game.DynamicDrawing
{
    public interface IScenarioDraw
    {
        public LinkedListNode<IScenarioDraw> ScenarioKey { get; set; }

        public void Draw(ScenarioUtils utils);

        public void Erase(ScenarioUtils utils);
    }
}
