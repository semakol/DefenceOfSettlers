using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UlearnGameMG
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }

    public class GameInterface
    {
        public SpriteBatch spriteBatch;
        public List<IDrawable> objects = new();
        public List<Button> buttons = new();

        public GameInterface(SpriteBatch spriteBatch) 
        { 
            this.spriteBatch = spriteBatch;
        }

        public void Draw()
        {
            foreach (var obj in objects)
            {
                obj.Draw(spriteBatch);
            }
        }

        public void Update()
        {
            foreach (var button in buttons)
            {
                button.ClickButton();
            }
        }

        public void AddButton(Button button)
        {
            buttons.Add(button);
        }
        
        public class Button : IDrawable
        {
            public Rectangle rect;
            public Texture2D texture;
            public Action action;

            public Button(Point point, Texture2D texture, Action action) 
            { 
                rect = new Rectangle(point.X, point.Y, texture.Width, texture.Height);
                texture = texture;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(
                texture,
                rect,
                new Color(255, 255, 255)
                );
            }

            public bool enterButton()
            {
                if (rect.Contains(InputManager.mousePos))
                {
                    return true;
                }
                return false;
            }

            public bool ClickButton()
            {
                if (InputManager.JustPressed(MouseInput.LeftButton) && enterButton())
                {
                    action.Invoke();
                    return true;
                }
                return false;
            }
        }
    }
}
