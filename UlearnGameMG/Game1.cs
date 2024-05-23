
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public Dictionary<string, Texture2D> textures = new();
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
        public ContentLoader loader;
        public GameInterface ingame;
        

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
            Debug.Assert(true);
            Map_map = new Map();
            Map_map.LevelLoad(Level.level1);
            ingame = GameInterface.InGame;
            Game = new GameLogic();
            Game.AddCharacter(new Character("aboba", new Point(2, 3), 3, "characters/pers2"));
            Game.AddCharacter(new Character("aboba2", new Point(4, 3), 3, "characters/pers2"));
            Game.AddSupplies(new Supplies(new(2, 2), 1, "objects/tile_001"));
            Game.AddSupplies(new Supplies(new(3, 4), 1, "objects/tile_001"));
            Game.AddSupplies(new Supplies(new(5, 2), 1, "objects/tile_001"));
            Game.AddSupplies(new Supplies(new(5, 5), 1, "objects/tile_001"));
            Game.AddSupplies(new Supplies(new(5, 5), 1, "objects/tile_001"));
            Game.AddSupplies(new Supplies(new(1, 1), 1, "objects/tile_001"));
            Game.AddSupplies(new Supplies(new(7, 4), 1, "objects/tile_001"));
            Game.AddSupplies(new Supplies(new(1, 6), 1, "objects/tile_001"));
            Game.MapLoad(Map_map);
            pInput = new PlayerInput(Game, ingame);
            draw = new(Game, pInput, ingame);
            draw.LoadMap(Map_map);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            loader = new(GraphicsDevice);
            foreach (var item in Map_map.GetTexturables())
                item.TextureLoad(Content.Load<Texture2D>(item.textureName));
            foreach (var item in ingame.GetTexturables())
                item.TextureLoad(Content.Load<Texture2D>(item.textureName));
            font = Content.Load<SpriteFont>("arial");
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
            draw.DrawMap(_spriteBatch);
            draw.DrawObjects(_spriteBatch);
            draw.DrawInterface(_spriteBatch);
            draw.DrawDebug(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }

    public interface ITexturable
    {
        public string textureName { get; set; }
        public Texture2D texture { get; set; }

        void TextureLoad(Texture2D texture);
    }
}