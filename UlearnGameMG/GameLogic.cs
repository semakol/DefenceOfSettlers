using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UlearnGameMG
{
    public class GameLogic
    {
        public Map map;
        public List<Character> characters = new();
        public List<Enemy> enemies = new();
        public List<Supplies> suplies = new();
        public Character choise = null;
        public int Turn = 1;

        public GameLogic() { }

        public void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        public void AddSupplies(Supplies character)
        {
            suplies.Add(character);
        }

        public void AddEnemies(Enemy character)
        {
            enemies.Add(character);
        }

        public bool ChoiseCharacter(Point point)
        {
            var ch = characters.Where(x => x.position == point).FirstOrDefault();
            if (ch == default || ch.castDo) { return false; }
            else { 
                choise = ch;
                choise.canMove = map.CanMove(choise.position, choise.move);
                choise.canCast = choise.FirstSpell.GetAttackPoints(map.RelativePosition(choise.position)).Select(x => x + choise.position).ToList();
                return true; 
            }
        }

        public void ClearChoise() { choise = null; }


        public bool CharacterMove(Point point)
        {
            var list = choise.canMove;
            if (list.Contains(point) && !choise.moveDo)
            {
                choise.Move(point);
                choise.moveDo = true;
                choise.canMove = map.CanMove(choise.position, choise.move);
                choise.canCast = choise.FirstSpell.GetAttackPoints(map.RelativePosition(choise.position)).Select(x => x + choise.position).ToList();
                return true;
            }
            else return false;
        }

        public bool CharacterSpell(Point point) 
        {
            if (SpellActivate(choise.position, choise.FirstSpell, point)) 
            {
                choise.castDo = true;
                choise = null;
                return true;
            }
            else return false;
        }

        public bool SpellActivate(Point fPoint, Spell spell, Point tPoint)
        {
            if (spell.GetAttackPoints(map.RelativePosition(choise.position)).All(x => x + fPoint != tPoint)) return false;
            foreach (var atPoint in spell.GetSpPoints(InputManager.mouseCell - fPoint))
            {
                var r = map.gameObjects.Where(x => x.position == tPoint + atPoint.Item1).FirstOrDefault();
                if (r != default) r.Hp -= atPoint.Item2;
            }
            return true;
        }

        public void DoAiTurn()
        {
            foreach (var enemy in enemies)
            {
                
            }
        }

        //public List<(Point, int)> GetAiMove(Enemy enemy)
        //{
        //    var canmove = map.CanMove(enemy.position, enemy.move);

        //}

        public void EndTurn()
        {
            Turn += 1;
            characters.ForEach(x => { x.castDo = false; x.moveDo = false; });
        }

        public void MapLoad(Map map) 
        {
            this.map = map;
            characters.ForEach(x => map.GameObjectAdd(x));
            enemies.ForEach(x => map.GameObjectAdd(x));
            suplies.ForEach(x => map.GameObjectAdd(x));
        }
    }

}
