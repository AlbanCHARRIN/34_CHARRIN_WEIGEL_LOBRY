using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;

public class Personnage : GameScreen
{
    private Game _myGame;
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
        _positionPerso = new Vector2(400,400);

        SpriteSheet spriteSheet = Content.Load<SpriteSheet>("animation.sf", new JsonContentLoader());
        _perso = new AnimatedSprite(spriteSheet);

        base.LoadContent();
    }
    public override void Update(GameTime gameTime)
    {
        
    }
    public override void Draw(GameTime gameTime)
    {
        _myGame.GraphicsDevice.Clear(Color.Gray); // on utilise la reference vers
                                                         // Game1 pour chnager le graphisme
    }
}