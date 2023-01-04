using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;


public class Menu : GameScreen
{
    private Game _myGame;
    private SpriteBatch _spriteBatch;
    private Texture2D _background;

    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1
    public Menu(Game game) : base(game)
    {
        _myGame = game;

    }
    public override void LoadContent()
    {
        _background = Content.Load<Texture2D>("background");
        base.LoadContent();
    }
    public override void Update(GameTime gameTime)
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }
    public override void Draw(GameTime gameTime)
    {

        _myGame.GraphicsDevice.Clear(Color.SaddleBrown);
        _spriteBatch.Begin();


        _spriteBatch.Draw(_background, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);

        _spriteBatch.End();

    }

}