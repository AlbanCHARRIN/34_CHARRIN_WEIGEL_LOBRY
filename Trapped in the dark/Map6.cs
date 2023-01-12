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

public class Map6 : GameScreen
{
    private Game1 _myGame;
    private SpriteBatch _spriteBatch { get; set; }
    // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
    // défini dans Game1

    // Map
    public TiledMap _tiledMap;
    public TiledMapRenderer _tiledMapRenderer;
    public TiledMapTileLayer Obstacle;
    public TiledMapTileLayer piege;
    public TiledMapTileset _tileset;
    public TiledMapTileLayer _arrivee;

    // Timer
    private int _timer;

    //constante
    const int _width = 37;
    const int _height = 37;

    //Fond noir
    private Texture2D _fondNoirBlue;
    private Vector2 _positionFondBlue;

    //Collisions
    bool _collision = false;
    string _directioncollisonRed;
    string _directioncollisionBlue;
    Vector2 _collisionVectorPersonnageRed;
    Vector2 _collisionVectorPersonnageBlue;
    Vector2 _collisionVectorRed;
    Vector2 _collisionVectorBlue;

    //Pièges
    private AnimatedSprite[] _piege = new AnimatedSprite[23];
    private Vector2[] _positionPiege = new Vector2[23];
    private int _numPiegePosition = 0;
    private int[] _namePiege = new int[23];
    private int[] _positionXPieges = new int[23];
    private int[] _positionYPieges = new int[23];
    private bool[] _colisionCoeur = new bool[23];
    private Song coeurajouter;
    private bool[] _colisionCoeurFull = new bool[23];
    private bool[] _colisionChrono = new bool[23];
    private bool[] _colisionChronoPlus = new bool[23];
    private int serpentTime;
    private bool[] serpentTouch = new bool[23];
    private Song snakeHissing;
    private int bombeTime;
    private bool[] bombeTouch = new bool[23];
    private bool _colisionBombe;
    private Song bombesong;
    private int _chauveSourisTime;
    private bool[] chauveSourisTouch = new bool[23];
    private int piqueTime;
    private Song piqueSword;
    private int trapTime;
    private int[] enclumeTime = new int[23];
    private bool[] enclumeTouch = new bool[23];
    private Song enclumeTombe;



    //Joueur Rouge
    private Vector2 _positionPersoRed;
    private AnimatedSprite _persoRed;

    private double accelerationRed = 1;
    private double positionPersoRedX = 0;
    private double positionPersoRedY = 0;
    bool vieActuelleRed = false;

    private int compteurRed;

    //Coeur 
    private AnimatedSprite _coeurRed;
    int vieRed;
    private Vector2 _positionCoeurRed;

    //Joueur Bleu
    private Vector2 _positionPersoBlue;
    private AnimatedSprite _persoBlue;

    private double accelerationBlue = 1;
    private double positionPersoBlueX = 0;
    private double positionPersoBlueY = 0;
    bool vieActuelleBlue = false;


    private int compteurBlue;

    //Coeur 
    private AnimatedSprite _coeurBlue;
    int vieBlue;
    private Vector2 _positionCoeurBlue;

    // écran
    private ScreenManager _screenManager;
    private Texture2D _pause;
   

    //gestion de pause
   

    //gestion des touches
    private MouseState _mouseState;
    private Rectangle _rSouris;

    private SpriteFont _font;

    public Map6(Game1 game) : base(game)
    {
        _myGame = game;
    }
    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("PixelFont");

        // colision

        _directioncollisonRed = "idle";
        _directioncollisionBlue = "idle";
        //tileMap
        _tiledMap = Content.Load<TiledMap>("Map6");
        _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

        GraphicsDevice.BlendState = BlendState.AlphaBlend;

        Obstacle = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacle");

        _arrivee = _tiledMap.GetLayer<TiledMapTileLayer>("Arrivee");


        //Timer
        _timer = 150 * 60;

        //Piege
        piege = _tiledMap.GetLayer<TiledMapTileLayer>("Piege");

        SpriteSheet spriteSheetPiege = Content.Load<SpriteSheet>("PiegeAnimation.sf", new JsonContentLoader());


