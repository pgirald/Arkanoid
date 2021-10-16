using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Game.DynamicDrawing;

namespace Arkanoid.Application.App.Game.Scenarios
{
    public class ArkanoidScenarioUtils : ScenarioUtils
    {
        private ArkanoidScenario _arkScenario;

        public ArkanoidScenarioUtils(ScenarioUtils utils) : base(utils)
        {
            _arkScenario = (ArkanoidScenario)utils.Scenario;
        }

        public Paddle Paddle => _arkScenario.Paddle;

        public BlockSet Blocks => _arkScenario.Blocks;

        public Projectile Projectile => _arkScenario.Projectile;
    }
}
