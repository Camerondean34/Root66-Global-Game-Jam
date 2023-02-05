using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Root66.GameFolder;
using Root66.GameFolder.Obsticals;

namespace Root66
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _screenWidth;
        private int _screenHeight;

        private Texture2D carTexture;
        private Texture2D fuelTexture;
        private Texture2D engineTexture;
        private Texture2D coneTexture;
        private Texture2D personFrontTexture;
        private Texture2D personBackTexture;

        private double _eventTimer = 0;

        private Player player;
        private Background background;
        private DisplayBar healthBar;
        private DisplayBar fuelBar;

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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;

            // TODO: use this.Content to load your game content here
            Texture2D backgroundTexture = Content.Load<Texture2D>("Background");
            carTexture = Content.Load<Texture2D>("CarWhite");
            fuelTexture = Content.Load<Texture2D>("Fuel");
            engineTexture = Content.Load<Texture2D>("Engine");
            coneTexture = Content.Load<Texture2D>("Cone");
            personFrontTexture = Content.Load<Texture2D>("PersonFront");
            personBackTexture = Content.Load<Texture2D>("PersonBack");

            background = new Background(_screenHeight, backgroundTexture, 10f);
            gameSprites.Add(background);
            player = new Player(_screenHeight, carTexture, _screenHeight / 10, 3);
            gameSprites.Add(player);
            Texture2D blankRect = new Texture2D(GraphicsDevice, 1, 1);
            blankRect.SetData(new[] { Color.White });
            healthBar = new DisplayBar(blankRect, _screenWidth / 5, _screenHeight / 30, _screenWidth / 10 * 7, _screenHeight / 30 * 2, Color.Red);
            gameSprites.Add(healthBar);
            fuelBar = new DisplayBar(blankRect, _screenWidth / 5, _screenHeight / 30, _screenWidth / 10 * 3, _screenHeight / 30 * 2, Color.YellowGreen);
            gameSprites.Add(fuelBar);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // Do a dice roll
            if (player.Alive)
            {
                // Update Player, Background, and Stat Bars
                player.Update(gameTime);
                background.SpeedRatio = player.speedRatio;
                background.Update(gameTime);


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
                            gameSprites.RemoveAt(index);
                            index--;
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

                //Update stat bars
                healthBar.ProgressRatio = player.Health / Player.MaxHealth;
                fuelBar.ProgressRatio = player.Fuel / Player.MaxFuel;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LawnGreen);

            // Draw each game sprite
            _spriteBatch.Begin();
            foreach (var sprite in gameSprites)
            {
                sprite.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}