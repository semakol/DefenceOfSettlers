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
        public Point mousePoint = new();
        public Vector2 mousePos = new();

        public GameLogic() { }

        public void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        public bool CharacterMove(Character character, Point point)
        {
            var list = map.CanMove(character.position, character.move);
            if (list.Contains(point))
            {
                character.Move(point);
                return true;
            }
            else return false;
        }

        public bool SpellActivate(Point fPoint, Spell spell, Point atPoint)
        {

            foreach (var atPoint in spell.splashPoints)
            {
                var r = map.gameObjects.Where(x => x.position == point + atPoint.Item1).FirstOrDefault();
                if (r != default) r.Hp -= atPoint.Item2;
            }
        }

        public void MapLoad(Map map) 
        {
            this.map = map;
            
        }
    }

}