        for (int i = 0; i < 23; i++)
        {
            Random r = new Random();
            _numPiegePosition = r.Next(0, 100);
            _piege[i] = new AnimatedSprite(spriteSheetPiege);

            if (_numPiegePosition == 100)
            {
                _piege[i].Play("chronoPlus");
                _namePiege[i] = 1;
                _colisionChronoPlus[i] = false;
            }
            else if (_numPiegePosition == 99)
            {
                _piege[i].Play("coeurfull");
                _namePiege[i] = 2;
                _colisionCoeurFull[i] = false;
            }
            else if (_numPiegePosition >= 93 && _numPiegePosition <= 98)
            {
                _piege[i].Play("chrono");
                _namePiege[i] = 3;
                _colisionChrono[i] = false;
            }

            else if (_numPiegePosition >= 87 && _numPiegePosition <= 92)
            {
                _piege[i].Play("coeur");
                _namePiege[i] = 4;
                _colisionCoeur[i] = false;
                coeurajouter = Content.Load<Song>("Health_up");
            }

            else if (_numPiegePosition >= 80 && _numPiegePosition <= 86)
            {
                _piege[i].Play("serpent");
                _namePiege[i] = 5;
                serpentTime = 0;
                serpentTouch[i] = false;
                snakeHissing = Content.Load<Song>("snake-hissing-6092");
            }
            else if (_numPiegePosition >= 75 && _numPiegePosition <= 79)
            {
                _piege[i].Play("bomb");
                _namePiege[i] = 6;
                bombeTime = 0;
                _colisionBombe = false;
                bombeTouch[i] = false;
                bombesong = Content.Load<Song>("boom_c_06-102838");
            }
            else if (_numPiegePosition >= 70 && _numPiegePosition <= 74)
            {
                _piege[i].Play("chauveSourisCouche");
                _namePiege[i] = 7;
                _chauveSourisTime = 0;
                chauveSourisTouch[i] = false;
            }
            else if (_numPiegePosition >= 50 && _numPiegePosition <= 69)
            {
                _piege[i].Play("piqueFerme");
                _namePiege[i] = 8;
                piqueTime = 0;
                piqueSword = Content.Load<Song>("sword-impact-100489");
            }
            else if (_numPiegePosition >= 30 && _numPiegePosition <= 49)
            {
                _piege[i].Play("trapOuverture");
                _namePiege[i] = 9;
                trapTime = 0;
            }
            else if (_numPiegePosition <= 29)
            {
                _piege[i].Play("enclumeAvant");
                _namePiege[i] = 10;
                enclumeTime[i] = 0;
                enclumeTouch[i] = false;
                enclumeTombe = Content.Load<Song>("big-metal-knock-001-11345");
            }


        }

