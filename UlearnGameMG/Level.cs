using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace UlearnGameMG
{
    public class Level
    {
        public string[,] cells = new string[8, 8];
        public GameObject[,] gameObjects = new GameObject[8, 8];
        public List<Enemy> enemies = new();
        public List<Point> spawnPoints = new();
        public List<Character> characters = new();
        public List<Supplies> suplies = new();
        public int level;

        public Level(int level)
        {
            this.level = level;
        }

        public void SetCells(string[,] cells)
        {
            for (int j = 0; j < 8; j++)
                for (int i = 0; i < 8; i++)
                    this.cells[i,j] = "tiles/isometric_pixel_0" + cells[i,j];
        }

        public void SetObjects(string[,] objects)
        {
            for (int j = 0; j < 8; j++)
                for (int i = 0; i < 8; i++)
                    if (objects[i, j] != "..")
                    {
                           this.gameObjects[j, i] = new Barricade(new(j, i), 5, "objects/tile_0" + objects[i, j]);
                    }
        }

        static public Level GetLevel(int level) 
        {
            if (level == 1)
            {
                var result = new Level(1);
                result.SetCells(new string[8, 8]
                {
                {"014", "014", "073", "046", "046", "046", "014", "014"},
                {"014", "014", "073", "073", "073", "107", "014", "014"},
                {"014", "014", "014", "046", "046", "073", "014", "014"},
                {"014", "014", "073", "046", "107", "073", "014", "014"},
                {"014", "014", "014", "073", "046", "073", "014", "014"},
                {"014", "014", "107", "046", "073", "107", "014", "014"},
                {"014", "014", "046", "046", "046", "014", "014", "014"},
                {"014", "014", "046", "046", "046", "046", "014", "014"}
                });
                result.SetObjects(new string[8, 8]
                {
                {"44", "52", "..", "..", "..", "64", "..", "49"},
                {"44", "..", "..", "..", "..", "..", "..", "47"},
                {"..", "..", "..", "..", "..", "..", "..", ".."},
                {"..", "..", "..", "..", "..", "..", "..", "45"},
                {"62", "..", "..", "..", "..", "..", "..", ".."},
                {"..", "..", "..", "..", "..", "..", "..", ".."},
                {"..", "..", "..", "..", "..", "..", "..", "46"},
                {"45", "..", "64", "..", "..", "..", "46", "51"}
                });
                result.characters.Add(new Character("Archer", new Point(4, 3), 2, "characters/Huntress2", Spell.Shot));
                result.characters.Add(new Character("SwordsMan", new Point(2, 3), 3, "characters/Warrior", Spell.Sword));
                result.spawnPoints.Add(new Point(3, 7));
                result.spawnPoints.Add(new Point(5, 7));
                result.spawnPoints.Add(new Point(0, 3));
                result.spawnPoints.Add(new Point(0, 5));
                result.spawnPoints.Add(new Point(7, 2));
                result.spawnPoints.Add(new Point(7, 4));
                result.enemies.Add(new Enemy("Enemy1", 3, "characters/EvilWarrior", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy2", 3, "characters/EvilWarrior2", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy3", 3, "characters/EvilWarrior", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy4", 3, "characters/EvilWarrior2", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy5", 3, "characters/EvilWarrior", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy6", 3, "characters/EvilWarrior2", Spell.Sword));
                result.suplies.Add(new Supplies(new(2, 2), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(3, 4), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(5, 2), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(5, 5), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(2, 1), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(7, 4), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(1, 6), 1, "objects/tile_001"));
                return result;
            }
            else if (level == 2)
            {
                var result = new Level(2);
                result.SetCells(new string[8, 8]
                { //151, 160, 157, 073, 080, 021
                {"160", "080", "157", "151", "151", "157", "080", "160"},
                {"157", "157", "151", "073", "151", "157", "157", "157"},
                {"157", "151", "151", "157", "157", "151", "151", "157"},
                {"151", "151", "157", "160", "160", "157", "151", "157"},
                {"157", "151", "157", "021", "160", "157", "073", "151"},
                {"157", "157", "151", "157", "157", "151", "157", "157"},
                {"160", "157", "157", "151", "151", "157", "157", "160"},
                {"160", "160", "157", "151", "073", "157", "157", "021"}
                });
                result.SetObjects(new string[8, 8]
                {
                {"59", "48", "..", "..", "..", "..", "..", "56"},
                {"49", "..", "..", "..", "..", "..", "..", "60"},
                {"..", "..", "..", "..", "..", "..", "..", ".."},
                {"..", "..", "..", "58", "57", "..", "..", ".."},
                {"..", "..", "..", "49", "54", "..", "..", ".."},
                {"..", "..", "..", "53", "..", "..", "..", ".."},
                {"50", "..", "..", "..", "..", "..", "..", "59"},
                {"53", "..", "..", "..", "..", "..", "49", "54"}
                });
                result.characters.Add(new Character("Archer", new Point(5, 3), 2, "characters/Huntress2", Spell.Shot));
                result.characters.Add(new Character("SwordsMan", new Point(2, 3), 3, "characters/Warrior", Spell.Sword));
                result.characters.Add(new Character("Archer", new Point(6, 3), 2, "characters/Huntress", Spell.Spear));
                result.spawnPoints.Add(new Point(3, 7));
                result.spawnPoints.Add(new Point(4, 7));
                result.spawnPoints.Add(new Point(0, 3));
                result.spawnPoints.Add(new Point(0, 5));
                result.spawnPoints.Add(new Point(7, 3));
                result.spawnPoints.Add(new Point(7, 4));
                result.enemies.Add(new Enemy("Enemy1", 3, "characters/EvilWarrior", Spell.LongSword));
                result.enemies.Add(new Enemy("Enemy2", 3, "characters/EvilWarrior2", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy3", 2, "characters/EvilMage", Spell.Shot));
                result.enemies.Add(new Enemy("Enemy4", 3, "characters/EvilWarrior2", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy5", 3, "characters/EvilWarrior", Spell.LongSword));
                result.enemies.Add(new Enemy("Enemy6", 2, "characters/EvilMage", Spell.Shot));
                result.enemies.Add(new Enemy("Enemy6", 2, "characters/EvilMage", Spell.Shot));
                result.enemies.Add(new Enemy("Enemy6", 2, "characters/EvilMage", Spell.Shot));
                result.enemies.Add(new Enemy("Enemy4", 3, "characters/EvilWarrior2", Spell.Sword));
                result.suplies.Add(new Supplies(new(1, 1), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(6, 0), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(4, 2), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(1, 4), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(1, 7), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(6, 5), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(6, 6), 1, "objects/tile_001"));
                return result;
            }
            else if (level == 3)
            {
                var result = new Level(3);
                result.SetCells(new string[8, 8]
                { //151, 160, 157, 073, 080, 021
                {"063", "054", "055", "052", "052", "055", "054", "063"},
                {"054", "055", "055", "052", "054", "055", "045", "054"},
                {"055", "055", "055", "052", "052", "055", "055", "054"},
                {"052", "054", "055", "055", "052", "055", "055", "054"},
                {"055", "055", "055", "052", "052", "055", "055", "055"},
                {"045", "045", "063", "052", "055", "055", "055", "052"},
                {"053", "053", "045", "052", "052", "055", "055", "045"},
                {"056", "053", "045", "055", "054", "055", "054", "063"}
                });
                result.SetObjects(new string[8, 8]
                {
                {"68", "65", "..", "..", "..", "..", "67", "64"},
                {"43", "..", "..", "..", "..", "..", "48", "65"},
                {"..", "..", "..", "..", "..", "..", "..", ".."},
                {"..", "..", "..", "..", "..", "..", "..", "62"},
                {"..", "..", "..", "..", "..", "..", "..", ".."},
                {"65", "..", "..", "..", "..", "..", "..", ".."},
                {"47", "..", "..", "..", "..", "..", "..", "49"},
                {"46", "..", "62", "..", "..", "65", "..", "62"}
                });
                result.characters.Add(new Character("Archer", new Point(5, 3), 2, "characters/Huntress2", Spell.Shot));
                result.characters.Add(new Character("SwordsMan", new Point(2, 3), 3, "characters/Warrior", Spell.Sword));
                result.characters.Add(new Character("Archer", new Point(6, 3), 2, "characters/Huntress", Spell.Spear));
                result.spawnPoints.Add(new Point(1, 7));
                result.spawnPoints.Add(new Point(6, 7));
                result.spawnPoints.Add(new Point(0, 2));
                result.spawnPoints.Add(new Point(0, 5));
                result.spawnPoints.Add(new Point(7, 2));
                result.spawnPoints.Add(new Point(7, 5));
                result.enemies.Add(new Enemy("Enemy1", 3, "characters/EvilWarrior", Spell.LongSword));
                result.enemies.Add(new Enemy("Enemy2", 3, "characters/EvilWarrior2", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy3", 2, "characters/EvilMage", Spell.Shot));
                result.enemies.Add(new Enemy("Enemy4", 3, "characters/EvilWarrior2", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy5", 3, "characters/EvilWarrior", Spell.LongSword));
                result.enemies.Add(new Enemy("Enemy6", 2, "characters/EvilMage", Spell.Shot));
                result.enemies.Add(new Enemy("Enemy6", 2, "characters/EvilMage", Spell.Shot));
                result.enemies.Add(new Enemy("Enemy6", 2, "characters/EvilMage", Spell.Shot));
                result.enemies.Add(new Enemy("Enemy4", 3, "characters/EvilWarrior2", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy5", 3, "characters/EvilWarrior", Spell.LongSword));
                result.enemies.Add(new Enemy("Enemy6", 2, "characters/EvilMage", Spell.Shot));
                result.enemies.Add(new Enemy("Enemy5", 5, "characters/EvilWarrior", Spell.LongSword));
                result.enemies.Add(new Enemy("Enemy6", 4, "characters/EvilMage", Spell.Shot));
                result.suplies.Add(new Supplies(new(3, 3), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(3, 4), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(4, 3), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(4, 4), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(5, 0), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(3, 7), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(1, 3), 1, "objects/tile_001"));
                return result;
            }
            else return new Level(1);
        }    
        
    }
}
