using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;


namespace Trapped_in_the_dark
{
    public class Game1 : Game
    {
        public SpriteBatch _spriteBatch { get; set; }
        private ScreenManager _screenManager;
        private Menu _menu;
        private Pause _pause;
        private Personnage _personnage;
        public GraphicsDeviceManager _graphics;


        public enum Etats { Menu, Controls, Play, Quit };

        private Etats etat;




        public Etats Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }

        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _menu = new Menu(this);
            _pause = new Pause(this);
            _personnage = new Personnage(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;

            //_graphics.ToggleFullScreen();
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            _graphics.ApplyChanges();

            Etat = Etats.Menu;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice, Color.Black));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            KeyboardState keyboardState = Keyboard.GetState();

            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                if (this.Etat == Etats.Play)
                    _screenManager.LoadScreen(_personnage, new FadeTransition(GraphicsDevice, Color.Black));



                else if (this.Etat == Etats.Quit)
                    Exit();

            }


            if (keyboardState.IsKeyDown(Keys.F2))
            {
                _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice,
                Color.Black));
            }

            else if (keyboardState.IsKeyDown(Keys.F3))
            {
                _screenManager.LoadScreen(_pause, new FadeTransition(GraphicsDevice,
                Color.Black));
            }

            else if (keyboardState.IsKeyDown(Keys.F4))
            {
                _screenManager.LoadScreen(_personnage, new FadeTransition(GraphicsDevice,
                Color.Black));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Salmon);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
