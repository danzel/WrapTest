using System;
#if ANDROID
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
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

			var g = new Game1();
			SetContentView((View)g.Services.GetService(typeof(View)));
			g.Run();
		}
	}
#endif
}

