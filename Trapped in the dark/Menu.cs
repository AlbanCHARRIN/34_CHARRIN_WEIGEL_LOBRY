using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;

using MonoGame.Extended.Screens.Transitions;
using System;
using System.Threading;
using Trapped_in_the_dark;

public class Menu : GameScreen
{
    private Game1 _myGame;
    private SpriteBatch _spriteBatch { get; set; }

    private Texture2D _background;


    private Texture2D _logo;
    private Vector2 _positionLogo;


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


    private ScreenManager _screenManager;

    private Song _sonRect;
    private Song _sonMenu;


    private int etatMouvement = 0;
    private int _musiqueEtat = 0;

    public const int TAILLE_LOGO = 500;

    public const int POSISTION_BOUTON_JOUER_CENTRER = 115;
    public const int POSISTION_RECTANGLE_JOUER_CENTRER = 120;

    public const int POSISTION_BOUTON_CONTROLES_CENTRER = 207;

    public const int POSISTION_BOUTON_QUITTER_CENTRER = 141;
    public const int POSISTION_RECTANGLE_QUITTER_CENTRER = 150;



    public Menu(Game1 game) : base(game)
    {

        _myGame = game;
    }
    public override void Initialize()
    {

        _positionBoutonJouer = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - POSISTION_BOUTON_JOUER_CENTRER, 500);
        _positionBoutonControles = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - POSISTION_BOUTON_CONTROLES_CENTRER, 600);
        _positionBoutonQuitter = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - POSISTION_BOUTON_QUITTER_CENTRER, 700);

        _recBoutonJouer = new Rectangle((GraphicsDevice.DisplayMode.Width / 2) - POSISTION_RECTANGLE_JOUER_CENTRER, 515, 245, 50);
        _recBoutonQuitter = new Rectangle((GraphicsDevice.DisplayMode.Width / 2) - POSISTION_RECTANGLE_QUITTER_CENTRER, 720, 300, 50);
        _recBoutonControles = new Rectangle((GraphicsDevice.DisplayMode.Width / 2) - 210, 620, 425, 50);

        _positionLogo = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - (TAILLE_LOGO / 2), 0);

        _positionBoutonParametres = new Vector2((GraphicsDevice.DisplayMode.Width) - 200, 0);
        _recBoutonParametres = new Rectangle((GraphicsDevice.DisplayMode.Width) - 200, 0, 200, 160);
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

        _background = Content.Load<Texture2D>("background2");

        _logo = Content.Load<Texture2D>("Logo");

        _font = Content.Load<SpriteFont>("PixelFont");

        _rectangleHover = Content.Load<Texture2D>("Carre");

        _rectangleHover2 = Content.Load<Texture2D>("Carre2");

        _parametres = Content.Load<Texture2D>("Parametres");

        _sonRect = Content.Load<Song>("Whoosh");

        _options = Content.Load<Texture2D>("Option");

        _controles = Content.Load<Texture2D>("Controles");

        _carreHover = Content.Load<Texture2D>("Carre3");

        _sonMenu = Content.Load<Song>("Myuu-HoldOn");

        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {


        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


      

        //permet d'avoir la position de la souris et de pouvoir créer des Intersect
        _mouseState = Mouse.GetState();
        _rSouris.X = _mouseState.X;
        _rSouris.Y = _mouseState.Y;


        if(_musiqueEtat == 0)
        {
            MediaPlayer.Play(_sonMenu);
            _musiqueEtat++;
        }

        //permet de faire bouger le logo

        if (_positionLogo.Y < 0)
        {
            etatMouvement = 0;
        }
        if (_positionLogo.Y > 20)
            etatMouvement = 1;


        if (etatMouvement == 0)
            _positionLogo.Y = _positionLogo.Y + (float)0.3;

        if (etatMouvement == 1)
            _positionLogo.Y = _positionLogo.Y - (float)0.3;




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
                
                _myGame.Etat = Game1.Etats.Play;
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
    public override void Draw(GameTime gameTime)
    {
        {

            _myGame.GraphicsDevice.Clear(Color.SaddleBrown);
            _spriteBatch.Begin();

            //dessines les textes, logos et symboles
            _spriteBatch.Draw(_background, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);
            _spriteBatch.Draw(_logo, _positionLogo, Microsoft.Xna.Framework.Color.White);

            _spriteBatch.DrawString(_font, "Jouer", _positionBoutonJouer, Microsoft.Xna.Framework.Color.White);
            _spriteBatch.DrawString(_font, "Controles", _positionBoutonControles, Microsoft.Xna.Framework.Color.White);
            _spriteBatch.DrawString(_font, "Quitter", _positionBoutonQuitter, Microsoft.Xna.Framework.Color.White);

            _spriteBatch.Draw(_parametres, new Vector2((GraphicsDevice.DisplayMode.Width) - 200, 0), Microsoft.Xna.Framework.Color.White);


            //dessine des contours autours des textes
            if (_rSouris.Intersects(_recBoutonQuitter))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 200, 695), Microsoft.Xna.Framework.Color.White);
            }
            if (_rSouris.Intersects(_recBoutonJouer))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 200, 495), Microsoft.Xna.Framework.Color.White);
            }
            if (_rSouris.Intersects(_recBoutonControles))
            {
                _spriteBatch.Draw(_rectangleHover2, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 245, 595), Microsoft.Xna.Framework.Color.White);
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
                    _spriteBatch.Draw(_carreHover, new Vector2(701,445), Microsoft.Xna.Framework.Color.White);
                if (_sonEtat == 2)
                    _spriteBatch.Draw(_carreHover, new Vector2(800, 445), Microsoft.Xna.Framework.Color.White);
                if (_sonEtat == 3)
                    _spriteBatch.Draw(_carreHover, new Vector2(907, 445), Microsoft.Xna.Framework.Color.White);
                if (_sonEtat == 4)
                    _spriteBatch.Draw(_carreHover, new Vector2(1002, 445), Microsoft.Xna.Framework.Color.White);

            }
                
                

            _spriteBatch.End();

        }

    }

}
