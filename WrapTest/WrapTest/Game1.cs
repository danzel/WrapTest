using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Color = Microsoft.Xna.Framework.Color;
using File = System.IO.File;
using Matrix = Microsoft.Xna.Framework.Matrix;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

#if ANDROID
using Android.Graphics;
using Java.Nio;
#endif

#if IOS
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
#endif

#if WINDOWS
using System.Drawing.Imaging;
#endif

#if TOUCH
using Microsoft.Xna.Framework.Input.Touch;
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

		private int _mode = 1;
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
#if !WINDOWS_PHONE //Loading 2 textures from .png fails on Windows phone, since we are a hack job just load one
			_checkers60 = Content.Load<Texture2D>("Checkers60");
#endif
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
#if !PSS
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();
			
#if TOUCH
			if (TouchPanel.GetState().Any(t => t.State == TouchLocationState.Pressed))
				_mode = (_mode + 1) % 4;
#else
			var spacePressed = Keyboard.GetState().IsKeyDown(Keys.Space);

			if (spacePressed && !_spaceWasPressed)
			{
				_mode = (_mode + 1) % 4;
			}

			_spaceWasPressed = spacePressed;
#endif
#endif
			// TODO: Add your update logic here

			base.Update(gameTime);
		}
		
		DateTime _firstDrawTime;
		int _drawCount = 0;
		
		double _first60fps;
		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			if (_drawCount == 0)
				_firstDrawTime = DateTime.Now;
			_drawCount ++;
			if (_drawCount == 60)
			{
				DateTime end = DateTime.Now;
				TimeSpan timeTaken = end - _firstDrawTime;
				_first60fps = 60 / timeTaken.TotalSeconds;
				
				_firstDrawTime = end;
			}
			if (_drawCount == 120)
			{
				DateTime end = DateTime.Now;
				TimeSpan timeTaken = end - _firstDrawTime;
				
				var first = _first60fps;
				var second = 60 / timeTaken.TotalSeconds;
			}
			
			GraphicsDevice.Clear(Color.CornflowerBlue);

			Texture2D texture = (_mode % 2 == 0) ? _checkers60 : _checkers64;
			
			//for (int i = 0; i < 10; i++)
			{
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
			}
			// TODO: Add your drawing code here

			base.Draw(gameTime);
			
			byte[] screenData = new byte[GraphicsDevice.Viewport.Width * GraphicsDevice.Viewport.Height * 4];
			GraphicsDevice.GetBackBufferData(screenData);
			//This returns the screen in BGRA format in XNA

#if WINDOWS
#if !(WINDOWS && DIRECTX)
			//In XNA and MonoGame.OpenGL we need to swap the R and B bytes so we are in RGBA
			for (var i = 0; i < screenData.Length; i += 4)
			{
				byte temp = screenData[i];
				screenData[i] = screenData[i + 2];
				screenData[i + 2] = temp;
			}
#endif

			using (var bitmap = new Bitmap(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height))
			{
				var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb); //This is actually RGBA byte format

				Marshal.Copy(screenData, 0, data.Scan0, screenData.Length);

				bitmap.UnlockBits(data);

				bitmap.Save("out.png", ImageFormat.Png);
			}
#endif

#if IOS
			using (var colorSpace = CGColorSpace.CreateDeviceRGB())
			using (var provider = new CGDataProvider(screenData, 0, screenData.Length))
			using (var cgImage = new CGImage(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 8, 4 * 8, 4 * GraphicsDevice.Viewport.Width, colorSpace, CGBitmapFlags.ByteOrderDefault, provider, null, false, CGColorRenderingIntent.Default))
			using (var image = UIImage.FromImage(cgImage))
			{
				image.SaveToPhotosAlbum(null);
			}
#endif

#if ANDROID
			using (var buffer = ByteBuffer.Wrap(screenData))
			using (var bitmap = Bitmap.CreateBitmap(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, Bitmap.Config.Argb8888))
			{
				bitmap.CopyPixelsFromBuffer(buffer);

				var filename = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "out.png");

				//Make sure you have <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
				using (var output = File.OpenWrite(filename))
				{
					bitmap.Compress(Bitmap.CompressFormat.Png, 100, output);
				}
			}
#endif
		}
	}
}
