using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UlearnGameMG
{
    public abstract class GameObject : ITexturable
    {
        public Point position;
        public int Hp;
        public bool canUse = false;
        public bool moving = false;

        public string textureName { get; set; }
        public Texture2D texture { get; set; }

        public void Move(Point point) => position = point;

        public void TextureLoad(Texture2D texture)
        {
            this.texture = texture;
        }
    }

    public class Character : GameObject
    {
        public string Name;
        public int move = 5;
        public Spell FirstSpell = Spell.Shot;
        public Spell SecondSpell;
        public List<Point> canMove;
        public List<Point> canCast;
        public bool moveDo = false;
        public bool castDo = false;

        public Character(string Name, Point pos, int hp, string textureName, Spell spell)
        {
            Hp = hp;
            this.textureName = textureName;
            position = pos;
            this.Name = Name;
            canUse = true;
            moving = true;
            FirstSpell = spell;
        }

        public void Heal() => Hp++;

    }

    public class Enemy : GameObject
    {
        public string Name;
        public int move = 5;
        public Spell FirstSpell = Spell.Sword;
        public Spell SecondSpell;
        public List<(Point, int)> NextAttack = new();

        public Enemy(string Name, int hp, string textureName, Spell spell)
        {
            Hp = hp;
            this.textureName = textureName;
            this.Name = Name;
            canUse = true;
            moving = true;
            position = new(-1, -1);
            FirstSpell = spell;
        }

        public void Heal() => Hp++;

    }

    public class Barricade : GameObject
    {
        public Barricade(Point pos, int hp, string textureName)
        {
            position = pos;
            this.textureName = textureName;
            Hp = hp;
        }
    }

    public class Supplies : GameObject
    {
        public Supplies(Point pos, int hp, string textureName)
        {
            position = pos;
            this.textureName = textureName;
            Hp = hp;
        }
    }

    public class Spell
    {
        public string Name;
        public int start;
        public int end;
        public List<(Point, int)> splashPoints;
        public bool straight;
        public bool moving = true;

        public Spell(string name, int start, int end, List<(Point, int)> spPoints, bool straight, bool? moving = null)
        {
            Name = name;
            this.start = start;
            this.end = end;
            splashPoints = spPoints;
            this.straight = straight;
            this.moving = moving ?? true;
        }

        public List<Point> GetAttackPoints(List<Point> gameObjects)
        {
            var result = new List<Point>();
            var dir = (true, true, true, true);
            if (!straight)
                for (int i = start; i < end; i++)
                {
                    result.Add(new(0, i));
                    result.Add(new(0, -i));
                    result.Add(new(i, 0));
                    result.Add(new(-i, 0));
                }
            else
                for (int i = start; i < end; i++)
                {
                    if (dir.Item1)
                    {
                        if (gameObjects.Contains(new(0, i)))
                        {
                            dir.Item1 = false;
                            result.Add(new(0, i));
                        }
                        else result.Add(new(0, i));
                    }
                    if (dir.Item2)
                    {
                        if (gameObjects.Contains(new(0, -i)))
                        {
                            dir.Item2 = false;
                            result.Add(new(0, -i));
                        }
                        else result.Add(new(0, -i));
                    }
                    if (dir.Item3)
                    {
                        if (gameObjects.Contains(new(i, 0)))
                        {
                            dir.Item3 = false;
                            result.Add(new(i, 0));
                        }
                        else result.Add(new(i, 0));
                    }
                    if (dir.Item4)
                    {
                        if (gameObjects.Contains(new(-i, 0)))
                        {
                            dir.Item4 = false;
                            result.Add(new(-i, 0));
                        }
                        else result.Add(new(-i, 0));
                    }
                }
            return result;
        }

        public List<(Point, int)> GetSpPoints(Point direct)
        {
            if (direct.X > 0)
                return splashPoints;
            if (direct.X < 0)
                return splashPoints.Select(x => (new Point(-x.Item1.X, x.Item1.Y), x.Item2)).ToList();
            if (direct.Y < 0)
                return splashPoints.Select(x => (new Point(x.Item1.Y, -x.Item1.X), x.Item2)).ToList();
            if (direct.Y > 0)
                return splashPoints.Select(x => (new Point(-x.Item1.Y, x.Item1.X), x.Item2)).ToList();
            else return splashPoints;
        }

        static public Spell Shot { get 
            {
                var spPoints = new List<(Point, int)>();
                spPoints.Add((new(0, 0), 2));
                return new Spell("Shot", 2, 9, spPoints, true, false);
            } }

        static public Spell Sword { get
            {
                var spPoints = new List<(Point, int)>();
                spPoints.Add((new(0, 0), 1));
                return new Spell("Sword", 1, 2, spPoints, false);
            } }
        static public Spell Spear
        {
            get
            {
                var spPoints = new List<(Point, int)>();
                spPoints.Add((new(0, 0), 1));
                spPoints.Add((new(1, 0), 2));
                return new Spell("Sword", 1, 3, spPoints, false);
            }
        }
        static public Spell LongSword
        {
            get
            {
                var spPoints = new List<(Point, int)>();
                spPoints.Add((new(0, 0), 2));
                spPoints.Add((new(1, 0), 2));
                return new Spell("Sword", 1, 2, spPoints, false);
            }
        }
    }
}
