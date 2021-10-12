using System;

namespace Arkanoid.Application.App.General
{
    public static class BlockFactory
    {
        static BlockFactory()
        {
            BlockNames = Enum.GetNames(typeof(BlockTypes));
            BlockPrototypes = new Block[BlockNames.Length];

            BlockNames[(int)BlockTypes.Block] = nameof(Block);
            BlockPrototypes[(int)BlockTypes.Block] = new Block();
        }

        public static string[] BlockNames;
        private static Block[] BlockPrototypes;

        public static Block Block()
        {
            return new Block() { Texture = _2dTexturesFactory.BlockTexture() };
        }
    }
}
