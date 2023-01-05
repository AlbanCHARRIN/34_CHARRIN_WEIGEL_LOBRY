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
    private double acceleration = 1;
    private double positionPersoX = 0;
    private double positionPersoY = 0;

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
        KeyboardState keyboardState = Keyboard.GetState();

        positionPersoX = 0;
        positionPersoY = 0;

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            _perso.Play("left");
            positionPersoX = 0;
            positionPersoY = -10;

        }

        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            _perso.Play("right");
            positionPersoX = 0;
            positionPersoY = 10;
        }

        else if (keyboardState.IsKeyDown(Keys.Up))
        {
            _perso.Play("up");
            positionPersoX = -10;
            positionPersoY = 0;
        }
        else if (keyboardState.IsKeyDown(Keys.Down))
        {
            _perso.Play("down");
            positionPersoX = 10;
            positionPersoY = 0;
        }
        else
        {
            _perso.Play("idle");
        }

        if (keyboardState.IsKeyDown(Keys.LeftShift))
        {
            acceleration = 1.5;
        }
        else if (keyboardState.IsKeyUp(Keys.LeftShift))
        {
            acceleration = 1;
        }


        _perso.Update(gameTime);

        _positionPerso += new Vector2((float)acceleration * (float)positionPersoY, (float)acceleration * (float)positionPersoX);
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