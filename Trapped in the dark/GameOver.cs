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

public class GameOver : GameScreen
{

    private SpriteBatch _spriteBatch { get; set; }


    private Texture2D _imageGameOver;
    private SpriteFont _font;

    private MouseState _mouseState;
    private Rectangle _rSouris;



    private ScreenManager _screenManager;

    private Game1 _myGame;
    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1
    public GameOver(Game1 game) : base(game)
    {
        _myGame = game;
    }
    public override void Initialize()
    {



        

        _screenManager = new ScreenManager();


        _myGame.Components.Add(_screenManager);



        base.Initialize();
    }
    public override void LoadContent()
    {

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _imageGameOver = Content.Load<Texture2D>("GAME OVER");

        _font = Content.Load<SpriteFont>("PixelFont");


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
            _myGame.Etat = Game1.Etats.Quit;
        }

        







    }
    public override void Draw(GameTime gameTime)
    {
        _myGame.GraphicsDevice.Clear(Color.DarkRed); // on utilise la reference vers  Game1 pour changer le graphisme

        _spriteBatch.Begin();


        _spriteBatch.Draw(_imageGameOver, new Vector2( 0,0), Microsoft.Xna.Framework.Color.White);

        _spriteBatch.DrawString(_font, "Appuyer sur espace pour quitter", new Vector2((GraphicsDevice.DisplayMode.Width / 2)-700, 200), Microsoft.Xna.Framework.Color.White);



        _spriteBatch.End();


    }
}