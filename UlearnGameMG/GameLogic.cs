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
        public Queue<Enemy> enemiesReserv = new();
        public List<Point> spawnPoints = new();
        public List<Supplies> suplies = new();
        public Character choise = null;
        public int levelN;
        public int spawn = 0;
        public int Hp = 3;
        public int Win = 0;
        public int Turn = 0;
        private Random random = new();

        public GameLogic() { }

        public void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        public void AddSupplies(Supplies character)
        {
            suplies.Add(character);
        }

        public void AddEnemies(Enemy character, Point pos)
        {
            character.position = pos;
            enemies.Add(character);
        }

        public void LevelLoad(Level level)
        {
            level.enemies.ForEach(x => enemiesReserv.Enqueue(x));
            spawnPoints = level.spawnPoints;
            characters = level.characters;
            suplies = level.suplies;
            levelN = level.level;
        }

        public void EnemySpawn()
        {
            int i = 0;
            while (enemies.Count < 3 && i < 2) 
            { 
                i++;
                while (map != null && map.GetGameObjectByPoint(spawnPoints[spawn % spawnPoints.Count]) != default)
                {
                    spawn++;
                }
                if (enemiesReserv.Count != 0)
                    AddEnemies(enemiesReserv.Dequeue(), spawnPoints[spawn % spawnPoints.Count]);
                spawn++;
            }
        }

        public bool ChoiseCharacter(Point point)
        {
            var ch = characters.Where(x => x.position == point).FirstOrDefault();
            if (Turn == 0) return false;
            if (ch == default || ch.castDo) { return false; }
            else { 
                choise = ch;
                choise.canMove = map.CanMove(choise.position, choise.move);
                choise.canCast = choise.FirstSpell.GetAttackPoints(map.RelativePosition(choise.position)).Select(x => x + choise.position).ToList();
                return true; 
            }
        }

        public void ClearChoise() { choise = null; }

        public void CheckState()
        {
            map.gameObjects.RemoveAll(x => x.Hp < 1);
            characters.RemoveAll(x => x.Hp < 1);
            enemies.RemoveAll(x => x.Hp < 1);
            suplies.RemoveAll(x => { if (x.Hp < 1) { Hp -= 1; return true; } else return false; });
            if (Hp < 1 || characters.Count == 0) GameLose();
            if (enemiesReserv.Count == 0 && enemies.Count == 0) GameWin();
        }

        public void GameLose()
        {
            Win = -1;

        }

        public void GameWin()
        {
            Win = 1;
        }


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
            var dir = (tPoint - fPoint).ToVector2();
            dir.Normalize();
            var dirP = dir.ToPoint();
            foreach (var atPoint in spell.GetSpPoints(dirP))
            {
                var r = map.GetGameObjectByPoint(tPoint + atPoint.Item1);
                if (r != default)
                {
                    r.Hp -= atPoint.Item2;
                    if (r.moving && spell.moving)
                    {
                        var r2 = map.GetGameObjectByPoint(r.position + dirP);
                        if (Map.OutOfBounds(r.position + dirP) || r2 != default)
                        {
                            if (r2 != default)
                                r2.Hp -= 1;
                            r.Hp -= 1;
                        }
                        else if (!Map.OutOfBounds(r.position + dirP)) r.Move(r.position + dirP);
                    }
                }
            }
            return true;
        }

        public void DoAiTurn()
        {
            foreach (var enemy in enemies)
            {
                if (Turn != 0)
                {
                    DoAiAttack(enemy);
                }
                CheckState();
                var list = GetAiMove(enemy);
                list = list.OrderByDescending(x => x.Item2).ToList();
                if (list.Count > 3)
                    enemy.Move(list[random.Next(0,2)].Item1);
                CheckAiAttack(enemy);
            }
            CheckState();
        }

        public void CheckAiAttack(Enemy enemy)
        {
            var list = new List<Point>();
            foreach (var atPoint in enemy.FirstSpell.GetAttackPoints(map.RelativePosition(enemy.position)))
            {
                var point = atPoint + enemy.position;
                if (suplies.Select(x => x.position).Contains(point)) list.Add(point);
                if (characters.Select(x => x.position).Contains(point)) list.Add(point);
            }
            if (list.Count <= 0) enemy.NextAttack.Clear();
            else
            enemy.NextAttack = enemy.FirstSpell.GetSpPoints(list[0] - enemy.position).Select(x => (x.Item1 + list[0] - enemy.position,x.Item2)).ToList();
        }

        public void DoAiAttack(Enemy enemy)
        {
            foreach (var atPoint in enemy.NextAttack)
            {
                var r = map.gameObjects.Where(x => x.position == atPoint.Item1 + enemy.position).FirstOrDefault();
                if (r != default) r.Hp -= atPoint.Item2;
            }
        }

        public List<(Point, int)> GetAiMove(Enemy enemy)
        {
            var result = new List<(Point, int)>();
            var canmove = map.CanMove(enemy.position, enemy.move);
            int cost;
            foreach (var point in canmove)
            {
                cost = 0;
                foreach (var atPoint in enemy.FirstSpell.GetAttackPoints(map.RelativePosition(enemy.position)))
                {
                    if (suplies.Select(x => x.position).Contains(atPoint + point)) cost += 2;
                    if (characters.Select(x => x.position).Contains(atPoint + point)) cost += 1;
                }
                result.Add((point, cost));
            }
            return result;
        }

        public void EndTurn()
        {
            characters.ForEach(x => { x.castDo = false; x.moveDo = false; });
            DoAiTurn();
            if (Turn > 0)
                EnemySpawn();
            Turn += 1;
        }

        public void MapLoad(Map map) 
        {
            this.map = map;
            characters.ForEach(x => map.GameObjectAdd(x));
            enemiesReserv.ToList().ForEach(x => map.GameObjectAdd(x));
            suplies.ForEach(x => map.GameObjectAdd(x));
            EnemySpawn();
        }
    }

}
