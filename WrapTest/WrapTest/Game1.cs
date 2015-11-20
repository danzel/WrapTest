using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace WrapTest
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		readonly GraphicsDeviceManager graphics;
		SpriteBatch _spriteBatch;

		private int _mode = 1;
		private Texture2D _checkers60;
		private Texture2D _checkers64;

		private SoundEffect _soundButtonPress, _soundMatch, _soundTone;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 320;
			graphics.PreferredBackBufferHeight = 480;
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
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			_checkers60 = Content.Load<Texture2D>("Checkers60");
			_checkers64 = Content.Load<Texture2D>("Checkers64");

			_soundButtonPress = Content.Load<SoundEffect>("buttonPress");
			_soundMatch = Content.Load<SoundEffect>("match");
			_soundTone = Content.Load<SoundEffect>("Tone");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		private Random _rand = new Random(1);

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			var touch = TouchPanel.GetState().FirstOrDefault(t => t.State == TouchLocationState.Pressed);
			if (touch != default(TouchLocation))
			{
				if (touch.Position.Y < GraphicsDevice.Viewport.Height / 2)
				{
					_soundMatch.Play(1, (float)(_rand.NextDouble() * 2 - 1), 0);
				}
				else
				{
					_soundTone.Play();
				}
			}

			base.Update(gameTime);
		}
		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_spriteBatch.Begin();

			_spriteBatch.Draw(_checkers60, Vector2.Zero, Color.White);


			_spriteBatch.End();

			base.Draw(gameTime);

		}
	}
}
