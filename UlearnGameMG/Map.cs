
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace UlearnGameMG
{
    public class Map
    {
        public Vector2 size = new Vector2(126, 75); 
        private MapCell[,] mapCells = new MapCell[8, 8];
        private GameObject[,] gameObjects= new GameObject[8, 8];
        private Vector2 rift = new Vector2(140, 300);
        private Vector2 mousePos = new Vector2(0,0);
        private (int, int) nowCell = (0, 0);
        private Point nowChoiseCell = new Point(-1,-1);
        private List<Point> canMove = new List<Point>();

        public Map(Texture2D texture)
        {
            for(int x = 0; x < 8; x++)
                for(int y = 0; y < 8; y++)
                {
                    mapCells[x, y] = new MapCell(new Vector2(x, y), texture);
                }
        }

        public void Draw(SpriteBatch _spriteBatch, Texture2D mark, SpriteFont font) 
        {
            int t = 0;
            for (int j = 0; j < mapCells.GetLength(0); j++)
                for (int i = mapCells.GetLength(1) - 1; i >= 0; i--)
                {
                    
                    var texture = mapCells[j, i].texture;
                    _spriteBatch.Draw(
                        texture,
                        //cordToIso(new Vector2(i * size.X/2, j * size.Y/1.2f)) + rift,
                        new Vector2(
                            (i * size.X / 2) + (j * size.X / 2) + rift.X, 
                            (j * size.Y / 2) - (i * size.Y / 2) + rift.Y),
                        ((i,j) == nowCell || nowChoiseCell == new Point(i,j) ? new Color(255, 191, 0) :
                         (canMove.Contains(new Point(i, j)) ? new Color(255, 191, 0) : new Color(255, 255, 255)))
                        );
                    //_spriteBatch.DrawString(font, j.ToString() + i.ToString(), new Vector2(
                    //        (i * size.X / 2) + (j * size.X / 2) + rift.X,
                    //        (j * size.Y / 2 * 0.489f) - (i * size.Y / 2 * 0.489f) + rift.Y), new Color(255, 255, 255));
                    t++;
                }
            for (int j = 0; j < mapCells.GetLength(0); j++)
                for (int i = mapCells.GetLength(1) - 1; i >= 0; i--)
                {
                    if (gameObjects[i, j] != null)
                    {
                        _spriteBatch.Draw(
                        gameObjects[i, j].texture,
                        new Vector2(
                            (i * size.X / 2) + (j * size.X / 2) + rift.X,
                            (j * size.Y / 2) - (i * size.Y / 2) + rift.Y - 110),
                        ((i, j) == nowCell ? new Color(255, 191, 0) : new Color(255, 255, 255))
                        );
                    }
                }
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                //    _spriteBatch.Draw(mark, new Vector2(x * 73.316f, y * 73.316f), new Color(255, 255, 255));
                }
            _spriteBatch.Draw(mark, mousePos, new Color(255, 255, 255));
        }

        public void CheckMouseCell(MouseState mouse)
        {
            var cordPos = IsoToCord(mouse.Position.ToVector2() - rift - new Vector2(0,size.Y / 2));
            var cordPos2 = new Vector2(cordPos.X / 73.316f, cordPos.Y / 73.316f);
            if (cordPos2.X < 0 || cordPos2.Y < 0 || cordPos2.X > 8 || cordPos2.Y > 8)
            {
                nowCell = (-1, -1);
                if (mouse.LeftButton.HasFlag(ButtonState.Pressed)) nowChoiseCell = new Point(-1, -1);
            }
            else
            {
                nowCell = ((int)(cordPos.X / 73.316f), (int)(cordPos.Y / 73.316f));
                if (mouse.LeftButton.HasFlag(ButtonState.Pressed))
                {
                    if (gameObjects[nowCell.Item1, nowCell.Item2] != null && gameObjects[nowCell.Item1, nowCell.Item2].canUse) 
                    { 
                        nowChoiseCell = new Point(nowCell.Item1, nowCell.Item2);
                        CanMoveDetect(nowChoiseCell, 5);
                    }
                }
            }
            mousePos = cordPos;
        }

        public void CharacterAdd(Сharacter character)
        {
            gameObjects[(int)character.position.X, (int)character.position.Y] = character;
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

        public void CanMoveDetect(Point start, int move)
        {
            for (int x = 0; x < move; x++)
                for (int y = 0; y < move; y++)
                {
                    if (gameObjects[x+start.X, y+start.Y] is null)
                    {
                        canMove.Add(new Point(x, y));
                    }
                }
        }
    }

    public class MapCell
    {
        public Vector2 Location;
        public Texture2D texture;
        //public GameObject;

        public MapCell(Vector2 location, Texture2D texture) 
        { 
            this.Location = location; 
            this.texture = texture;
        }
    }

    
}
