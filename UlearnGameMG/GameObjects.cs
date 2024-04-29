using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UlearnGameMG
{
    public abstract class GameObject
    {
        public Vector2 position;
        public Texture2D texture;
        public int Hp;
        public bool canUse = false;
    }

    public class Сharacter : GameObject
    {
        public string Name;
        
        public Сharacter(string Name, Vector2 pos, Texture2D texture, int hp)
        {
            Hp = hp;
            this.texture = texture;
            position = pos;
            Name = Name;
            canUse = true;
        }
    }
}
