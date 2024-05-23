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

        static public Level level1 { get
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
                {"..", "..", "..", "..", "..", "..", "45", ".."},
                {"62", "..", "..", "..", "..", "..", "..", ".."},
                {"..", "..", "..", "..", "..", "..", "..", ".."},
                {"..", "..", "..", "..", "..", "..", "..", "46"},
                {"45", "..", "64", "..", "..", "..", "46", "51"}
                });
                return result;
            }}    
        
    }
}
