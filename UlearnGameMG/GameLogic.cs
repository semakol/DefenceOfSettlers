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
        public List<Character> characters = new List<Character>();
        public List<Enemies> enemies = new();
        public Character choise = null;

        public GameLogic() { }

        public void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        public bool CharacterMove(Point point)
        {
            var list = map.CanMove(choise.position, choise.move);
            if (list.Contains(point))
            {
                choise.Move(point);
                return true;
            }
            else return false;
        }

        public bool SpellActivate(Point fPoint, Spell spell, Point tPoint)
        {
            if (spell.atacksPoints.All(x => x + fPoint != tPoint)) return false;
            foreach (var atPoint in spell.splashPoints)
            {
                var r = map.gameObjects.Where(x => x.position == tPoint + atPoint.Item1).FirstOrDefault();
                if (r != default) r.Hp -= atPoint.Item2;
            }
            return true;
        }

        public void MapLoad(Map map) 
        {
            this.map = map;
            characters.ForEach(x => map.GameObjectAdd(x));
        }
    }

}
