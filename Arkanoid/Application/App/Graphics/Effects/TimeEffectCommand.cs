using Arkanoid.Application.Utils.Game;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public abstract class TimeEffectCommand : CheckableEffectCommand
    {
        protected TimeEffectCommand(ArkanoidScenario scenario) : base(scenario)
        {
            _elapsedTime = 0;
        }

        private double _elapsedTime;

        public override void Check(GameInfo info)
        {
            _elapsedTime += info.ElapsedFrameTime;
            if (_elapsedTime > SecondsLimit)
            {
                Disable();
            }
        }

        public abstract float SecondsLimit { get; }
    }
}
