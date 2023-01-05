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
    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1

    //constante
    const int _width = 37;
    const int _height = 37;

    //Collisions
    int _collision = 1;
    //Joueur Rouge
    private Vector2 _positionPersoRed;
    private AnimatedSprite _persoRed;

    private double accelerationRed = 1;
    private double positionPersoRedX = 0;
    private double positionPersoRedY = 0;

    //Coeur 
    private AnimatedSprite _CoeurRed;
    int vieRed;
    private Vector2 _positionCoeurRed;

    //Joueur Bleu
    private Vector2 _positionPersoBlue;
    private AnimatedSprite _persoBlue;

    private double accelerationBlue = 1;
    private double positionPersoBlueX = 0;
    private double positionPersoBlueY = 0;

    //Coeur 
    private AnimatedSprite _CoeurBlue;
    int vieBlue;
    private Vector2 _positionCoeurBlue;


    public Personnage(Game game) : base(game)
    {
        _myGame = game;
    }
    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);


        //Joueur Rouge
        _positionPersoRed = new Vector2(1000,500);

        SpriteSheet spriteSheetRed = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
        _persoRed = new AnimatedSprite(spriteSheetRed);

        //Coeur
        SpriteSheet spriteSheetCoeurRed = Content.Load<SpriteSheet>("coeurPerso.sf", new JsonContentLoader());
        _CoeurRed = new AnimatedSprite(spriteSheetCoeurRed);
        vieRed = 3;
        _positionCoeurRed = _positionPersoRed + new Vector2(0, -20);

        //Joueur Bleu
        _positionPersoBlue = new Vector2(500, 500);

        SpriteSheet spriteSheetBlue = Content.Load<SpriteSheet>("persoPrincipaleAnimation.sf", new JsonContentLoader());
        _persoBlue = new AnimatedSprite(spriteSheetBlue);

        //Coeur
        SpriteSheet spriteSheetCoeurBlue = Content.Load<SpriteSheet>("coeurPersoPrincipale.sf", new JsonContentLoader());
        _CoeurBlue = new AnimatedSprite(spriteSheetCoeurBlue);
        vieBlue = 3;
        _positionCoeurBlue = _positionPersoBlue + new Vector2(0, -20);

        base.LoadContent();
    }
    public override void Update(GameTime gameTime)
    {
        //variables personnage rouge
        positionPersoRedY = 0;
        positionPersoRedX = 0;

        //variables personnage bleu
        positionPersoBlueY = 0;
        positionPersoBlueX = 0;

        // Colisions

        //collisions avec décor infligeant des dégats

        //collisions avec décor

        //Colisions entre joueur

        //coin haut droit
        if (_positionPersoBlue.X + _width >= _positionPersoRed.X && _positionPersoBlue.X + _width <= _positionPersoRed.X + _width && _positionPersoBlue.Y >= _positionPersoRed.Y && _positionPersoBlue.Y <= _positionPersoRed.Y + _height)
        {
            _collision = 0;
            _positionPersoBlue += new Vector2(-10,10);
            _positionPersoRed += new Vector2(10,-10);
            vieRed -= 1;
            vieBlue -= 1;
        }
        if (_positionPersoRed.X + _width >= _positionPersoBlue.X && _positionPersoRed.X + _width <= _positionPersoBlue.X + _width && _positionPersoRed.Y >= _positionPersoBlue.Y && _positionPersoRed.Y <= _positionPersoBlue.Y + _height)
        {
            _collision = 0;
            _positionPersoBlue += new Vector2(10, -10);
            _positionPersoRed += new Vector2(-10, 10);
        }

        //coin haut gauche
        if (_positionPersoBlue.X >= _positionPersoRed.X && _positionPersoBlue.X <= _positionPersoRed.X + _width && _positionPersoBlue.Y >= _positionPersoRed.Y && _positionPersoBlue.Y <= _positionPersoRed.Y + _height)
        {
            _collision = 0;
            _positionPersoBlue += new Vector2(10, 10);
            _positionPersoRed += new Vector2(-10, -10);
        }
        if (_positionPersoRed.X >= _positionPersoBlue.X && _positionPersoRed.X <= _positionPersoBlue.X + _width && _positionPersoRed.Y >= _positionPersoBlue.Y && _positionPersoRed.Y <= _positionPersoBlue.Y + _height)
        {
            _collision = 0;
            _positionPersoBlue += new Vector2(-10, -10);
            _positionPersoRed += new Vector2(10, 10);
        }

        //coin bas droit
        if (_positionPersoBlue.X + _width >= _positionPersoRed.X && _positionPersoBlue.X + _width <= _positionPersoRed.X + _width && _positionPersoBlue.Y + _height >= _positionPersoRed.Y && _positionPersoBlue.Y + _height <= _positionPersoRed.Y + _height)
        {
            _collision = 0;
            _positionPersoBlue += new Vector2(-10, -10);
            _positionPersoRed += new Vector2(10, 10);
        }
        if (_positionPersoRed.X + _width >= _positionPersoBlue.X && _positionPersoRed.X + _width <= _positionPersoBlue.X + _width && _positionPersoRed.Y + _height >= _positionPersoBlue.Y && _positionPersoRed.Y + _height <= _positionPersoBlue.Y + _height)
        {
            _collision = 0;
            _positionPersoBlue += new Vector2(10, 10);
            _positionPersoRed += new Vector2(-10, -10);
        }

        //coin bas gauche
        if (_positionPersoBlue.X >= _positionPersoRed.X && _positionPersoBlue.X <= _positionPersoRed.X + _width && _positionPersoBlue.Y + _height >= _positionPersoRed.Y && _positionPersoBlue.Y + _height <= _positionPersoRed.Y + _height)
        {
            _collision = 0;
            _positionPersoBlue += new Vector2(10, -10);
            _positionPersoRed += new Vector2(-10, 10);
        }
        if (_positionPersoRed.X >= _positionPersoBlue.X && _positionPersoRed.X <= _positionPersoBlue.X + _width && _positionPersoRed.Y + _height >= _positionPersoBlue.Y && _positionPersoRed.Y + _height <= _positionPersoBlue.Y + _height)
        {
            _collision = 0;
            _positionPersoBlue += new Vector2(-10, 10);
            _positionPersoRed += new Vector2(10, -10);
        }

        else
        {
            _collision = 1;
        }


        KeyboardState keyboardState = Keyboard.GetState();
        
        //Joueur Rouge

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            _persoRed.Play("left");
            positionPersoRedY = 0;
            positionPersoRedX = -10;

        }

        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            _persoRed.Play("right");
            positionPersoRedY = 0;
            positionPersoRedX = 10;
        }

        else if (keyboardState.IsKeyDown(Keys.Up))
        {
            _persoRed.Play("up");
            positionPersoRedY = -10;
            positionPersoRedX = 0;
        }
        else if (keyboardState.IsKeyDown(Keys.Down))
        {
            _persoRed.Play("down");
            positionPersoRedY = 10;
            positionPersoRedX = 0;
        }
        else
        {
            _persoRed.Play("idle");
        }

        if (keyboardState.IsKeyDown(Keys.NumPad1))
        {
            accelerationRed = 1.5;
        }
        else if (keyboardState.IsKeyUp(Keys.NumPad1))
        {
            accelerationRed = 1;
        }


        _persoRed.Update(gameTime);

        _positionPersoRed += new Vector2((float)accelerationRed * (float)positionPersoRedX * _collision, (float)accelerationRed * (float)positionPersoRedY * _collision);

        //Joueur Bleu

        positionPersoBlueY = 0;
        positionPersoBlueX = 0;

        if (keyboardState.IsKeyDown(Keys.Q))
        {
            _persoBlue.Play("left");
            positionPersoBlueY = 0;
            positionPersoBlueX = -10;

        }

        else if (keyboardState.IsKeyDown(Keys.D))
        {
            _persoBlue.Play("right");
            positionPersoBlueY = 0;
            positionPersoBlueX = 10;
        }

        else if (keyboardState.IsKeyDown(Keys.Z))
        {
            _persoBlue.Play("up");
            positionPersoBlueY = -10;
            positionPersoBlueX = 0;
        }
        else if (keyboardState.IsKeyDown(Keys.S))
        {
            _persoBlue.Play("down");
            positionPersoBlueY = 10;
            positionPersoBlueX = 0;
        }
        else
        {
            _persoBlue.Play("idle");
        }

        if (keyboardState.IsKeyDown(Keys.LeftShift))
        {
            accelerationBlue = 1.5;
        }
        else if (keyboardState.IsKeyUp(Keys.LeftShift))
        {
            accelerationBlue = 1;
        }


        _persoBlue.Update(gameTime);

        _positionPersoBlue += new Vector2((float)accelerationBlue * (float)positionPersoBlueX * _collision, (float)accelerationBlue * (float)positionPersoBlueY * _collision);


        _positionCoeurRed = _positionPersoRed + new Vector2(0, -20);
        _positionCoeurBlue = _positionPersoBlue + new Vector2(0, -20);


    }
    public override void Draw(GameTime gameTime)
    {
        _myGame.GraphicsDevice.Clear(Color.Gray); // on utilise la reference vers
                                                  // Game1 pour chnager le graphisme
        _spriteBatch.Begin();
        _spriteBatch.Draw(_persoRed, _positionPersoRed);
        _spriteBatch.Draw(_persoBlue, _positionPersoBlue);
        _spriteBatch.Draw(_CoeurBlue, _positionCoeurBlue);
        _spriteBatch.Draw(_CoeurRed, _positionCoeurRed);

        _spriteBatch.End();
    }
}