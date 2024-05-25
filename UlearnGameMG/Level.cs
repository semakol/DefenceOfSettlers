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
                result.characters.Add(new Character("Archer", new Point(4, 3), 2, "characters/pers2", Spell.Shot));
                result.characters.Add(new Character("SwordsMan", new Point(2, 3), 3, "characters/pers2", Spell.Sword));
                result.spawnPoints.Add(new Point(3, 7));
                result.spawnPoints.Add(new Point(5, 7));
                result.spawnPoints.Add(new Point(0, 3));
                result.spawnPoints.Add(new Point(0, 5));
                result.spawnPoints.Add(new Point(7, 2));
                result.spawnPoints.Add(new Point(7, 4));
                result.enemies.Add(new Enemy("Enemy1", 3, "objects/tile_054", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy2", 3, "objects/tile_054", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy3", 3, "objects/tile_054", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy4", 3, "objects/tile_054", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy5", 3, "objects/tile_054", Spell.Sword));
                result.enemies.Add(new Enemy("Enemy6", 3, "objects/tile_054", Spell.Sword));
                result.suplies.Add(new Supplies(new(2, 2), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(3, 4), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(5, 2), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(5, 5), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(2, 1), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(7, 4), 1, "objects/tile_001"));
                result.suplies.Add(new Supplies(new(1, 6), 1, "objects/tile_001"));
                return result;
            }
            else return new Level(1);
        }    
        
    }
}