        _numPiegePosition = 0;
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 26; j++)
            {

                if (PiegePositionning((ushort)i, (ushort)j, piege))
                {

                    _positionXPieges[_numPiegePosition] = i;
                    _positionYPieges[_numPiegePosition] = j;
                    _numPiegePosition++;

                }

            }
        }

        for (int i = 0; i < _numPiegePosition; i++)
        {
            _positionPiege[i] = new Vector2(_positionXPieges[i] * 32, _positionYPieges[i] * 32);
        }



        //Joueur Rouge
        _positionPersoRed = new Vector2(75, 810);

        SpriteSheet spriteSheetRed = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
        _persoRed = new AnimatedSprite(spriteSheetRed);

        compteurRed = 0;

        //Coeur
        SpriteSheet spriteSheetCoeurRed = Content.Load<SpriteSheet>("coeurPerso.sf", new JsonContentLoader());
        _coeurRed = new AnimatedSprite(spriteSheetCoeurRed);
        vieRed = 3;
        _positionCoeurRed = _positionPersoRed + new Vector2(0, -20);

        //Joueur Bleu
        _positionPersoBlue = new Vector2(150, 810);

        SpriteSheet spriteSheetBlue = Content.Load<SpriteSheet>("persoPrincipaleAnimation.sf", new JsonContentLoader());
        _persoBlue = new AnimatedSprite(spriteSheetBlue);

        compteurBlue = 0;

        //Coeur
        SpriteSheet spriteSheetCoeurBlue = Content.Load<SpriteSheet>("coeurPersoPrincipale.sf", new JsonContentLoader());
        _coeurBlue = new AnimatedSprite(spriteSheetCoeurBlue);
        vieBlue = 3;
        _positionCoeurBlue = _positionPersoBlue + new Vector2(0, -20);

        //Fond noir
        _fondNoirBlue = Content.Load<Texture2D>("FondNoir");
        _positionFondBlue = _positionPersoBlue + new Vector2(-_fondNoirBlue.Width / 2 + 10, -_fondNoirBlue.Height / 2 + 10);


        _screenManager = new ScreenManager();
        _myGame.Components.Add(_screenManager);

        _pause = Content.Load<Texture2D>("Pause");
   

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


        //Colisions entre joueur

        _collisionVectorPersonnageRed = CollisionAvecUnPersonnage(_positionPersoRed, _positionPersoBlue);
        _collisionVectorPersonnageBlue = CollisionAvecUnPersonnage(_positionPersoBlue, _positionPersoRed);

        //collision avec le monde


        float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
        KeyboardState keyboardState = Keyboard.GetState();

        _mouseState = Mouse.GetState();
        _rSouris.X = _mouseState.X;
        _rSouris.Y = _mouseState.Y;

        //Piege

        for (int i = 0; i < _piege.Length; i++)
        {
            if (_namePiege[i] == 1)
            {
                if (_colisionChronoPlus[i] == false)
                {
                    _piege[i].Play("chronoPlus");

                    if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                    {
                        MediaPlayer.Play(coeurajouter);
                        _timer += 100;
                        _colisionChronoPlus[i] = true;
                        _piege[i].Play("stop");
                    }
                    else if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                    {
                        MediaPlayer.Play(coeurajouter);
                        _timer += 100;
                        _colisionChronoPlus[i] = true;
                        _piege[i].Play("stop");
                    }
                }

            }
            else if (_namePiege[i] == 2)
            {
                if (_colisionCoeurFull[i] == false)
                {
                    _piege[i].Play("coeurfull");
                    if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                    {
                        MediaPlayer.Play(coeurajouter);
                        vieBlue = 3;
                        _colisionCoeurFull[i] = true;
                        _piege[i].Play("stop");
                    }
                    else if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                    {
                        MediaPlayer.Play(coeurajouter);
                        vieRed = 3;
                        _colisionCoeurFull[i] = true;
                        _piege[i].Play("stop");
                    }
                }

            }
            else if (_namePiege[i] == 3)
            {
                if (_colisionChrono[i] == false)
                {
                    _piege[i].Play("chrono");
                    if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                    {
                        MediaPlayer.Play(coeurajouter);
                        _timer += 20;
                        _colisionChrono[i] = true;
                        _piege[i].Play("stop");
                    }
                    else if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                    {
                        MediaPlayer.Play(coeurajouter);
                        _timer += 20;
                        _colisionChrono[i] = true;
                        _piege[i].Play("stop");
                    }
                }

            }
            else if (_namePiege[i] == 4)
            {
                if (_colisionCoeur[i] == false)
                {
                    _piege[i].Play("coeur");
                    if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                    {
                        MediaPlayer.Play(coeurajouter);
                        vieBlue++;
                        _colisionCoeur[i] = true;
                        _piege[i].Play("stop");
                    }
                    else if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                    {
                        MediaPlayer.Play(coeurajouter);
                        vieRed++;
                        _colisionCoeur[i] = true;
                        _piege[i].Play("stop");
                    }
                }
            }
            else if (_namePiege[i] == 5)
            {
                if (serpentTime == 0)
                    _piege[i].Play("serpent");
                if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                    serpentTouch[i] = true;
                else if (serpentTouch[i])
                {
                    serpentTime++;
                    if (serpentTime == 1)
                    {
                        _piege[i].Play("serpentSortant");
                    }
                    else if (serpentTime >= 36)
                    {
                        MediaPlayer.Play(snakeHissing);
                        _piege[i].Play("serpentMoving");
                        if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                            compteurBlue = 10;
                    }
                }
                if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                    serpentTouch[i] = true;
                if (serpentTouch[i])
                {
                    serpentTime++;
                    if (serpentTime == 1)
                    {
                        _piege[i].Play("serpentSortant");
                    }
                    else if (serpentTime >= 36)
                    {
                        _piege[i].Play("serpentMoving");
                        if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                            compteurRed = 10;
                    }
                }
            }
            else if (_namePiege[i] == 6)
            {
                if (bombeTime == 0)
                    _piege[i].Play("bomb");
                if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                    bombeTouch[i] = true;
                else if (bombeTouch[i])
                {
                    if (bombeTime <= 40)
                    {
                        _piege[i].Play("boom");
                        bombeTime++;
                    }

                    else if (bombeTime == 40)
                    {
                        if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                            compteurBlue = 10;
                        if (bombeTime == 64)
                        {
                            _colisionBombe = true;
                            _piege[i].Play("stop");
                        }

                    }
                }
                if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                    bombeTouch[i] = true;
                else if (bombeTouch[i])
                {
                    if (bombeTime <= 40)
                    {
                        _piege[i].Play("boom");
                        bombeTime++;

                    }
                    if (bombeTime == 40)
                    {
                        MediaPlayer.Play(bombesong);
                        if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                            compteurRed = 10;
                        if (bombeTime == 84)
                        {
                            _colisionBombe = true;
                            _piege[i].Play("stop");
                        }
                    }
                }
            }
            else if (_namePiege[i] == 7)
            {
                if (_chauveSourisTime == 0)
                    _piege[i].Play("chauveSourisCouche");
                if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                    chauveSourisTouch[i] = true;
                else if (chauveSourisTouch[i])
                {
                    _chauveSourisTime++;
                }
                if (_chauveSourisTime == 1)
                {
                    _piege[i].Play("chauveSourisSortant");
                }

                else if (_chauveSourisTime >= 60)
                {
                    _piege[i].Play("chauveSouris");
                    if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                        compteurBlue = 10;
                }

                if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                    chauveSourisTouch[i] = true;
                else if (chauveSourisTouch[i])
                {
                    if (_chauveSourisTime == 1)
                    {
                        _piege[i].Play("chauveSourisSortant");
                        _chauveSourisTime++;
                    }
                    else if (_chauveSourisTime >= 45)
                    {
                        _piege[i].Play("chauveSouris");
                        if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                            compteurRed = 10;
                    }
                }
            }
            else if (_namePiege[i] == 8)
            {
                if (piqueTime == 60)
                {
                    _piege[i].Play("piqueOuverture");
                    if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                        compteurBlue = 10;
                    else if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                        compteurRed = 10;
                }
                else if (piqueTime == 120)
                {
                    MediaPlayer.Play(piqueSword);
                    _piege[i].Play("piqueOuvert");
                    if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                        compteurBlue = 10;
                    else if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                        compteurRed = 10;
                }
                else if (piqueTime == 180)
                {
                    _piege[i].Play("piqueFermeture");
                }
                else if (piqueTime >= 240)
                {
                    _piege[i].Play("piqueFerme");
                    piqueTime = 0;

                }


            }
            else if (_namePiege[i] == 9)
            {
                if (trapTime == 60)
                {
                    _piege[i].Play("trapOuverture");
                    if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                        compteurBlue = 10;
                    else if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                        compteurRed = 10;
                }
                else if (trapTime == 120)
                {
                    _piege[i].Play("trapOuvert");
                    if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                        compteurBlue = 10;
                    else if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                        compteurRed = 10;
                }
                else if (trapTime == 180)
                {
                    _piege[i].Play("trapFermeture");
                }
                else if (trapTime >= 240)
                {
                    _piege[i].Play("trapFerme");
                    trapTime = 0;

                }

            }
            else if (_namePiege[i] == 10)
            {
                if (enclumeTime[i] == 0)
                    _piege[i].Play("enclumeAvant");
                if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                    enclumeTouch[i] = true;
                else if (enclumeTouch[i])
                {
                    if (enclumeTime[i] <= 60)
                    {
                        _piege[i].Play("enclume");
                        enclumeTime[i]++;
                    }
                    else if (enclumeTime[i] == 60)
                    {
                        if (bombeTime == 62) ;
                        if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoBlue) != new Vector2(0, 0))
                            compteurBlue = 10;
                    }
                }
                if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                    enclumeTouch[i] = true;
                else if (enclumeTouch[i])
                {
                    if (enclumeTime[i] <= 60)
                    {
                        _piege[i].Play("enclume");
                        enclumeTime[i]++;
                    }
                    else if (enclumeTime[i] == 60)
                    {
                        MediaPlayer.Play(enclumeTombe);
                        if (bombeTime == 62) ;
                        if (CollisionAvecUnPersonnage(_positionPiege[i], _positionPersoRed) != new Vector2(0, 0))
                            compteurRed = 10;
                    }
                }
            }
        }

        //Gestion de la vie
        if (compteurRed <= 0)
        {
            vieActuelleRed = false;
            accelerationRed = 1;
        }
        else
        {
            if (vieActuelleRed == false)
            {
                vieActuelleRed = true;
                vieRed--;
            }
            compteurRed--;
        }

        if (compteurBlue == 0)
        {
            vieActuelleBlue = false;
            accelerationBlue = 1;
        }
        else
        {
            if (vieActuelleBlue == false)
            {
                vieActuelleBlue = true;
                vieBlue--;
            }
            compteurBlue--;
        }

        //coeur Rouge
        if (vieRed == 3)
        {
            _coeurRed.Play("troisVie");
        }
        else if (vieRed == 2)
        {
            _coeurRed.Play("deuxVie");
        }
        else if (vieRed == 1)
        {
            _coeurRed.Play("uneVie");
        }
        else if (vieRed <= 0 || _timer == 0)
        {
            _coeurRed.Play("zeroVie");
            _myGame.Etat = Game1.Etats.GameOver;
        }
        else if (vieRed >= 3)
        {
            vieRed = 3;
        }

        //Coeur Bleu
        if (vieBlue == 3)
        {
            _coeurBlue.Play("troisVie");
        }
        else if (vieBlue == 2)
        {
            _coeurBlue.Play("deuxVie");
        }
        else if (vieBlue == 1)
        {
            _coeurBlue.Play("uneVie");
        }
        else if (vieBlue == 0)
        {
            _coeurBlue.Play("zeroVie");
            _myGame.Etat = Game1.Etats.GameOver;
        }

        //Joueur Rouge

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            _persoRed.Play("left");

            positionPersoRedY = 0;
            positionPersoRedX = -10;
            ushort tx = (ushort)(_positionPersoRed.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoRed.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty, Obstacle))
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
            if (IsCollision(tx, ty, Obstacle))
            {
                _collisionVectorRed = new Vector2(-10, 0);
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

            if (IsCollision(tx, ty, Obstacle))
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
            if (IsCollision(tx, ty, Obstacle))
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
            if (IsCollision(tx, ty, Obstacle))
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
            if (IsCollision(tx, ty, Obstacle))
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
            if (IsCollision(tx, ty, Obstacle))
            {
                _collisionVectorBlue = new Vector2(-10, 0);
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
            if (IsCollision(tx, ty, Obstacle))
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
            if (IsCollision(tx, ty, piege) && compteurBlue == 0)
            {
                compteurBlue = 10;
            }
        }
        else if (keyboardState.IsKeyDown(Keys.S))
        {
            _persoBlue.Play("down");
            positionPersoBlueY = 10;
            positionPersoBlueX = 0;
            ushort tx = (ushort)(_positionPersoBlue.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPersoBlue.Y / _tiledMap.TileHeight);
            if (IsCollision(tx, ty, Obstacle))
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
            if (IsCollision(tx, ty, Obstacle))
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



        _coeurBlue.Update(gameTime);
        _coeurRed.Update(gameTime);
        _persoRed.Update(gameTime);
        _persoBlue.Update(gameTime);

        for (int i = 0; i < _piege.Length; i++)
        {
            if (_namePiege[i] == 1 && _colisionChronoPlus[i]) ;
            else if (_namePiege[i] == 2 && _colisionCoeurFull[i]) ;
            else if (_namePiege[i] == 3 && _colisionChrono[i]) ;
            else if (_namePiege[i] == 4 && _colisionCoeur[i]) ;
            else if (_namePiege[i] == 6 && _colisionBombe) ;
            else
            {
                _piege[i].Update(gameTime);
            }

        }

        if (_collision == false)
        {
            _positionPersoBlue += new Vector2((float)accelerationBlue * (float)positionPersoBlueX, (float)accelerationBlue * (float)positionPersoBlueY);
            _positionPersoRed += new Vector2((float)accelerationRed * (float)positionPersoRedX, (float)accelerationRed * (float)positionPersoRedY);
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

        _collisionVectorBlue = new Vector2(0, 0);
        _collisionVectorRed = new Vector2(0, 0);

        _positionFondBlue = _positionPersoBlue + new Vector2(-_fondNoirBlue.Width / 2 + 10, -_fondNoirBlue.Height / 2 + 10);




        piqueTime++;
        _positionCoeurRed = _positionPersoRed + new Vector2(0, -20);
        _positionCoeurBlue = _positionPersoBlue + new Vector2(0, -20);




        _tiledMapRenderer.Update(gameTime);
        _timer--;
        trapTime++;


        ushort bx = (ushort)(_positionPersoBlue.X / _tiledMap.TileWidth);
        ushort by = (ushort)(_positionPersoBlue.Y / _tiledMap.TileHeight);
        ushort rx = (ushort)(_positionPersoRed.X / _tiledMap.TileWidth);
        ushort ry = (ushort)(_positionPersoRed.Y / _tiledMap.TileHeight);
        if (IsCollision(bx, by, _arrivee) && IsCollision(rx, ry, _arrivee))
        {
            _myGame.Etat = Game1.Etats.Quit;
        }

    }
    public override void Draw(GameTime gameTime)
    {
        _myGame.GraphicsDevice.Clear(Color.Black); // on utilise la reference vers
                                                   // Game1 pour chnager le graphisme
        _spriteBatch.Begin();
        _tiledMapRenderer.Draw();

        for (int i = 0; i < _piege.Length; i++)
        {
            if (_namePiege[i] == 1 && _colisionChronoPlus[i]) ;
            else if (_namePiege[i] == 2 && _colisionCoeurFull[i]) ;
            else if (_namePiege[i] == 3 && _colisionChrono[i]) ;
            else if (_namePiege[i] == 4 && _colisionCoeur[i]) ;
            else if (_namePiege[i] == 6 && _colisionBombe) ;
            else
            {
                _spriteBatch.Draw(_piege[i], _positionPiege[i]);
            }
        }
        _spriteBatch.Draw(_coeurRed, _positionCoeurRed);
        _spriteBatch.Draw(_persoRed, _positionPersoRed);
        _spriteBatch.Draw(_persoBlue, _positionPersoBlue);
        _spriteBatch.Draw(_coeurBlue, _positionCoeurBlue);
        _spriteBatch.Draw(_fondNoirBlue, _positionFondBlue, Microsoft.Xna.Framework.Color.White);

        _spriteBatch.DrawString(_font, $"{_timer}", new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);


        _spriteBatch.End();
    }


    private bool IsCollision(ushort x, ushort y, TiledMapTileLayer tileref)
    {
        // définition de tile qui peut être null (?)
        TiledMapTile? tile;
        TiledMapTile valeurTile = tileref.GetTile(x, y);
        if (tileref.TryGetTile(x, y, out tile) == false)
            return false;
        if (!tile.Value.IsBlank)
            return true;

        return false;
    }

    private Vector2 CollisionAvecUnPersonnage(Vector2 positionPersonnage, Vector2 positionAutre)
    {
        Vector2 recul = new Vector2(0, 0);
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

    private bool PiegePositionning(ushort x, ushort y, TiledMapTileLayer tileref)
    {
        // définition de tile qui peut être null (?)
        TiledMapTile? tile;
        TiledMapTile valeurTile = tileref.GetTile(x, y);
        if (tileref.GetTile(x, y).GlobalIdentifier != 0)
        {
            return true;

        }
        else
        {
            return false;

        }


        return false;
    }

}