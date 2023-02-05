using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Root66.GameFolder;
using Root66.GameFolder.Obsticals;

namespace Root66
{
    enum Scene { Menu, Game }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _screenWidth;
        private int _screenHeight;

        private Scene _currentScene;

        private SpriteFont deathFont;
        private SpriteFont UIFont;

        private Texture2D blankTexture;
        private Texture2D menuBackground;
        private Texture2D carTexture;
        private Texture2D fuelTexture;
        private Texture2D engineTexture;
        private Texture2D coneTexture;
        private Texture2D personFrontTexture;
        private Texture2D personBackTexture;

        private double _eventTimer;
        private double _distance;

        private Player player;
        private Background background;
        private DisplayBar healthBar;
        private DisplayBar fuelBar;

        private Sprite startButton;
        private Sprite exitButton;

        List<Sprite> gameSprites = new List<Sprite>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            _currentScene = Scene.Menu;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;

            // TODO: use this.Content to load your game content here
            Texture2D backgroundTexture = Content.Load<Texture2D>("Background");
            menuBackground = Content.Load<Texture2D>("MenuBackground");
            carTexture = Content.Load<Texture2D>("CarWhite");
            fuelTexture = Content.Load<Texture2D>("Fuel");
            engineTexture = Content.Load<Texture2D>("Engine");
            coneTexture = Content.Load<Texture2D>("Cone");
            personFrontTexture = Content.Load<Texture2D>("PersonFront");
            personBackTexture = Content.Load<Texture2D>("PersonBack");
            Person.BloodTexture = Content.Load<Texture2D>("Blood");

            deathFont = Content.Load<SpriteFont>("DeathFont");
            UIFont = Content.Load<SpriteFont>("UIFont");

            background = new Background(_screenHeight, backgroundTexture, 10f);
            gameSprites.Add(background);
            player = new Player(_screenHeight, carTexture, _screenHeight / 10, 3);
            blankTexture = new Texture2D(GraphicsDevice, 1, 1);
            blankTexture.SetData(new[] { Color.White });
            healthBar = new DisplayBar(blankTexture, _screenWidth / 5, _screenHeight / 30, _screenWidth / 10 * 7, _screenHeight / 30 * 2, Color.Red);
            gameSprites.Add(healthBar);
            fuelBar = new DisplayBar(blankTexture, _screenWidth / 5, _screenHeight / 30, _screenWidth / 10 * 3, _screenHeight / 30 * 2, Color.YellowGreen);
            gameSprites.Add(fuelBar);

