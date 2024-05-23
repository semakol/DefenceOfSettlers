
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System;
using System.Linq;
using System.Diagnostics;

namespace UlearnGameMG
{
    public class Map
    {
        //public Vector2 size = new Vector2(126, 75); 
        //private Vector2 rift = new Vector2(140, 3..);
        //private Vector2 mousePos = new Vector2(-1, -1);
        //private Point mousePoint = new Point(-1, -1);
        //private bool mouseClick = false;
        //private Point nowCell = new Point(-1, -1);
        //private Point nowChoiseCell = new Point(-1,-1);
        //public List<Point> canMove = new List<Point>();
        //public List<Point> canSpell = new List<Point>();
        public MapCell[,] mapCells = new MapCell[8, 8];
        public List<GameObject> gameObjects = new();


        //public Character choiseCharacter() => !OutOfBounds(nowChoiseCell) ? (Character)gameObjects[nowChoiseCell.X, nowChoiseCell.Y] : null;

        //public void Draw(SpriteBatch _spriteBatch, Texture2D mark, SpriteFont font) 
        //{
        //    int t = 0;
        //    for (int j = 0; j < mapCells.GetLength(0); j++)
        //        for (int i = mapCells.GetLength(1) - 1; i >= 0; i--)
        //        {
        //            var texture = mapCells[j, i].texture;
        //            _spriteBatch.Draw(
        //                texture,
        //                //cordToIso(new Vector2(i * size.X/2, j * size.Y/1.2f)) + rift,
        //                new Vector2(
        //                    (i * size.X / 2) + (j * size.X / 2) + rift.X, 
        //                    (j * size.Y / 2) - (i * size.Y / 2) + rift.Y),
        //                (new Point(i,j) == nowCell || nowChoiseCell == new Point(i,j) ? new Color(255, 191, 0) :
        //                 (canSpell.Contains(new Point(i, j)) ? new Color(255, 191, 0) : 
        //                 canMove.Contains(new Point(i, j)) ? new Color(0, 2.., 0) : new Color(255, 255, 255)))
        //                );
        //            //_spriteBatch.DrawString(font, j.ToString() + i.ToString(), new Vector2(
        //            //        (i * size.X / 2) + (j * size.X / 2) + rift.X,
        //            //        (j * size.Y / 2 * 0.489f) - (i * size.Y / 2 * 0.489f) + rift.Y), new Color(255, 255, 255));
        //            t++;
        //        }
        //    for (int j = 0; j < mapCells.GetLength(0); j++)
        //        for (int i = mapCells.GetLength(1) - 1; i >= 0; i--)
        //        {
        //            if (gameObjects[i, j] != null)
        //            {
        //                _spriteBatch.Draw(
        //                gameObjects[i, j].texture,
        //                new Vector2(
        //                    (i * size.X / 2) + (j * size.X / 2) + rift.X,
        //                    (j * size.Y / 2) - (i * size.Y / 2) + rift.Y - 110),
        //                (new Point(i, j) == nowCell ? new Color(255, 191, 0) : new Color(255, 255, 255))
        //                );
        //            }
        //        }
        //    for (int x = 0; x < 9; x++)
        //        for (int y = 0; y < 9; y++)
        //        {
        //        //    _spriteBatch.Draw(mark, new Vector2(x * 73.316f, y * 73.316f), new Color(255, 255, 255));
        //        }
        //    // _spriteBatch.Draw(mark, mousePos, new Color(255, 255, 255));
        //    _spriteBatch.DrawString(font, mode.ToString(), new Vector2(10,10), new Color(255, 255, 255));
        //}

        

        //public void CheckMouse(MouseState mouse, out Point mousePoint)
        //{
        //    mousePos = IsoToCord(mouse.Position.ToVector2() - rift - new Vector2(0, size.Y / 2));
        //    mousePoint = new Point((int)(mousePos.X / 73.316f), (int)(mousePos.Y / 73.316f));
        //    //mouseClick = mouse.LeftButton.HasFlag(ButtonState.Pressed);
        //    //var cordPos = IsoToCord(mouse.Position.ToVector2() - rift - new Vector2(0,size.Y / 2));
        //    //var cordPos2 = new Point((int)(cordPos.X / 73.316f), (int)(cordPos.Y / 73.316f));
        //    //canSpell.Clear();
        //    //if (OutOfBounds(cordPos2))
        //    //{
        //    //    nowCell = new Point(-1, -1);
        //    //    if (mouse.LeftButton.HasFlag(ButtonState.Pressed))
        //    //    { 
        //    //        nowChoiseCell = new Point(-1, -1);
        //    //        canMove.Clear();
        //    //    }    
        //    //}
        //    //else
        //    //{
        //    //    if (choiseCharacter() != null)
        //    //    CanMoveDetect(nowChoiseCell, choiseCharacter().move);
        //    //    nowCell = cordPos2;
        //    //    if (mouse.LeftButton.HasFlag(ButtonState.Pressed))
        //    //    {
        //    //        if (!spellCasting && gameObjects[nowCell.X, nowCell.Y] != null && gameObjects[nowCell.X, nowCell.Y].canUse) 
        //    //        {
        //    //            var gameObject = (Character)gameObjects[nowCell.X, nowCell.Y];
        //    //            nowChoiseCell = new Point(nowCell.X, nowCell.Y);
        //    //            CanMoveDetect(nowChoiseCell, gameObject.move);
        //    //        }
        //    //        else if ((!spellCasting && gameObjects[nowCell.X, nowCell.Y] is null) && (canMove.Contains(cordPos2)))
        //    //        {
        //    //            ObjectMove(choiseCharacter(), cordPos2);
        //    //            nowChoiseCell = new Point(-1, -1);
        //    //            canMove.Clear();
        //    //        }
        //    //        else if (!spellCasting)
        //    //        {

