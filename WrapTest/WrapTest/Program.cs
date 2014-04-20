using System;

#if ANDROID
using Android.App;
using Android.Content.PM;
using Android.OS;
#endif

namespace WrapTest
{
#if WINDOWS || XBOX || PSS
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#elif ANDROID
	[Activity(Label = "WrapTest", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
	public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Microsoft.Xna.Framework.Game.Activity = this;
			var g = new Game1();
			SetContentView(g.Window);
			g.Run();
		}
	}
#endif
}

