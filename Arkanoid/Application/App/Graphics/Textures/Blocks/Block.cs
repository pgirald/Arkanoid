using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Textures;
using System;

namespace Arkanoid.Application.App.Graphics.Textures.Blocks
{
    public class Block : TextureComponent
    {
        public EventHandler<ItemDroppedEventArgs> ItemDropped;

        public override string ParentPath => "Blocks/";

        public override string TexturePath => "Block";

        public float MarginLeft { get; set; } = 0;

        public float MarginTop { get; set; } = 0;

        public float MarginRight { get; set; } = 0;

        public float MarginBottom { get; set; } = 0;

        public virtual void Hit()
        {
            EffectItem effect = EffectItemsFactory.GetEffectItemRandomly();
            if (effect != null)
            {
                effect.PutOn(this, Alignment.BottomCenter);
                ItemDropped?.Invoke(this, new ItemDroppedEventArgs { Effect = effect });
            }
            Destroyed?.Invoke(this, null);
        }
    }
}
