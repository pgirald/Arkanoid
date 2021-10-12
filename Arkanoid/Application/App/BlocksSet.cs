using Arkanoid.Application.Utils;
using Microsoft.Xna.Framework;

namespace Arkanoid.Application.App
{
    public class BlocksSet : Container
    {
        private float _height = 0;
        private float _width = 0;

        public BlocksSet() { }

        public BlocksSet(ParentContainer container)
        {
            Container = container;
        }

        public void AddRows()
        {
            float YPos = 0;
            BlocksRow row;
            for (int i = 1; i <= 6; i++)
            {
                row = generateRow(0, YPos);
                YPos = row.Bottom;
            }
            OriginAlignment = Alignment.TopCenter;
            PositionAlignment = Alignment.MiddleCenter;
        }

        private BlocksRow generateRow(float X, float Y)
        {
            BlocksRow row = new BlocksRow(this, X, Y) { MaxWidth = Container.Width / 2, OriginAlignment = Alignment.TopLeft };
            while (row.AddBlock()) ;
            if (row.Width > _width)
                _width = row.Width;
            _height += row.Height;
            base.Size = new Vector2(_width, _height);
            return row;
        }
    }
}
