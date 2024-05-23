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
                        (InputManager.mouseCell == new Point(i,j) && !attackBool) ? new Color(255, 191, 0) :
                        canSpell.Contains(new Point(i, j)) ? new Color(255, 191, 0) :
                        canMove.Contains(new Point(i,j)) ? new Color(0, 200, 0) : new Color(255,255,255)
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
