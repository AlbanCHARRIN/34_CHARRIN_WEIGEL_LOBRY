using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;


public class Menu : GameScreen
{
    private Game _myGame;
    private SpriteBatch _spriteBatch { get; set; }

    private Texture2D _background;

    private Texture2D _logo;
    private Vector2 _positionLogo;


    private SpriteFont _font;
    private Vector2 _positionBoutonJouer;
    private Vector2 _positionBoutonOptions;
    private Vector2 _positionBoutonQuitter;


    public const int TAILLE_LOGO = 500;





    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1
    public Menu(Game game) : base(game)
    {
        _myGame = game;


    }
    public override void Initialize()
    {

        _positionBoutonJouer = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 120, 500);
        _positionBoutonOptions = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 130, 600);
        _positionBoutonQuitter = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 130, 700);

        _positionLogo = new Vector2((GraphicsDevice.DisplayMode.Width / 2) - (TAILLE_LOGO / 2), 0);
        base.Initialize();
    }


    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _background = Content.Load<Texture2D>("background");

        _logo = Content.Load<Texture2D>("Logo");

        _font = Content.Load<SpriteFont>("PixelFont");

        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
    }
    public override void Draw(GameTime gameTime)
    {

        _myGame.GraphicsDevice.Clear(Color.SaddleBrown);
        _spriteBatch.Begin();


        _spriteBatch.Draw(_background, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);

        _spriteBatch.Draw(_logo, _positionLogo, Microsoft.Xna.Framework.Color.White);

        _spriteBatch.DrawString(_font, $"Jouer", _positionBoutonJouer, Microsoft.Xna.Framework.Color.White);
        _spriteBatch.DrawString(_font, $"Options", _positionBoutonOptions, Microsoft.Xna.Framework.Color.White);



        _spriteBatch.End();

    }

}