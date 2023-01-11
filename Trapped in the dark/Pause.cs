using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using Trapped_in_the_dark;
using System.Threading;

public class Pause : GameScreen
{

    private Texture2D _imagePause;
    private SpriteBatch _spriteBatch { get; set; }

    private Texture2D _parametres;
    private Vector2 _positionBoutonParametres;
    private Rectangle _recBoutonParametres;
    private Rectangle _recCroixParametres;
    private bool _parametresEtat;


    private Rectangle _recSonNull;
    private Rectangle _recSonBas;
    private Rectangle _recSonMoyen;
    private Rectangle _recSonHaut;
    private int _sonEtat;
    private Texture2D _carreHover;

    private Texture2D _rectangleHover;
    private Texture2D _rectangleHover2;

    private SpriteFont _font;

    private Texture2D _options;


    private Vector2 _positionBoutonJouer;
    private Rectangle _recBoutonJouer;

    private Vector2 _positionBoutonControles;
    private Rectangle _recBoutonControles;
    private Texture2D _controles;
    private bool _controlesEtat;

    private Rectangle _recCroixPleinEcran;
    private bool _pleinEcranEtat;
    private bool _fenetreEtat;

    private Rectangle _recCroixControles;

    private Vector2 _positionBoutonQuitter;
    private Rectangle _recBoutonQuitter;


    private MouseState _mouseState;
    private Rectangle _rSouris;

    private int _pauseEtat;

    private ScreenManager _screenManager;

    private Game1 _myGame;
    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1
    public Pause(Game1 game) : base(game)
    {
        _myGame = game;
    }
    public override void Initialize()
    {

       

        _recBoutonJouer = new Rectangle(1160, 210, 340, 40);
        _recBoutonQuitter = new Rectangle(1220, 690, 250, 50);
        _recBoutonControles = new Rectangle(1168, 350, 335, 50);
        _recBoutonParametres = new Rectangle(1216, 510, 235, 50);

        _recCroixParametres = new Rectangle(1048, 350, 40, 45);
        _recCroixPleinEcran = new Rectangle(928, 390, 38, 30);

        _recCroixControles = new Rectangle(346, 120, 55, 55);

        _recSonNull = new Rectangle(717, 455, 27, 34);
        _recSonBas = new Rectangle(810, 455, 40, 34);
        _recSonMoyen = new Rectangle(913, 455, 47, 34);
        _recSonHaut = new Rectangle(1005, 455, 55, 50);

        _screenManager = new ScreenManager();


        _myGame.Components.Add(_screenManager);



        base.Initialize();
    }
    public override void LoadContent()
    {

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _imagePause = Content.Load<Texture2D>("Pause");
        _font = Content.Load<SpriteFont>("PixelFont");

        _rectangleHover = Content.Load<Texture2D>("Carre");

        _rectangleHover2 = Content.Load<Texture2D>("Carre2");

        _parametres = Content.Load<Texture2D>("Parametres");

        _options = Content.Load<Texture2D>("Option");

        _controles = Content.Load<Texture2D>("Controles");

        _carreHover = Content.Load<Texture2D>("Carre3");

        


        base.LoadContent();
    }
    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


        KeyboardState keyboardState = Keyboard.GetState();

        //permet d'avoir la position de la souris et de pouvoir créer des Intersect
        _mouseState = Mouse.GetState();
        _rSouris.X = _mouseState.X;
        _rSouris.Y = _mouseState.Y;

        if (keyboardState.IsKeyDown(Keys.Space))
        {
            _pauseEtat = 1;
        }

