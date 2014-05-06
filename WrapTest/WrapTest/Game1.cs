using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#if ANDROID
using Android.Graphics;
using Java.Nio;
#endif

#if IOS
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
#endif

namespace WrapTest
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private Texture2D _checkers64;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 480;
			graphics.PreferredBackBufferHeight = 320;
			Content.RootDirectory = "Content";

			IsFixedTimeStep = false;
			graphics.SynchronizeWithVerticalRetrace = false;
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

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			//if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			//	this.Exit();
			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		private DateTime firstFrame = DateTime.MinValue;
		private int frameCount = 0;

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			if (firstFrame == DateTime.MinValue)
				firstFrame = DateTime.Now;
			frameCount++;
			if (frameCount == 120)
			{
				var end = DateTime.Now;
				var taken = end - firstFrame;
				Debugger.Break();
			}
			GraphicsDevice.Clear(Color.CornflowerBlue);

			if (_checkers64 != null)
			{
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

				spriteBatch.Draw(_checkers64, new Vector2(5, 5), null, Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);

				spriteBatch.End();
			}

			base.Draw(gameTime);
		}
	}
}
