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
                healthBar.ProgressRatio = player.Health / Player.MaxHealth;
                fuelBar.ProgressRatio = player.Fuel / Player.MaxFuel;

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
                    }
                }

                // Generate new obsticals
                if (player.speed * player.speedRatio > 0)
                {
                    //TODO Make this based on game time?
                    int random = new Random().Next(6000); // Assuming 60 FPS * 100
                    if (random < 10) // 5%
                    {
                        int size = _screenHeight / 20;
                        gameSprites.Add(new Fuel(fuelTexture, _screenWidth, _screenHeight, size, size));
                    }
                    //TODO Obsticals to add:
                    // Traffic Cones
                    // Engines for health
                    // People, and then police to come bust you if you hit them
                }
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