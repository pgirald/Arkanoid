using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public static class EffectItemsFactory
    {
        private static List<EffectItem> EffectItems;

        private static Random NumberGenerator;

        private const double Probability = 1;

        static EffectItemsFactory()
        {
            EffectItems = new List<EffectItem>();
            NumberGenerator = new Random();
        }

        public static void AddItem(EffectItem effectItem)
        {
            EffectItems.Add(effectItem);
        }

        public static EffectItem GetEffectItemRandomly()
        {
            double generatedNumber = NumberGenerator.NextDouble();
            if (generatedNumber > Probability)
            {
                return null;
            }
            int itemIndex = NumberGenerator.Next(0, EffectItems.Count);
            return (EffectItem)EffectItems[itemIndex].Clone();
        }
    }
}