        if (_pauseEtat == 1)
        {
            //les intersections de tous les boutons du menu
            if (_rSouris.Intersects(_recBoutonQuitter))


                if (_mouseState.LeftButton == ButtonState.Pressed)
                {
                    _myGame.Etat = Game1.Etats.Quit;

                }

            if (_rSouris.Intersects(_recBoutonJouer))
            {

                if (_mouseState.LeftButton == ButtonState.Pressed)
                {

                    _pauseEtat = 0;
                }
            }
            if (_rSouris.Intersects(_recBoutonControles))
            {

                if (_mouseState.LeftButton == ButtonState.Pressed)
                {

                    _controlesEtat = true;

                }
            }
            if (_controlesEtat == true)
            {
                if (_rSouris.Intersects(_recCroixControles))
                {

                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {

                        _controlesEtat = false;

                    }
                }
            }
            if (_rSouris.Intersects(_recBoutonParametres))
            {

                if (_mouseState.LeftButton == ButtonState.Pressed)
                {

                    _parametresEtat = true;

                }
            }
            if (_parametresEtat == true)
            {
                if (_rSouris.Intersects(_recCroixParametres))
                {

                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {

                        _parametresEtat = false;
                    }
                }
            }
            if (_parametresEtat == true)
            {
                if (_rSouris.Intersects(_recCroixPleinEcran))
                {
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {

                        if (_pleinEcranEtat == true)
                        {
                            _fenetreEtat = true;
                            Thread.Sleep(200);
                        }
                        if (_pleinEcranEtat == false)
                        {
                            _fenetreEtat = false;

                        }

                    }
                }
            }
            if (_fenetreEtat == true)
            {
                _myGame.Option = Game1.Options.PleinEcran;
                _pleinEcranEtat = false;

            }


            if (_fenetreEtat == false)
            {
                _myGame.Option = Game1.Options.Fenetre;
                _pleinEcranEtat = true;

            }

            if (_parametresEtat)
            {
                if (_rSouris.Intersects(_recSonNull))
                {
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        MediaPlayer.Volume = 0f;
                        _sonEtat = 1;
                    }
                }
            }
            if (_parametresEtat)
            {
                if (_rSouris.Intersects(_recSonBas))
                {
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        MediaPlayer.Volume = 0.25f;
                        _sonEtat = 2;
                    }
                }
            }
            if (_parametresEtat)
            {
                if (_rSouris.Intersects(_recSonMoyen))
                {
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        MediaPlayer.Volume = 0.75f;
                        _sonEtat = 3;
                    }
                }
            }
            if (_parametresEtat)
            {
                if (_rSouris.Intersects(_recSonHaut))
                {
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        MediaPlayer.Volume = 1f;
                        _sonEtat = 4;
                    }
                }
            }
        }



        

         

    }
    public override void Draw(GameTime gameTime)
    {
        _myGame.GraphicsDevice.Clear(Color.DarkGray); // on utilise la reference vers  Game1 pour changer le graphisme

        _spriteBatch.Begin();

        if ( _pauseEtat == 1)
        {
            _spriteBatch.Draw(_imagePause, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);
            
            

            


            //dessine des contours autours des textes
            if (_rSouris.Intersects(_recBoutonQuitter))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2(1138, 660), Microsoft.Xna.Framework.Color.White);
            }
            if (_rSouris.Intersects(_recBoutonJouer))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2(1130, 180), Microsoft.Xna.Framework.Color.White);
            }
            if (_rSouris.Intersects(_recBoutonControles))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2(1134, 325), Microsoft.Xna.Framework.Color.White);
            }
            if (_rSouris.Intersects(_recBoutonParametres))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2(1134, 485), Microsoft.Xna.Framework.Color.White);
            }

            if (_controlesEtat == true)
            {
                _spriteBatch.Draw(_controles, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 470, 100), Microsoft.Xna.Framework.Color.White);
            }

            if (_parametresEtat == true)
            {
                _spriteBatch.Draw(_options, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 1000, -100), Microsoft.Xna.Framework.Color.White);

                if (_fenetreEtat == true)
                {
                    _spriteBatch.DrawString(_font, "X", new Vector2(928, 364), Microsoft.Xna.Framework.Color.Black);
                }
                if (_sonEtat == 1)
                    _spriteBatch.Draw(_carreHover, new Vector2(701, 445), Microsoft.Xna.Framework.Color.White);
                if (_sonEtat == 2)
                    _spriteBatch.Draw(_carreHover, new Vector2(800, 445), Microsoft.Xna.Framework.Color.White);
                if (_sonEtat == 3)
                    _spriteBatch.Draw(_carreHover, new Vector2(907, 445), Microsoft.Xna.Framework.Color.White);
                if (_sonEtat == 4)
                    _spriteBatch.Draw(_carreHover, new Vector2(1002, 445), Microsoft.Xna.Framework.Color.White);

            }

        }




        _spriteBatch.End();


    }
}