﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using Trapped_in_the_dark;

public class Personnage : GameScreen
{
    private Game _myGame;
    private SpriteBatch _spriteBatch { get; set; }
    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1

    // Map
    public TiledMap _tiledMap;
    public TiledMapRenderer _tiledMapRenderer;
    public TiledMapTileLayer mapLayer;
    public TiledMapTileset _tileset;
    private Vector2 _positionMap;



    //constante
    const int _width = 37;
    const int _height = 37;

    /*Fond noir
    private Texture2D _fondNoirBlue;
    private Vector2 _positionFondBlue;

    private Texture2D _fondNoirRed;
    private Vector2 _positionFondRed;*/

    //Collisions
    bool _collision = false;
    string _directioncollisonRed;
    string _directioncollisionBlue;
    Vector2 _collisionVectorPersonnageRed;
    Vector2 _collisionVectorPersonnageBlue;
    Vector2 _collisionVectorRed;
    Vector2 _collisionVectorBlue;

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

    private ScreenManager _screenManager;
    private Texture2D _pause;
    private int _pauseEtat = 0;
    private Texture2D _rectangleHover;

    private Rectangle _recBoutonReprendre;
    private Rectangle _recBoutonOptions;
    private Rectangle _recBoutonControles;
    private Rectangle _recBoutonQuitter;

    private MouseState _mouseState;
    private Rectangle _rSouris;


