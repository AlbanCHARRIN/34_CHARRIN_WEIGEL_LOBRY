using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.IO;
using System.Text;

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
        public int[,] _tableauMur;
        public int dimensionx = Case.dimensionx;
        public int dimensiony = Case.dimensiony;

        //Map

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

            _tableauMur = Case.GenerateurDuTileset();
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

            else if (keyboardState.IsKeyDown(Keys.F2))
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

        /*private void MapAléatoire()
        {

            Random rand = new Random();
            int valeurMur;

            for (int i = 0; i < dimensionx; i++)
            {
                for (int j = 0; j < dimensiony; j++)
                {
                    valeurMur = rand.Next(8, 13);
                    if (_personnage.mapLayer.GetTileIndex((ushort) i, (ushort) j) == 1)
                    {
                        _personnage.mapLayer.RemoveTile((ushort)i, (ushort)j);
                        _personnage._tiledMap.AddTileset(_personnage._tileset, valeurMur);
                    }
                        
                }


            }

            String line;
            try
            {
                //Open the File
                StreamWriter sw = new StreamWriter("C:\\Users\\weigelr\\source\repos\\AlbanCHARRIN\\34_CHARRIN_WEIGEL_LOBRY\\Trapped in the dark\\Content\\Map.tmx", true, Encoding.ASCII);
                StreamReader sr = new StreamReader("C:\\Users\\weigelr\\source\repos\\AlbanCHARRIN\\34_CHARRIN_WEIGEL_LOBRY\\Trapped in the dark\\Content\\Map.tmx");

                line = sr.ReadLine();
                Random rand = new Random();
                if (line.Contains("Obstacle"))
                {
                    line = sr.ReadLine();
                    for (int i = 0; i < dimensionx; i++)
                    {
                        for (int j = 0; j < dimensiony; j++)
                        {
                            if (_tableauMur[i,j] == '1')
                            {
                                line.Replace(line.Substring(i, j+2), "1");
                            }

                        }
                    }
                }

                //close the file
                sw.Close();
                sr.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }




        }*/
    }
}
