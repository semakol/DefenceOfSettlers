using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UlearnGameMG
{
    public class Draw
    {
        public SpriteBatch spriteBatch;
        public Vector2 size = new Vector2(126, 75);
        private Vector2 rift = new Vector2(140, 300);
        private Map map;
        private readonly GameLogic gameLogic;
        public Draw(SpriteBatch spriteBatch, GameLogic gameLogic) 
        { 
            this.spriteBatch = spriteBatch;
            this.gameLogic = gameLogic;
        }

        public void LoadMap(Map map) 
        {
            this.map = map;
        }

        public void DrawMap() 
        {
            var mapCells = map.mapCells; 
            for (int j = 0; j < mapCells.GetLength(0); j++)
                for (int i = mapCells.GetLength(1) - 1; i >= 0; i--)
                {
                    var texture = mapCells[j, i].texture;
                    spriteBatch.Draw(
                        texture,
                        new Vector2(
                            (i * size.X / 2) + (j * size.X / 2) + rift.X,
                            (j * size.Y / 2) - (i * size.Y / 2) + rift.Y),
                        new Color(255,255,255)
                        );
                }
        }

        public void DrawObjects()
        {
            foreach (var gameObject in map.gameObjects)
            {
                spriteBatch.Draw(
                gameObject.texture,
                new Vector2(
                    (gameObject.position.Y * size.X / 2) + (gameObject.position.X * size.X / 2) + rift.X,
                    (gameObject.position.X * size.Y / 2) - (gameObject.position.Y * size.Y / 2) + rift.Y - 110),
                gameLogic.choise == gameObject ? new Color(255, 191, 0) : new Color(255, 255, 255)
                );
            }
        }

        private Vector2 cordToIso(Vector2 position)
        {
            float sin = 0.5105f;
            float cos = 0.85985f;
            return new Vector2(
                (position.X * cos + position.Y * cos),
                (-position.X * sin + position.Y * sin)
                );
        }

        private Vector2 IsoToCord(Vector2 position)
        {
            float a = 0.5814967f;
            float b = 0.9794319f;
            return new Vector2(
                (position.X * a - position.Y * b),
                (position.X * a + position.Y * b)
                );
        }
    }
}
