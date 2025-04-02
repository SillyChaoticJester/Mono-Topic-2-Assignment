﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Mono_Topic_2_Assignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;
        Random generator;
        Texture2D spaceBackgroundTexture;
        List<Texture2D> textures;
        List<Texture2D> planetTextures;
        List<Rectangle> planetRects;
        float seconds;
        float respawnTime;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            generator = new Random();
            textures = new List<Texture2D>();
            planetTextures = new List<Texture2D>();
            planetRects = new List<Rectangle>();
            seconds = 0f;
            respawnTime = 5f;
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spaceBackgroundTexture = Content.Load<Texture2D>("Images/space_background");
            for (int i = 1; i <= 13; i++)
            {
                textures.Add(Content.Load<Texture2D>("Images/16-bit-planet" + i));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds > respawnTime)
            {
                for (int i = 0; i < planetRects.Count; i++)
                {
                    planetTextures.Add(textures[generator.Next(textures.Count)]);
                }
                for (int i = 0; i < 30; i++)
                {
                    planetRects.Add
                        (
                        new Rectangle(generator.Next(window.Width - 25), generator.Next(window.Height - 25), generator.Next(25, 101), generator.Next(25, 101))
                        );
                }
                seconds = 0f;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceBackgroundTexture, window, Color.White);
            for (int i = 0; i <= planetRects.Count; i++)
            {
                _spriteBatch.Draw(planetTextures[i], planetRects[i], Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
