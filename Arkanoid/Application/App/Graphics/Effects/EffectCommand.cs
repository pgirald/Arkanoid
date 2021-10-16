using Arkanoid.Application.Utils.GeneralExtensions;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public abstract class EffectCommand
    {
        public EffectCommand(ArkanoidScenario scenario)
        {
            _fullName = GetType().FullName;
            _scenario = scenario;
            IncompatibleEffects = new Dictionary<string, string>();
            string key = _fullName;
            IncompatibleEffects.Add(key, key);
        }

        private string _fullName;

        private Dictionary<string, string> IncompatibleEffects;

        private LinkedListNode<KeyValuePair<string, EffectCommand>> _key;

        protected ArkanoidScenario _scenario;

        public virtual void Enable()
        {
            DisableIncompatibles();
            _key = _scenario.ApplyiedEffects.AddLast(new KeyValuePair<string, EffectCommand>(_fullName, this));
            ChangeScenario();
        }

        public virtual void Disable()
        {
            _scenario.ApplyiedEffects.Remove(_key);
            _key = null;
            RecoverScenario();
        }

        protected abstract void ChangeScenario();

        protected abstract void RecoverScenario();

        private void DisableIncompatibles()
        {
            string aux;
            _scenario.ApplyiedEffects.ForAll(effect =>
            {
                if (IncompatibleEffects.TryGetValue(effect.Value.Key, out aux))
                {
                    effect.Value.Value.Disable();
                }
            });
        }

        public void AddIncompatibleEffect<T>() where T : EffectCommand
        {
            string key = typeof(T).FullName;
            string aux;
            if (IncompatibleEffects.TryGetValue(key, out aux))
            {
                throw new Exception("The specified effect type is already registered");
            }
            IncompatibleEffects.Add(key, key);
        }
    }
}
