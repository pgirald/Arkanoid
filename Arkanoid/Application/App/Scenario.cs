using Arkanoid.Application.App.Components;
using Arkanoid.Application.App.Components.Textures.Paddles;
using Arkanoid.Application.App.Components.Textures.Projectiles;
using Arkanoid.Application.Utils;

namespace Arkanoid.Application.App
{
    public class Scenario : TexturesContainer
    {
        public BlockSet Blocks { get; set; }

        public Paddle Paddle { get; set; }

        public Projectile Projectile { get; set; }
    }
}
