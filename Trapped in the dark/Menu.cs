using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

    private Texture2D _rectangleHover;


    private SpriteFont _font;

    
    private Vector2 _positionBoutonJouer;
    private Rectangle _recBoutonJouer;

    private Vector2 _positionBoutonControles;
    private Rectangle _recBoutonControles;

    private Vector2 _positionBoutonQuitter;
    private Rectangle _recBoutonQuitter;


   
    
   
    


    private MouseState _mouseState;
    private Rectangle _rSouris;


    private ScreenManager _screenManager;


    private int etatMouvement = 0;


    public const int TAILLE_LOGO = 500;

    public const int POSISTION_BOUTON_JOUER_CENTRER = 115;
    public const int POSISTION_RECTANGLE_JOUER_CENTRER = 120;

    public const int POSISTION_BOUTON_CONTROLES_CENTRER = 207;

    public const int POSISTION_BOUTON_QUITTER_CENTRER = 141;
    public const int POSISTION_RECTANGLE_QUITTER_CENTRER = 150;



    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1
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
        _recBoutonQuitter = new Rectangle((GraphicsDevice.DisplayMode.Width / 2) - POSISTION_RECTANGLE_QUITTER_CENTRER, 700, 300, 70);


        _positionLogo = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - (TAILLE_LOGO / 2), 0);

        _positionBoutonParametres = new Vector2((GraphicsDevice.DisplayMode.Width )-200, 0);
        _recBoutonParametres = new Rectangle((GraphicsDevice.DisplayMode.Width) - 200, 0, 200, 160);

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

        _rectangleHover = Content.Load <Texture2D>("Carre");

        _parametres = Content.Load<Texture2D>("Parametres");


        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {


        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;



        _mouseState = Mouse.GetState();
        _rSouris.X = _mouseState.X;
        _rSouris.Y = _mouseState.Y;




        if (_positionLogo.Y < 0)
        {
            etatMouvement = 0;
        }
        if (_positionLogo.Y > 20)
            etatMouvement = 1;



        if (etatMouvement == 0)
        {
            _positionLogo.Y = _positionLogo.Y + (float)0.3;

        }




        if (etatMouvement == 1)
            _positionLogo.Y = _positionLogo.Y - (float)0.3;





        if (_rSouris.Intersects(_recBoutonQuitter))


            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                _myGame.Etat = Game1.Etats.Quit;

            }

        if (_rSouris.Intersects(_recBoutonJouer))
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {

                _myGame.Etat = Game1.Etats.Play;
            }
        if (_rSouris.Intersects(_recBoutonParametres))
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {

                _myGame.Etat = Game1.Etats.Play;
            }


    }
    public override void Draw(GameTime gameTime)
    {
        {

            _myGame.GraphicsDevice.Clear(Color.SaddleBrown);
            _spriteBatch.Begin();


            _spriteBatch.Draw(_background, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.Draw(_logo, _positionLogo, Microsoft.Xna.Framework.Color.White);

            _spriteBatch.DrawString(_font, "Jouer", _positionBoutonJouer, Microsoft.Xna.Framework.Color.White);
            _spriteBatch.DrawString(_font, "Controles", _positionBoutonControles, Microsoft.Xna.Framework.Color.White);
            _spriteBatch.DrawString(_font, "Quitter", _positionBoutonQuitter, Microsoft.Xna.Framework.Color.White);


            if (_rSouris.Intersects(_recBoutonQuitter))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2((GraphicsDevice.DisplayMode.Width / 2)-200, 695), Microsoft.Xna.Framework.Color.White);
            }
            if (_rSouris.Intersects(_recBoutonJouer))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 200, 495), Microsoft.Xna.Framework.Color.White);
            }

            _spriteBatch.Draw(_parametres, new Vector2((GraphicsDevice.DisplayMode.Width )-200,0), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();

        }

    }
}
