using Arkanoid.Application.Utils.Game;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public abstract class TimeCommandEffect : CheckableCommandEffect
    {
        protected TimeCommandEffect(ArkanoidScenario scenario) : base(scenario)
        {
            _elapsedTime = 0;
        }

        private double _elapsedTime;

        public override void Check(GameInfo info)
        {
            _elapsedTime += info.ElapsedFrameTime;
            if (_elapsedTime > TimeLimit)
            {
                Disable();
            }
        }

        public abstract float TimeLimit { get; }
    }
}
