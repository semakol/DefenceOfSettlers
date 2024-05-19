
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UlearnGameMG
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map Map_map;
        private Texture2D block;
        private Texture2D mark;
        private Texture2D pers;
        static public SpriteFont font;
        Vector2 position = Vector2.Zero;
        private readonly Vector2 Scale;
        private Rectangle cellRect = new Rectangle(0,0, 126, 72);
        private MouseState lastMouseState;
        private readonly Matrix screenXform;
        private bool click = false;
        private GameLogic Game;
        private Draw draw;
        private PlayerInput pInput;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            var screenScale = _graphics.PreferredBackBufferHeight / 720.0f;
            screenXform = Matrix.CreateScale(screenScale, screenScale, 1.0f);
            Scale = new Vector2(_graphics.PreferredBackBufferWidth / 1280, _graphics.PreferredBackBufferHeight / 720);
        }

        protected override void Initialize()
        { 
            base.Initialize();
            Debug.Assert(true);
            Map_map = new Map(block);
            Game = new GameLogic();
            Game.AddCharacter(new Character("aboba", new Point(1, 1), pers, 3));
            Game.AddCharacter(new Character("aboba2", new Point(1, 3), pers, 3));
            Game.MapLoad(Map_map);
            pInput = new PlayerInput(Game);
            draw = new(_spriteBatch, Game, pInput);
            draw.LoadMap(Map_map);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            block = Content.Load<Texture2D>("isometric_pixel_0014");
            mark = Content.Load<Texture2D>("mark");
            font = Content.Load<SpriteFont>("arial");
            pers = Content.Load<Texture2D>("pers2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyUp(Keys.Escape)) { click = false; }

            InputManager.Update();
            pInput.ClickHandler();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, screenXform);
            draw.DrawMap();
            draw.DrawObjects();
            draw.DrawDebug();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}