using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace UlearnGameMG
{
    public class Map
    {
        private MapCell[,] mapCells = new MapCell[8, 8];

        public Map(Texture2D texture)
        {
            for(int x = 0; x < 8; x++)
                for(int y = 0; y < 8; y++)
                {
                    mapCells[x, y] = new MapCell(new Vector2(x, y), texture);
                }
        }

        public void Draw(SpriteBatch _spriteBatch) 
        {
            var rift = new Vector2(200, 250);
            for (int i = 0; i < mapCells.GetLength(0); i++)
                for (int j = mapCells.GetLength(1) - 1; j >= 0; j--)
                {
                    var texture = mapCells[i, j].texture;
                    _spriteBatch.Draw(
                        mapCells[i, j].texture,
                        new Vector2(
                            (j * texture.Width / 2) + (i * texture.Width / 2) + rift.X, 
                            (i * texture.Height / 2 * 0.489f) - (j * texture.Height / 2 * 0.489f) + rift.Y),
                        new Color(255, 255, 255)
                        );
                }
        }

        public Vector2 cordToIso(Vector2 cord)
        {
            return new Vector2(cord.X - cord.Y, (cord.X + cord.Y) / 2);
        }
    }

    public class MapCell
    {
        public Vector2 Location;
        public Texture2D texture;

        public MapCell(Vector2 location, Texture2D texture) 
        { 
            this.Location = location; 
            this.texture = texture;
        }
    }

    
}
