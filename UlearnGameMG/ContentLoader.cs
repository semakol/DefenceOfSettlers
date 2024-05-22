using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace UlearnGameMG
{
    public class ContentLoader
    {
        private GraphicsDevice graphics;
        public ContentLoader(GraphicsDevice graphics) 
        { 
            this.graphics = graphics;
        }

        public Texture2D GetTexture(string pFile)
        {
            Texture2D texture = null;

            if (File.Exists(pFile))
            {
                using (var stream = File.OpenRead(pFile))
                {
                    texture = Texture2D.FromStream(graphics, stream);
                }
            }
            else
            {
                throw new FileNotFoundException("Not find file " + pFile);
            }

            return texture;
        }
    }
}
