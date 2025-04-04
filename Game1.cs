using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Mono_Topic_2_Assignment
{
    enum Screens
    {
        Title,
        Explaination,
        Game
    }
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
        MouseState mouseState;
        KeyboardState keyboardState;
        string message, messageTwo, messageThree;
        SpriteFont titleFont;
        Screens screen;
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
            respawnTime = 3f;
            message = "Explaination as to why it's similar to the Tutorial:";
            messageTwo = "Once you exit this page, you'll notice that the assignement is\n almost completely identical to the tutorial. And it's because it\n made the most sense to me to just take what was on the tutorial,\n add the new stuff onto it instead of making an entirely new thing\n with the same code and different textures, and made this actually\n feel like an assignment by re-typing everything instead of\n copying and pasting the code.";
            messageThree = "But enough word salad, here's some instructions:\n Left Click - Delete Planets | Right Click - Change Planet Texture";
            screen = Screens.Title;
            

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
            titleFont = Content.Load<SpriteFont>("Font/messageFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            if (screen == Screens.Title)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screens.Explaination;
                }
            }
            else if (screen == Screens.Explaination)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screens.Game;
                }
            }
            else if (screen == Screens.Game)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (seconds > respawnTime)
                {
                    planetTextures.Add(textures[generator.Next(textures.Count)]);
                    planetRects.Add(new Rectangle(generator.Next(window.Width - 25), generator.Next(window.Height - 25), 25, 25));
                    seconds = 0f;
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    for (int i = 0; i < planetRects.Count; i++)
                    {
                        if (planetRects[i].Contains(mouseState.Position))
                        {
                            planetRects.RemoveAt(i);
                            planetTextures.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else if (mouseState.RightButton == ButtonState.Pressed)
                {
                    for (int i = 0; i < planetRects.Count; i++)
                    {
                        if (planetRects[i].Contains(mouseState.Position))
                        {
                            planetTextures[i] = textures[generator.Next(textures.Count)];
                        }
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    Exit();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screens.Title)
            {
                _spriteBatch.DrawString(titleFont, "Miracle McMahon", new Vector2(275, 10), Color.Black);
                _spriteBatch.DrawString(titleFont, "Left Click to Continue", new Vector2(250, 400), Color.Black);
            }
            else if (screen == Screens.Explaination)
            {
                _spriteBatch.DrawString(titleFont, message, new Vector2(10, 10), Color.Black);
                _spriteBatch.DrawString(titleFont, messageTwo, new Vector2(5, 75), Color.Black);
                _spriteBatch.DrawString(titleFont, messageThree, new Vector2(5, 330), Color.Black);
                _spriteBatch.DrawString(titleFont, "Press Enter to Continue", new Vector2(270, 450), Color.Black);
            }
            else if (screen == Screens.Game)
            {   
                _spriteBatch.Draw(spaceBackgroundTexture, window, Color.White);
                for (int i = 0; i < planetRects.Count; i++)
                {
                    _spriteBatch.Draw(planetTextures[i], planetRects[i], Color.White);
                }
                _spriteBatch.DrawString(titleFont, "Press Q to Quit", new Vector2(10, 10), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
