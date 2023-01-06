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


    private SpriteFont _font;
    private Vector2 _positionBoutonJouer;
    private Vector2 _positionBoutonOptions;
    private Vector2 _positionBoutonQuitter;
    private Rectangle _recBoutonQuitter;

    private MouseState _mouseState;
    private Rectangle _rSouris;

    private Personnage _personnage;
    private ScreenManager _screenManager;


   


    public const int TAILLE_LOGO = 500;

    



    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1
    public Menu(Game1 game) : base(game)
    {
        _myGame = game;
    }
    public override void Initialize()
    {

        _positionBoutonJouer = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 120, 500);
        _positionBoutonOptions = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 130, 600);
        _positionBoutonQuitter = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 130, 700);
        _recBoutonQuitter = new Rectangle(0, 0, 50, 50);

        _positionLogo = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - (TAILLE_LOGO / 2), 0);

        _screenManager = new ScreenManager();
        _myGame.Components.Add(_screenManager);

     

        base.Initialize();
    }


    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _background = Content.Load<Texture2D>("background");

        _logo = Content.Load<Texture2D>("Logo");

        _font = Content.Load<SpriteFont>("PixelFont");

        //_personnage = new Personnage(this);


        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {


        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;



        _mouseState = Mouse.GetState();
        _rSouris.X = _mouseState.X;
        _rSouris.Y = _mouseState.Y;

        if (_positionLogo.Y >= 0 && _positionLogo.Y < 20)
            _positionLogo.Y = _positionLogo.Y + 1;
      
        
        if (_rSouris.Intersects(_recBoutonQuitter))
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {

                _myGame.Etat = Game1.Etats.Play;
            }
        //_myGame.Exit();

    }
    public override void Draw(GameTime gameTime)
    {
        {

            _myGame.GraphicsDevice.Clear(Color.SaddleBrown);
            _spriteBatch.Begin();


            _spriteBatch.Draw(_background, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.Draw(_logo, _positionLogo, Microsoft.Xna.Framework.Color.White);

            _spriteBatch.DrawString(_font, "Jouer", _positionBoutonJouer, Microsoft.Xna.Framework.Color.White);
            _spriteBatch.DrawString(_font, "Options", _positionBoutonOptions, Microsoft.Xna.Framework.Color.White);
            _spriteBatch.DrawString(_font, "Quitter", _positionBoutonQuitter, Microsoft.Xna.Framework.Color.White);


            _spriteBatch.End();

        }

    }
}
