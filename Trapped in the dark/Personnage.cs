using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

public class Personnage : GameScreen
{
    private Game _myGame;
    private SpriteBatch _spriteBatch { get; set; }
    private Vector2 _positionPerso;
    private AnimatedSprite _perso;
    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1
    public Personnage(Game game) : base(game)
    {
        _myGame = game;
    }
    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _positionPerso = new Vector2(1000,500);

        SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
        _perso = new AnimatedSprite(spriteSheet);

        base.LoadContent();
    }
    public override void Update(GameTime gameTime)
    {
        _perso.Play("left"); // une des animations définies dans « animation.sf »
        _perso.Update(gameTime);
    }
    public override void Draw(GameTime gameTime)
    {
        _myGame.GraphicsDevice.Clear(Color.Gray); // on utilise la reference vers
                                                  // Game1 pour chnager le graphisme
        _spriteBatch.Begin();
        _spriteBatch.Draw(_perso, _positionPerso);

        _spriteBatch.End();
    }
}