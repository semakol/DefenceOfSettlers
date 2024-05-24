using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static UlearnGameMG.Map;

namespace UlearnGameMG
{
    public class Draw
    {
        static public Vector2 size = new Vector2(126, 75);
        static private Vector2 rift = new Vector2(140, 300);
        private IComparer<Point> comparer = new Map.PointCompaire();
        private Map map;
        private readonly GameLogic gameLogic;
        private readonly PlayerInput playerInput;
        private readonly GameInterface gameInterface;
        public Draw(GameLogic gameLogic, PlayerInput playerInput, GameInterface gameInterface) 
        { 
            this.gameLogic = gameLogic;
            this.playerInput = playerInput;
            this.gameInterface = gameInterface; 
        }

        public void LoadMap(Map map) 
        {
            this.map = map;
        }

        public void DrawMap(SpriteBatch spriteBatch) 
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
                        new Color(255, 255, 255)
                        //(InputManager.mouseCell == new Point(i,j) && !attackBool) ? new Color(255, 191, 0) :
                        //canSpell.Contains(new Point(i, j)) ? new Color(255, 191, 0) :
                        //canMove.Contains(new Point(i,j)) ? new Color(0, 200, 0) : new Color(255,255,255)
                        );
                }
        }

        public void DrawTile(SpriteBatch spriteBatch)
        {
            var attackBool = false;
            var canMove = new List<Point>();
            if (playerInput.mode == Mode.Move) canMove = gameLogic.choise.canMove;
            var canSpell = new List<Point>();
            if (playerInput.mode == Mode.Attack)
                if (gameLogic.choise.canCast.Contains(InputManager.mouseCell))
                {
                    canSpell = gameLogic.choise.FirstSpell.GetSpPoints(InputManager.mouseCell - gameLogic.choise.position).Select(x => x.Item1 + InputManager.mouseCell).ToList();
                    attackBool = true;
                }
                else canSpell = gameLogic.choise.canCast;
            var enemySpell = new List<Point>();
            foreach (var enemy in gameLogic.enemies)
            {
                enemySpell.AddRange(enemy.NextAttack.Select(x => x.Item1));
            }
            var mapCells = map.mapCells;
            var list = new List<Point>(canSpell);
            list.AddRange(canMove);
            list.AddRange(enemySpell);
            list.Add(InputManager.mouseCell);
            foreach (var tile in list.OrderBy(x => x, comparer).Where(x => !OutOfBounds(x))) { 
                    spriteBatch.Draw(
                        Game1.tile_mark,
                        new Vector2(
                            (tile.X * size.X / 2) + (tile.Y * size.X / 2) + rift.X,
                            (tile.Y * size.Y / 2) - (tile.X * size.Y / 2) + rift.Y),
                        (InputManager.mouseCell == tile && !attackBool) ? new Color(255, 191, 0) :
                        canSpell.Contains(tile) ? new Color(200, 0, 0) :
                        enemySpell.Contains(tile) ? new Color(255, 10, 10) :
                        canMove.Contains(tile) ? new Color(100, 250, 140) : new Color(255, 255, 255)
                        );
                }
        }

        public void DrawObjects(SpriteBatch spriteBatch)
        {
            var attackBool = false;
            var canSpell = new List<Point>();
            if (playerInput.mode == Mode.Attack)
                if (gameLogic.choise.canCast.Contains(InputManager.mouseCell))
                {
                    canSpell = gameLogic.choise.FirstSpell.GetSpPoints(InputManager.mouseCell - gameLogic.choise.position).Select(x => x.Item1 + InputManager.mouseCell).ToList();
                    attackBool = true;
                }
                else canSpell = gameLogic.choise.canCast;
            foreach (var gameObject in map.gameObjects.OrderBy(x => x.position, comparer))
            {
                spriteBatch.Draw(
                gameObject.texture,
                new Vector2(
                    (gameObject.position.X * size.X / 2) + (gameObject.position.Y * size.X / 2) + rift.X,
                    (gameObject.position.Y * size.Y / 2) - (gameObject.position.X * size.Y / 2) + rift.Y - 78),
                gameLogic.choise == gameObject ? new Color(255, 191, 0) :
                InputManager.mouseCell == gameObject.position && !attackBool ? new Color(255, 191, 0) :
                canSpell.Contains(gameObject.position) ? new Color(255, 191, 0) : new Color(255, 255, 255)
                );
            }
        }

        public void DrawInterface(SpriteBatch spriteBatch)
        {
            gameInterface.Draw(spriteBatch);
            foreach (var enemy in gameLogic.enemies)
            {
                foreach(var point in enemy.NextAttack)
                {
                    spriteBatch.DrawString(Game1.font, point.Item2.ToString(),
                        new Vector2(
                        (point.Item1.X * size.X / 2) + (point.Item1.Y * size.X / 2) + rift.X + 56,
                        (point.Item1.Y * size.Y / 2) - (point.Item1.X * size.Y / 2) + rift.Y - 78),
                        new Color(255, 10, 10),
                        0,
                        new Vector2(0, 0),
                        2,
                        0,
                        0);
                }
            }
            var canSpell = new List<(Point, int)>();
            if (playerInput.mode == Mode.Attack)
                if (gameLogic.choise.canCast.Contains(InputManager.mouseCell))
                {
                    canSpell = gameLogic.choise.FirstSpell.GetSpPoints(InputManager.mouseCell - gameLogic.choise.position).Select(x => (x.Item1 + InputManager.mouseCell, x.Item2)).ToList();
                }
            foreach (var point in canSpell)
            {
                spriteBatch.DrawString(Game1.font, point.Item2.ToString(),
                        new Vector2(
                        (point.Item1.X * size.X / 2) + (point.Item1.Y * size.X / 2) + rift.X + 56,
                        (point.Item1.Y * size.Y / 2) - (point.Item1.X * size.Y / 2) + rift.Y - 78),
                        new Color(255, 10, 10),
                        0,
                        new Vector2(0, 0),
                        2,
                        0,
                        0);
            }
        }
        
        public void DrawDebug(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, InputManager.mouseCell.ToString(), new Vector2(10, 10), new Color(255, 255, 255));
            spriteBatch.DrawString(Game1.font, InputManager.mousePos.ToString(), new Vector2(10, 30), new Color(255, 255, 255));
            spriteBatch.DrawString(Game1.font, playerInput.mode.ToString(), new Vector2(10, 50), new Color(255, 255, 255));
            spriteBatch.DrawString(Game1.font, gameLogic.Turn.ToString(), new Vector2(10, 70), new Color(255, 255, 255));
            //foreach (var gameObject in map.gameObjects)
            //{
            //    spriteBatch.DrawString( Game1.font, gameObject.Hp.ToString(),
            //    new Vector2(
            //        (gameObject.position.X * size.X / 2) + (gameObject.position.Y * size.X / 2) + rift.X,
            //        (gameObject.position.Y * size.Y / 2) - (gameObject.position.X * size.Y / 2) + rift.Y - 120),
            //     new Color(0, 0, 0),
            //     0,
            //     new Vector2(0,0),
            //     2,
            //     0,
            //     0
            //    );
            //}

            //foreach (var enemy in gameLogic.enemies)
            //{
            //    var list = gameLogic.GetAiMove(enemy);
            //    foreach (var p in list)
            //    {
            //        spriteBatch.DrawString(Game1.font, p.Item2.ToString(),
            //        new Vector2(
            //            (p.Item1.X * size.X / 2) + (p.Item1.Y * size.X / 2) + rift.X,
            //            (p.Item1.Y * size.Y / 2) - (p.Item1.X * size.Y / 2) + rift.Y - 120),
            //         new Color(0, 0, 0),
            //         0,
            //         new Vector2(0, 0),
            //         2,
            //         0,
            //         0
            //        );
            //    }
            //}
        }

        static private Vector2 CordToIso(Vector2 position)
        {
            float sin = 0.5105f;
            float cos = 0.85985f;
            return new Vector2(
                (position.X * cos + position.Y * cos),
                (-position.X * sin + position.Y * sin)
                );
        }

        static private Vector2 IsoToCord(Vector2 position)
        {
            float a = 0.5814967f;
            float b = 0.9794319f;
            return new Vector2(
                (position.X * a - position.Y * b),
                (position.X * a + position.Y * b)
                );
        }

        static private Point WindToCord(Point position)
        {
            return IsoToCord(position.ToVector2() - rift - new Vector2(0, size.Y / 2)).ToPoint();
        } 

        static private Point CordToCell(Point position)
        {
            return new Point((int)(position.X / 73.316f), (int)(position.Y / 73.316f));
        }

        static public Point WindToCell(Point position)
        {
            var point = WindToCord(position);
            if (point.X < 0 || point.Y < 0) return new Point(-1, -1); 
            return CordToCell(WindToCord(position));
        }
    }
}
