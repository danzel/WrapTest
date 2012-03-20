using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WrapTest
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private int _mode;
		private Texture2D _checkers60;
		private Texture2D _checkers64;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 480;
			graphics.PreferredBackBufferHeight = 320;
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			_checkers60 = Content.Load<Texture2D>("Checkers60");
			_checkers64 = Content.Load<Texture2D>("Checkers64");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		bool _spaceWasPressed;

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			var spacePressed = Keyboard.GetState().IsKeyDown(Keys.Space);

			if (spacePressed && !_spaceWasPressed)
			{
				_mode = (_mode + 1) % 4;
			}

			_spaceWasPressed = spacePressed;

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			Texture2D texture = (_mode % 2 == 0) ? _checkers60 : _checkers64;

			if (_mode < 2) //Without matrix
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null);
			else //With matrix to rotate whole screen 180
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null, null, Matrix.CreateRotationZ(MathHelper.ToRadians(180)) * Matrix.CreateTranslation(480, 320, 0));

			var size = new Vector2(texture.Width, texture.Height);

			//Plain
			spriteBatch.Draw(texture, new Vector2(5, 5), new Rectangle(0, 0, (int)size.X, (int)size.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

			//2x across
			spriteBatch.Draw(texture, new Vector2(5, 10 + size.Y), new Rectangle(0, 0, (int)size.X * 2, (int)size.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

			//2x down
			spriteBatch.Draw(texture, new Vector2(5, 15 + 2 *size.Y), new Rectangle(0, 0, (int)size.X, (int)size.Y * 2), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

			//2x2 down
			spriteBatch.Draw(texture, new Vector2(10 + size.X, 15 + 2 * size.Y), new Rectangle(0, 0, (int)size.X * 2, (int)size.Y * 2), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

			//1.5 across
			spriteBatch.Draw(texture, new Vector2(10 + size.X, 5), new Rectangle(0, 0, (int)(size.X * 1.5f), (int)size.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

			//1.5 down
			spriteBatch.Draw(texture, new Vector2(15 + 2.5f * size.X, 5), new Rectangle(0, 0, (int)size.X, (int)(size.Y * 1.5f)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

			//1.5x1.5
			spriteBatch.Draw(texture, new Vector2(20 + 3.5f * size.X, 5), new Rectangle(0, 0, (int)(size.X * 1.5f), (int)(size.Y * 1.5f)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);


			spriteBatch.End();

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
