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
        public Draw(SpriteBatch spriteBatch) 
        { 
            this.spriteBatch = spriteBatch;

        }

        public void DrawMap(Map map) 
        {
            var mapCells = map.mapCells; 
            int t = 0;
            for (int j = 0; j < mapCells.GetLength(0); j++)
                for (int i = mapCells.GetLength(1) - 1; i >= 0; i--)
                {
                    var texture = mapCells[j, i].texture;
                    spriteBatch.Draw(
                        texture,
                        //cordToIso(new Vector2(i * size.X/2, j * size.Y/1.2f)) + rift,
                        new Vector2(
                            (i * size.X / 2) + (j * size.X / 2) + rift.X,
                            (j * size.Y / 2) - (i * size.Y / 2) + rift.Y),
                        new Color(255,255,255)
                        //(new Point(i, j) == nowCell || nowChoiseCell == new Point(i, j) ? new Color(255, 191, 0) :
                         //(canSpell.Contains(new Point(i, j)) ? new Color(255, 191, 0) :
                         //canMove.Contains(new Point(i, j)) ? new Color(0, 200, 0) : new Color(255, 255, 255)))
                        );
                    //_spriteBatch.DrawString(font, j.ToString() + i.ToString(), new Vector2(
                    //        (i * size.X / 2) + (j * size.X / 2) + rift.X,
                    //        (j * size.Y / 2 * 0.489f) - (i * size.Y / 2 * 0.489f) + rift.Y), new Color(255, 255, 255));
                    t++;
                }
        }
    }
}
