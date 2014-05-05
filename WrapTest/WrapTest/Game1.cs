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

		private RenderTarget2D renderTarget;
		private SpriteBatch m_spriteBatch;
		private Texture2D texture;

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
			renderTarget = new RenderTarget2D(GraphicsDevice, 200, 200);
			m_spriteBatch = new SpriteBatch(GraphicsDevice);

			texture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			Color clr = Color.Red;
			// set half transparency on my texture
			clr.A = 128;
			Color[] bitmap = new Color[1] { clr };
			texture.SetData(bitmap);
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
			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.SetRenderTarget(renderTarget);
			GraphicsDevice.Clear(Color.White);

			BlendState myState = new BlendState();
			// no blending on color
			myState.ColorSourceBlend = Blend.Zero;
			myState.ColorDestinationBlend = Blend.One;
			// multiplicative blending on alpha
			myState.AlphaSourceBlend = Blend.Zero;
			myState.AlphaDestinationBlend = Blend.SourceAlpha;

			m_spriteBatch.Begin(SpriteSortMode.Immediate, myState);
			// draw my texture twice, with an overlaping part
			m_spriteBatch.Draw(texture, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 100, SpriteEffects.None, 0);
			m_spriteBatch.Draw(texture, new Vector2(50, 50), null, Color.White, 0, Vector2.Zero, 100, SpriteEffects.None, 0);
			m_spriteBatch.End();

			GraphicsDevice.SetRenderTarget(null);

			GraphicsDevice.Clear(Color.Green);
			m_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			m_spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
			m_spriteBatch.End();
		}
	}
}
