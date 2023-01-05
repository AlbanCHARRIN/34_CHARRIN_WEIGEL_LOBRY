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
        private Labyrinthe _labyrinthe;
        private Personnage _personnage;
        public GraphicsDeviceManager _graphics;


        public Game1()
        {
           
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _menu = new Menu(this);
            _labyrinthe = new Labyrinthe(this);
            _personnage = new Personnage(this);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            KeyboardState keyboardState = Keyboard.GetState();


            if (keyboardState.IsKeyDown(Keys.F2))
            {
                _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice,
                Color.Black));
            }

            else if (keyboardState.IsKeyDown(Keys.F3))
            {
                _screenManager.LoadScreen(_labyrinthe, new FadeTransition(GraphicsDevice,
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
