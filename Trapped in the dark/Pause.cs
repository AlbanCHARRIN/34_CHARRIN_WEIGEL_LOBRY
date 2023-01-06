using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;


public class Pause : GameScreen
{
    private Game _myGame;
    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1
    public Pause(Game game) : base(game)
    {
        _myGame = game;
    }
    public override void LoadContent()
    {
        base.LoadContent();
    }
    public override void Update(GameTime gameTime)
    { }
    public override void Draw(GameTime gameTime)
    {
        _myGame.GraphicsDevice.Clear(Color.DarkGray); // on utilise la reference vers
                                                         // Game1 pour changer le graphisme
    }
}