        //    //        }
        //    //    }
        //    //}
        //    //if (spellCasting)
        //    //{
        //    //    canSpell.Clear();
        //    //    canMove.Clear();
        //    //    if (!OutOfBounds(nowChoiseCell))
        //    //    {
        //    //        var spell = choiseCharacter().FirstSpell;

        //    //        if (spell.atacksPoints.Select(x => x + nowChoiseCell).Contains(cordPos2))
        //    //            spell.splashPoints.ForEach(x => canSpell.Add(x.Item1 + cordPos2));
        //    //        else spell.atacksPoints.ForEach(x => canSpell.Add(x + nowChoiseCell));
        //    //    }
        //    //    mousePos = cordPos;
        //    //}

        //}

        //public void mouseEvent()
        //{
        //    if (mouseClick)
        //    {
        //        var choise = choiseCharacter();
        //        var point = mousePoint;
        //        if (mode == Mode.Choise)
        //        {
        //            if (OutOfBounds(mousePoint)) return;
        //            nowChoiseCell = point;
        //            mode = Mode.Move;
        //            CanMoveDetect(nowChoiseCell, choiseCharacter().move);
        //        }
        //        else if (mode == Mode.Move) 
        //        {
        //            if (canMove.Contains(point)) ObjectMove(choise, point);
        //            nowChoiseCell = new Point(-1,-1);
        //        }
        //        else if (mode == Mode.Attack)
        //        {

        //        }
        //    }
        //    if (OutOfBounds(mousePoint)) return;
        //    nowCell = mousePoint;
        //}
        
        //public void CharacterAdd(Character character)
        //{
        //    gameObjects[(int)character.position.X, (int)character.position.Y] = character;
        //}

        //public void SpellActivate(Spell spell)
        //{
        //    canMove.Clear();
            
        //}

        //private void ObjectMove(GameObject gameObject, Point point)
        //{
        //    gameObjects[gameObject.position.X, gameObject.position.Y] = null;
        //    gameObjects[point.X, point.Y] = gameObject;
        //    gameObject.position = point;
        //}

        //private Vector2 cordToIso(Vector2 position)
        //{
        //    float sin = 0.5105f;
        //    float cos = 0.85985f;
        //    return new Vector2(
        //        (position.X * cos + position.Y * cos),
        //        (-position.X * sin + position.Y * sin)
        //        );
        //}

        //private Vector2 IsoToCord(Vector2 position)
        //{
        //    float a = 0.5814967f;
        //    float b = 0.9794319f;
        //    return new Vector2(
        //        (position.X * a - position.Y * b),
        //        (position.X * a + position.Y * b)
        //        );
        //}

        public List<Point> CanMove(Point start, int move)
        {
            var canMove = new List<Point>();
            var queue = new Queue<(Point, int)>();
            var visited = new HashSet<(Point, int)>();
            bool first = true;
            queue.Enqueue((start, move+1));
            while (queue.Count != 0)
            {
                var deQueue = queue.Dequeue();
                var point = deQueue.Item1;
                if (OutOfBounds(point)) continue;
                if ((gameObjects.Any(x => x.position == point) && !first) || deQueue.Item2 == 0 || visited.Contains(deQueue)) continue;
                
                if (first) first = false;
                else if (!canMove.Contains(point)) canMove.Add(point);

                visited.Add(deQueue);

                for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                        if (dx != 0 && dy != 0 || dx == dy) continue;
                        else queue.Enqueue((new Point(point.X + dx, point.Y + dy), deQueue.Item2-1));

            }
            return canMove;
        }

        public List<Point> RelativePosition(Point point)
        {
            return gameObjects.Select(x => x.position - point).ToList() ;
        }

        public List<ITexturable> GetTexturables()
        {
            var list = new List<ITexturable>();
            foreach (var cell in mapCells)
                list.Add(cell);
            foreach (var obj in gameObjects)
                list.Add(obj);
            return list;
        }

        public void GameObjectAdd(GameObject go)
        {
            if (go == null && gameObjects.Any(x => x.position != go.position)) Debug.Write("Cann't add object");
            else gameObjects.Add(go);
        }

        public void LevelLoad(Level level)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    mapCells[i, j] = new MapCell(new(i, j), level.cells[i, j]);
            foreach(var obj in level.gameObjects)
                if (obj != null)
                    this.gameObjects.Add((GameObject)obj);
        }

        static public bool OutOfBounds(Point point)
        {
            return (point.X < 0 || point.Y < 0 || point.X > 7 || point.Y > 7 );
        }

        public class PointCompaire : IComparer<Point>
        {
            public int Compare(Point x, Point y)
            {
                if (x.Y == y.Y)
                {
                    if (x.X > y.X) return -1;
                    if (x.X == y.X) return 0;
                    else return 1;
                }
                if (x.Y < y.Y) return -1;
                else return 1;
            }
        }
    }

    public class MapCell : ITexturable
    {
        public Point Location;
        
        //public GameObject;

        public MapCell(Point location, string textureName) 
        { 
            this.Location = location; 
            this.textureName = textureName;
        }

        public string textureName { get; set; }
        public Texture2D texture { get; set; }

        public void TextureLoad(Texture2D texture)
        {
            this.texture = texture;
        }
    }

    
}