            Vector2 startButtonSize = UIFont.MeasureString("Start");
            startButton = new Sprite(blankTexture, (int)startButtonSize.X * 2, (int)startButtonSize.Y * 2, ((_screenWidth / 5 * 2) - (startButtonSize.X)), ((_screenHeight / 2) - (startButtonSize.Y)));
            Vector2 exitButtonSize = UIFont.MeasureString("Exit");
            exitButton = new Sprite(blankTexture, (int)exitButtonSize.X * 2, (int)exitButtonSize.Y * 2, ((_screenWidth / 5 * 4) - (exitButtonSize.X)), ((_screenHeight / 2) - (exitButtonSize.Y)));
        }

        public void RestartGame()
        {
            for (int index = 0; index < gameSprites.Count; index++)
            {
                if (gameSprites[index] is Obstical obstical)
                {
                    gameSprites.RemoveAt(index);
                    index--;
                }
                else
                {
                    gameSprites[index].Reset();
                }
            }
            player.Reset();
            _eventTimer = 0;
            _distance = 0;
        }

        protected override void Update(GameTime gameTime)
        {
            if (_currentScene == Scene.Game)
            {
                if (player.Alive)
                {
                    // Update Player, Background, and Stat Bars
                    player.Update(gameTime);
                    background.SpeedRatio = player.speedRatio;
                    background.Update(gameTime);
                    _distance += gameTime.ElapsedGameTime.TotalMinutes * 2;

                    // Update each obstical, and check to see if Player has collided
                    for (int index = 0; index < gameSprites.Count; index++)
                    {
                        if (gameSprites[index] is Obstical obstical)
                        {
                            obstical.xChange = background.speed * background.SpeedRatio;
                            obstical.Update(gameTime);

                            if (obstical.IntersectsWith(player))
                            {
                                obstical.Effect(player);
                                if (player.Alive && obstical is not Person)
                                {
                                    gameSprites.RemoveAt(index);
                                    index--;
                                }
                            }
                            else if (obstical.xPosition < -50)
                            {
                                gameSprites.RemoveAt(index);
                                index--;
                            }
                        }
                    }

                    // Generate new obsticals every second the player is at full speed
                    _eventTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (_eventTimer * player.speedRatio > 1)
                    {
                        _eventTimer = 0;

                        Random rand = new Random();
                        int effectChance = rand.Next(1000);

                        int size = _screenHeight / 10;

                        if (effectChance < 300) // 30% Cone
                        {
                            int coneCount = rand.Next(1, 4); // Number of cones to spawn
                            for (int coneIndex = 0; coneIndex < coneCount; coneIndex++)
                            {
                                gameSprites.Add(new Cone(coneTexture, _screenWidth, _screenHeight, (int)(size / ((float)coneTexture.Height / coneTexture.Width)), size));
                            }
                        }

                        if (effectChance < 5 || effectChance > 995) // 1% Engine = Health
                        {
                            gameSprites.Add(new Engine(engineTexture, _screenWidth, _screenHeight, (int)(size / ((float)engineTexture.Height / engineTexture.Width)), size));
                        }
                        else if (effectChance < 60 || effectChance > 935) // 5% Fuel
                        {
                            size = _screenHeight / 15;
                            gameSprites.Add(new Fuel(fuelTexture, _screenWidth, _screenHeight, size, size));
                        }
                        else if (effectChance < 360) // 30%
                        {
                            gameSprites.Add(new Car(carTexture, _screenWidth, _screenHeight, (int)(size / ((float)carTexture.Height / carTexture.Width)), size));
                        }
                        else if (effectChance < 460) // 10%
                        {
                            size = _screenHeight / 8;
                            if (rand.Next(1) == 1) // Random direction
                            {
                                gameSprites.Add(new Person(personBackTexture, _screenWidth, _screenHeight, (int)(size / ((float)personBackTexture.Height / personBackTexture.Width)), size, Direction.Up));
                            }
                            else
                            {
                                gameSprites.Add(new Person(personFrontTexture, _screenWidth, _screenHeight, (int)(size / ((float)personFrontTexture.Height / personFrontTexture.Width)), size, Direction.Down));
                            }
                        }
                    }


                }
                else
                {
                    KeyboardState keyboardState = Keyboard.GetState();

                    if (keyboardState.IsKeyDown(Keys.Enter) || keyboardState.IsKeyDown(Keys.Space))
                    {
                        _currentScene = Scene.Menu;
                    }
                }

                //Update stat bars
                healthBar.ProgressRatio = player.Health / Player.MaxHealth;
                fuelBar.ProgressRatio = player.Fuel / Player.MaxFuel;
                base.Update(gameTime);
            }
            else // Scene is menu
            {
                MouseState mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (startButton.IntersectsWith(mouseState.X, mouseState.Y))
                    {
                        RestartGame();
                        _currentScene = Scene.Game;
                    }
                    else if (exitButton.IntersectsWith(mouseState.X, mouseState.Y))
                    {
                        Exit();
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw each game sprite
            _spriteBatch.Begin();
            if (_currentScene == Scene.Game)
            {
                foreach (var sprite in gameSprites)
                {
                    sprite.Draw(_spriteBatch);
                }
                player.Draw(_spriteBatch);

                _spriteBatch.DrawString(UIFont, _distance.ToString("0.00") + " miles", new Vector2(_screenWidth / 10 * 1, _screenHeight / 30 * 2), Color.White);
                _spriteBatch.DrawString(UIFont, "Fuel", new Vector2(_screenWidth / 40 * 13, _screenHeight / 30 * 2), Color.White);
                _spriteBatch.DrawString(UIFont, "Health", new Vector2(_screenWidth / 40 * 29, _screenHeight / 30 * 2), Color.White);

                if (!player.Alive)
                {
                    _spriteBatch.DrawString(deathFont, "DEAD", new Vector2((_screenWidth / 2), _screenHeight / 2) - (deathFont.MeasureString("DEAD") / 2), Color.Red);
                    _spriteBatch.DrawString(UIFont, "Press Enter or Space to continue...", new Vector2(_screenWidth / 2, (_screenHeight / 2) + deathFont.MeasureString("DEAD").Y) - (UIFont.MeasureString("Press Enter or Space to continue...") / 2), Color.White);
                }
            }
            else // Menu
            {
                _spriteBatch.Draw(menuBackground, new Rectangle(0, 0, _screenWidth, _screenHeight), Color.White);

                startButton.Draw(_spriteBatch);
                Vector2 startButtonSize = UIFont.MeasureString("Start");
                _spriteBatch.DrawString(UIFont, "Start", new Vector2((_screenWidth / 5 * 2), _screenHeight / 2) - (startButtonSize / 2), Color.Black);

                exitButton.Draw(_spriteBatch);
                Vector2 exitButtonSize = UIFont.MeasureString("Exit");
                _spriteBatch.DrawString(UIFont, "Exit", new Vector2((_screenWidth / 5 * 4), _screenHeight / 2) - (exitButtonSize / 2), Color.Black);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}