
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime;
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
        static public SpriteFont font2;
        static public Texture2D tile_mark;
        static public Texture2D logo;
        static public Texture2D lor;
        static public Texture2D help;
        static public Texture2D fon;
        static public Texture2D lorB;
        public Vector2 Scale;
        private Matrix screenXform;
        private GameLogic Game;
        private DrawGame draw;
        private PlayerInput pInput;
        public ContentLoader loader;
        private bool IsInGame = false;
        private GameInterface InMenu = GameInterface.InMenu;
        private ScreenMode screenMode = ScreenMode.s1280;

        private Action NextLevel => () => { 
            if (Game.levelN + 1 == 4) Menu.Invoke();
            else InitLevel(Game.levelN + 1); };

        private Action Menu => () => {
            IsInGame = false; };

        private Action Restart => () => {
            InitLevel(Game.levelN); };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            var screenScale = _graphics.PreferredBackBufferHeight / 720.0f;
            screenXform = Matrix.CreateScale(screenScale, screenScale, 1.0f);
            Scale = new Vector2(screenScale, screenScale);
        }

        protected override void Initialize()
        {
            InMenu.buttons["Exit"].SetAction(() => { Exit(); });
            InMenu.buttons["StartGame"].SetAction(() => { InitLevel(1); IsInGame = true; });
            InMenu.buttons["S1920"].SetAction(() => { SetS1920(); });
            InMenu.buttons["S1280"].SetAction(() => { SetS1280(); });
            Window.Title = "Defense of settlers";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            loader = new(GraphicsDevice);
            font = Content.Load<SpriteFont>("arial");
            font2 = Content.Load<SpriteFont>("unispace");
            logo = Content.Load<Texture2D>("interface/Logo");
            lor = Content.Load<Texture2D>("interface/lor");
            fon = Content.Load<Texture2D>("interface/fon");
            tile_mark = Content.Load<Texture2D>("interface/Mark_tile");
            help = Content.Load<Texture2D>("interface/help");
            lorB = Content.Load<Texture2D>("interface/lorB");
            foreach (var item in InMenu.GetTexturables())
                item.TextureLoad(Content.Load<Texture2D>(item.textureName));
        }

        public void LoadLevelContent()
        {
            foreach (var item in Map_map.GetTexturables())
                item.TextureLoad(Content.Load<Texture2D>(item.textureName));
            foreach (var item in pInput.GetTexturables())
                item.TextureLoad(Content.Load<Texture2D>(item.textureName));
        }

        public void InitLevel(int level)
        {
            var newLevel = Level.GetLevel(level);
            Map_map = new Map();
            Map_map.LevelLoad(newLevel);
            Game = new GameLogic();
            Game.LevelLoad(newLevel);
            Game.MapLoad(Map_map);
            pInput = new PlayerInput(Game);
            pInput.SetButtonsAction(Restart, Menu, NextLevel);
            draw = new(Game, pInput);
            draw.LoadMap(Map_map);
            LoadLevelContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update(Scale.Y);
            if (IsInGame)
            {
                pInput.ClickHandler(Menu);
            }
            else
            {
                InMenu.Update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, screenXform);
            if (IsInGame)
            {
                draw.DrawMap(_spriteBatch);
                draw.DrawTile(_spriteBatch);
                draw.DrawObjects(_spriteBatch);
                draw.DrawInterface(_spriteBatch);
                //draw.DrawDebug(_spriteBatch);
            }
            else
            {
                DrawGame.DrawMenu(_spriteBatch, InMenu);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SetS1280()
        {
            if (screenMode == ScreenMode.s1280) return;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            var screenScale = _graphics.PreferredBackBufferHeight / 720.0f;
            screenXform = Matrix.CreateScale(screenScale, screenScale, 1.0f);
            Scale = new Vector2(screenScale, screenScale);
            Window.IsBorderless = false;
            Window.Position = new Point(0, 40);
            _graphics.ApplyChanges();
            screenMode = ScreenMode.s1280;
        }

        private void SetS1920()
        {
            if (screenMode == ScreenMode.s1920) return;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            var screenScale = _graphics.PreferredBackBufferHeight / 720.0f;
            screenXform = Matrix.CreateScale(screenScale, screenScale, 1.0f);
            Scale = new Vector2(screenScale, screenScale);
            Window.IsBorderless = true;
            Window.Position = new Point(0, 0);
            _graphics.ApplyChanges();
            screenMode = ScreenMode.s1920;
        }
    }

    public enum ScreenMode
    {
        s1920,
        s1280
    }

    public interface ITexturable
    {
        public string textureName { get; set; }
        public Texture2D texture { get; set; }

        void TextureLoad(Texture2D texture);
    }
}