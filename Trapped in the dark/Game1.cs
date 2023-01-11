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
        private Map1 _map1;
        private Map2 _map2;
        private Map3 _map3;
        private Map4 _map4;
        private Map5 _map5;
        private Map6 _map6;

        public GraphicsDeviceManager _graphics;
        public int[,] _tableauMur;
        public int dimensionx = Case2.dimensionx;
        public int dimensiony = Case2.dimensiony;

        //Map

        public enum Etats { Menu, Controls, Play, Quit };

        private Etats etat;

        public enum Options{ PleinEcran, Son, Fenetre}
        private Options option;



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
        public Options Option
        {
            get { return this.option; }
            set { this.option = value; }
        }

        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            _menu = new Menu(this);
            _pause = new Pause(this);
            _personnage = new Personnage(this);
            _map1 = new Map1(this);
            _map2 = new Map2(this);
            _map3 = new Map3(this);
            _map4 = new Map4(this);
            _map5 = new Map5(this);
            _map6 = new Map6(this);
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
            Option = Options.Fenetre;

            _tableauMur = Case2.GenerateurDuTileset();
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

            Random random = new Random();
            int randomMap = random.Next(1, 7);
            int[] tabMap = new int[6] {0,0,0,0,0,0};

            for ( int i = 0; i < tabMap.Length; i++)
            {
                if (tabMap[i] == i+1 )
                    random.Next(1, 7);
            }
            //mouseState.LeftButton == ButtonState.Pressed
            if (keyboardState.IsKeyDown(Keys.F6))
            {
                if (this.Etat == Etats.Play)
                    //_screenManager.LoadScreen(_personnage, new FadeTransition(GraphicsDevice, Color.Black));
                    if(randomMap == 1)
                    {
                        tabMap[0] = 1;
                        _screenManager.LoadScreen(_map1, new FadeTransition(GraphicsDevice, Color.Black));
                        
                    }
                    if (randomMap == 2)
                    {
                    tabMap[1] = 2;
                    _screenManager.LoadScreen(_map2, new FadeTransition(GraphicsDevice, Color.Black));
                    }
                if (randomMap == 3)
                {
                    tabMap[2] = 3;
                    _screenManager.LoadScreen(_map3, new FadeTransition(GraphicsDevice, Color.Black));
                }
                if (randomMap == 4)
                {
                    tabMap[3] = 4;
                    _screenManager.LoadScreen(_map4, new FadeTransition(GraphicsDevice, Color.Black));
                }
                if (randomMap == 5)
                {
                    tabMap[4] = 5;
                    _screenManager.LoadScreen(_map5, new FadeTransition(GraphicsDevice, Color.Black));
                }
                if (randomMap == 6)
                {
                    tabMap[5] = 6;
                    _screenManager.LoadScreen(_map6, new FadeTransition(GraphicsDevice, Color.Black));
                }


                else if (this.Etat == Etats.Quit)
                    Exit();

            }
            
                if (this.Option == Options.PleinEcran)
                {
                    _graphics.IsFullScreen = true;
                    _graphics.ApplyChanges();
                }
                 if (this.Option == Options.Fenetre)
                    _graphics.IsFullScreen = false;
                    _graphics.ApplyChanges();
           

            if (keyboardState.IsKeyDown(Keys.F2))
            {
                _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice,
                Color.Black));
            }

            else if (keyboardState.IsKeyDown(Keys.F3))
            {
                _screenManager.LoadScreen(_map1, new FadeTransition(GraphicsDevice,
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