    public Personnage(Game game) : base(game)
    {
        _myGame = game;
    }
    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);


        // colision

        _directioncollisonRed = "idle";
        _directioncollisionBlue = "idle";
        //tileMap
        _tiledMap = Content.Load<TiledMap>("Map1");
        _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

        GraphicsDevice.BlendState = BlendState.AlphaBlend;

        mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacle");
        
        //Joueur Rouge
        _positionPersoRed = new Vector2(400,500);

        SpriteSheet spriteSheetRed = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
        _persoRed = new AnimatedSprite(spriteSheetRed);

        //Coeur
        SpriteSheet spriteSheetCoeurRed = Content.Load<SpriteSheet>("coeurPerso.sf", new JsonContentLoader());
        _CoeurRed = new AnimatedSprite(spriteSheetCoeurRed);
        vieRed = 3;
        _positionCoeurRed = _positionPersoRed + new Vector2(0, -20);

        //Joueur Bleu
        _positionPersoBlue = new Vector2(450, 500);

        SpriteSheet spriteSheetBlue = Content.Load<SpriteSheet>("persoPrincipaleAnimation.sf", new JsonContentLoader());
        _persoBlue = new AnimatedSprite(spriteSheetBlue);

        //Coeur
        SpriteSheet spriteSheetCoeurBlue = Content.Load<SpriteSheet>("coeurPersoPrincipale.sf", new JsonContentLoader());
        _CoeurBlue = new AnimatedSprite(spriteSheetCoeurBlue);
        vieBlue = 3;
        _positionCoeurBlue = _positionPersoBlue + new Vector2(0, -20);

        /*Fond noir
        _fondNoirBlue = Content.Load<Texture2D>("New Piskel(11)");
        _positionFondBlue = _positionPersoBlue + new Vector2(-_fondNoirBlue.Width / 2 + 10, -_fondNoirBlue.Height / 2 + 10);

        _fondNoirRed = Content.Load<Texture2D>("New Piskel(11)");
        _positionFondRed = _positionPersoRed + new Vector2(-_fondNoirRed.Width / 2 + 10, -_fondNoirRed.Height / 2 + 10);*/

        _screenManager = new ScreenManager();
        _myGame.Components.Add(_screenManager);

        _pause = Content.Load<Texture2D>("Pause");
        _rectangleHover = Content.Load<Texture2D>("Carre");

        _recBoutonReprendre = new Rectangle(2000, 515, 245, 50);
        _recBoutonQuitter = new Rectangle(2000, 720, 300, 50);
        _recBoutonControles = new Rectangle(2000, 620, 425, 50);

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

        //Map aléatoire
        

        //Colisions entre joueur

        _collisionVectorPersonnageRed = CollisionAvecUnPersonnage(_positionPersoRed, _positionPersoBlue);
        _collisionVectorPersonnageBlue = CollisionAvecUnPersonnage(_positionPersoBlue, _positionPersoRed);

        //collision avec le monde


        float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
        KeyboardState keyboardState = Keyboard.GetState();

        _mouseState = Mouse.GetState();
        _rSouris.X = _mouseState.X;
        _rSouris.Y = _mouseState.Y;

        //Joueur Rouge

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            _persoRed.Play("left");
            positionPersoRedY = 0;
            positionPersoRedX = -10;
            ushort tx = (ushort)(_positionPersoRed.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoRed.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty))
            {
                _collisionVectorRed = new Vector2(10, 0);
                if (_directioncollisonRed == "idle")
                    _directioncollisonRed = "left";
                if (_directioncollisonRed != "left")
                {
                    positionPersoRedY = 0;
                    positionPersoRedX = -10;
                }
            }


        }

        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            _persoRed.Play("right");
            positionPersoRedY = 0;
            positionPersoRedX = 10;
            ushort tx = (ushort)(_positionPersoRed.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoRed.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty))
            {
                _collisionVectorRed = new Vector2(-10,0);
                if (_directioncollisonRed == "idle")
                    _directioncollisonRed = "right";
                if (_directioncollisonRed != "right")
                {
                    positionPersoRedY = 0;
                    positionPersoRedX = 10;
                }
            }
            

        }

        else if (keyboardState.IsKeyDown(Keys.Up))
        {
            ushort tx = (ushort)(_positionPersoRed.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoRed.Y / _tiledMap.TileHeight);
            _persoRed.Play("up");
            positionPersoRedY = -10;
            positionPersoRedX = 0;

            if (IsCollision(tx, ty))
            {
                _collisionVectorRed = new Vector2(0, 10);
                if (_directioncollisonRed == "idle")
                    _directioncollisonRed = "up";
                if (_directioncollisonRed != "up")
                    positionPersoRedY = -10;
                    positionPersoRedX = 0;
            }
        }
        else if (keyboardState.IsKeyDown(Keys.Down))
        {
            _persoRed.Play("down");
            positionPersoRedY = 10;
            positionPersoRedX = 0;
            ushort tx = (ushort)(_positionPersoRed.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoRed.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty))
            {
                _collisionVectorRed = new Vector2(0, -10);
                if (_directioncollisonRed == "idle")
                    _directioncollisonRed = "down";
                if (_directioncollisonRed != "down")
                {
                    positionPersoRedY = 10;
                    positionPersoRedX = 0;
                }
            }

        }
        else
        {
            _persoRed.Play("idle");
            ushort tx = (ushort)(_positionPersoRed.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoRed.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty))
            {
                if (_directioncollisonRed == "left")
                {
                    _collisionVectorRed = new Vector2(10, 0);
                }
                else if (_directioncollisonRed == "right")
                {
                    _collisionVectorRed = new Vector2(-10, 0);
                }
                else if (_directioncollisonRed == "up")
                {
                    _collisionVectorRed = new Vector2(0, 10);
                }
                else if (_directioncollisonRed == "down")
                {
                    _collisionVectorRed = new Vector2(0, -10);
                }
                _directioncollisonRed = "idle";
            }
        }

        /*if (keyboardState.IsKeyDown(Keys.NumPad1))
        {
            accelerationRed = 1.5;
        }
        else if (keyboardState.IsKeyUp(Keys.NumPad1))
        {
            accelerationRed = 1;
        }
        */


        //Joueur Bleu

        positionPersoBlueY = 0;
        positionPersoBlueX = 0;

        if (keyboardState.IsKeyDown(Keys.Q))
        {
            _persoBlue.Play("left");
            positionPersoBlueY = 0;
            positionPersoBlueX = -10;
            ushort tx = (ushort)(_positionPersoBlue.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoBlue.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty))
            {
                _collisionVectorBlue = new Vector2(10, 0);
                if (_directioncollisionBlue == "idle")
                    _directioncollisionBlue = "left";
                if (_directioncollisionBlue != "left")
                {
                    positionPersoBlueY = 0;
                    positionPersoBlueX = -10;
                }
            }

        }

        else if (keyboardState.IsKeyDown(Keys.D))
        {
            _persoBlue.Play("right");
            positionPersoBlueY = 0;
            positionPersoBlueX = 10;
            ushort tx = (ushort)(_positionPersoBlue.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoBlue.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty))
            {
                _collisionVectorBlue = new Vector2( - 10, 0);
                if (_directioncollisionBlue == "idle")
                    _directioncollisionBlue = "right";
                if (_directioncollisionBlue != "right")
                {
                    positionPersoBlueY = 0;
                    positionPersoBlueX = 10;
                }
            }
        }

        else if (keyboardState.IsKeyDown(Keys.Z))
        {
            _persoBlue.Play("up");
            positionPersoBlueY = -10;
            positionPersoBlueX = 0;
            ushort tx = (ushort)(_positionPersoBlue.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoBlue.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty))
            {
                _collisionVectorBlue = new Vector2(0, 10);
                if (_directioncollisionBlue == "idle")
                    _directioncollisionBlue = "up";
                if (_directioncollisionBlue != "up")
                {
                    positionPersoBlueY = -10;
                    positionPersoBlueX = 0;
                }
            }
        }
        else if (keyboardState.IsKeyDown(Keys.S))
        {
            _persoBlue.Play("down");
            positionPersoBlueY = 10;
            positionPersoBlueX = 0;
            ushort tx = (ushort)(_positionPersoBlue.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoBlue.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty))
            {
                _collisionVectorBlue = new Vector2(0, -10);
                if (_directioncollisionBlue == "idle")
                    _directioncollisionBlue = "down";
                if (_directioncollisionBlue != "down")
                {
                    positionPersoBlueY = 10;
                    positionPersoBlueX = 0;
                }
            }
        }
        else
        {
            _persoBlue.Play("idle");
            ushort tx = (ushort)(_positionPersoBlue.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoBlue.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty))
            {
                if (_directioncollisionBlue == "left")
                {
                    _collisionVectorBlue = new Vector2(10, 0);
                }
                else if (_directioncollisionBlue == "right")
                {
                    _collisionVectorBlue = new Vector2(-10, 0);
                }
                else if (_directioncollisionBlue == "up")
                {
                    _collisionVectorBlue = new Vector2(0, 10);
                }
                else if (_directioncollisionBlue == "down")
                {
                    _collisionVectorBlue = new Vector2(0, -10);
                }
                _directioncollisionBlue = "idle";
            }
        }
        /*
        if (keyboardState.IsKeyDown(Keys.LeftShift))
        {
            accelerationBlue = 1.5;
        }
        else if (keyboardState.IsKeyUp(Keys.LeftShift))
        {
            accelerationBlue = 1;
        }
        */
        _persoRed.Update(gameTime);
        _persoBlue.Update(gameTime);

        if (_collision == false)
        {
            _positionPersoBlue += new Vector2( (float)positionPersoBlueX,  (float)positionPersoBlueY);
            _positionPersoRed += new Vector2( (float)positionPersoRedX,  (float)positionPersoRedY);
            _collision = true;
            _positionPersoBlue += _collisionVectorPersonnageBlue;
        }
        else if (_collision == true)
        {
            _positionPersoRed += new Vector2((float)accelerationRed * (float)positionPersoRedX, (float)accelerationRed * (float)positionPersoRedY);
            _positionPersoBlue += new Vector2((float)accelerationBlue * (float)positionPersoBlueX, (float)accelerationBlue * (float)positionPersoBlueY);
            _collision = false;
            _positionPersoRed += _collisionVectorPersonnageRed;
        }

        _positionPersoBlue += _collisionVectorBlue;
        _positionPersoRed += _collisionVectorRed;

        _collisionVectorBlue = new Vector2(0,0);
        _collisionVectorRed = new Vector2(0,0);

        _positionCoeurRed = _positionPersoRed + new Vector2(0, -20);
        _positionCoeurBlue = _positionPersoBlue + new Vector2(0, -20);



        if (keyboardState.IsKeyDown(Keys.P))
        {
            _pauseEtat = 1;
        }
        if (_pauseEtat == 1)
        {
            _recBoutonReprendre.X = 500;
            _recBoutonReprendre.Y = 500;

            _recBoutonOptions.X = 500;
            _recBoutonOptions.Y = 500;

            _recBoutonQuitter.X = 500;
            _recBoutonQuitter.Y = 500;

            _recBoutonControles.X = 500;
            _recBoutonControles.Y = 500;


        }

        if (_rSouris.Intersects(_recBoutonReprendre))
            if (_mouseState.LeftButton == ButtonState.Pressed)
        {

                _pauseEtat = 0;
        }
        if (_rSouris.Intersects(_recBoutonQuitter))
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {

                _myGame.Exit();
            }

        if (_pauseEtat == 0)
        {
            _recBoutonReprendre.X = 2000;
            _recBoutonReprendre.Y = 2000;

            _recBoutonOptions.X = 2000;
            _recBoutonOptions.Y = 2000;

            _recBoutonQuitter.X = 2000;
            _recBoutonQuitter.Y = 2000;

            _recBoutonControles.X = 2000;
            _recBoutonControles.Y = 2000;
        }

            _tiledMapRenderer.Update(gameTime);

    }
    public override void Draw(GameTime gameTime)
    {
        _myGame.GraphicsDevice.Clear(Color.Black); // on utilise la reference vers
                                                  // Game1 pour chnager le graphisme
        _spriteBatch.Begin();
        _tiledMapRenderer.Draw();
        /*_spriteBatch.Draw(_fondNoirBlue, _positionFondBlue, Microsoft.Xna.Framework.Color.White);
        _spriteBatch.Draw(_fondNoirRed, _positionFondRed, Microsoft.Xna.Framework.Color.White);*/
        _spriteBatch.Draw(_persoRed, _positionPersoRed);
        _spriteBatch.Draw(_persoBlue, _positionPersoBlue);
        _spriteBatch.Draw(_CoeurBlue, _positionCoeurBlue);
        _spriteBatch.Draw(_CoeurRed, _positionCoeurRed);

        if (_pauseEtat == 1)
        {
            _spriteBatch.Draw(_pause, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);

            if (_rSouris.Intersects(_recBoutonQuitter))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 200, 695), Microsoft.Xna.Framework.Color.White);
            }
            if (_rSouris.Intersects(_recBoutonReprendre))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 200, 495), Microsoft.Xna.Framework.Color.White);
            }
            if (_rSouris.Intersects(_recBoutonControles))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 245, 595), Microsoft.Xna.Framework.Color.White);
            }
            if (_rSouris.Intersects(_recBoutonOptions))
            {
                _spriteBatch.Draw(_rectangleHover, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 245, 595), Microsoft.Xna.Framework.Color.White);
            }
        }

        _spriteBatch.End();
    }


    private bool IsCollision(ushort x, ushort y)
    {
        // définition de tile qui peut être null (?)
        TiledMapTile? tile;
        TiledMapTile valeurTile = mapLayer.GetTile(x, y);
        if (mapLayer.TryGetTile(x, y, out tile) == false)
            return false;
        if (!tile.Value.IsBlank)
            return true;
        
        return false;
    }

    private Vector2 CollisionAvecUnPersonnage(Vector2 positionPersonnage, Vector2 positionAutre)
    {
        Vector2 recul = new Vector2(0,0);
        //coin haut droit
        if (positionPersonnage.X + _width >= positionAutre.X && positionPersonnage.X + _width <= positionAutre.X + _width && positionPersonnage.Y >= positionAutre.Y && positionPersonnage.Y <= positionAutre.Y + _height)
        {
            return recul = new Vector2(-10, 10);
        }

        //coin haut gauche
        if (positionPersonnage.X >= positionAutre.X && positionPersonnage.X <= positionAutre.X + _width && positionPersonnage.Y >= positionAutre.Y && positionPersonnage.Y <= positionAutre.Y + _height)
        {
            return recul = new Vector2(10, 10);
        }

        //coin bas droit
        if (positionPersonnage.X + _width >= positionAutre.X && positionPersonnage.X + _width <= positionAutre.X + _width && positionPersonnage.Y + _height >= positionAutre.Y && positionPersonnage.Y + _height <= positionAutre.Y + _height)
        {
            return recul = new Vector2(-10, -10);
        }

        //coin bas gauche
        if (positionPersonnage.X >= positionAutre.X && positionPersonnage.X <= positionAutre.X + _width && positionPersonnage.Y + _height >= positionAutre.Y && positionPersonnage.Y + _height <= positionAutre.Y + _height)
        {
            return recul = new Vector2(10, -10);
        }
        else
        {
            return recul;
        }

       
    }

   /* private void MapAléatoire()
    {
        // définition de tile qui peut être null (?)
        TiledMapTile? tile;
        TiledMapTile valeurTile = mapLayer.GetTile(x, y);

        for (int i; i <)
        if (mapLayer.GetTileIndex(x, y) == 1)
            mapLayer.SetTile(x, y, 1);
        
    }*/

}