using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using GR.Net.Maroulis.Library;
using Android.Graphics;

namespace Flowers_shop
{
    [Activity(Label = "SplashActivity", MainLauncher =true, Theme ="@style/Theme.AppCompat.Light.NoActionBar")]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            var config = new EasySplashScreen(this)
                .WithFullScreen()
                .WithTargetActivity(Java.Lang.Class.FromType(typeof(List_News_activity)))
                .WithSplashTimeOut(3000)
                .WithBackgroundColor(Color.ParseColor("#ffffff"))
                .WithLogo(Resource.Drawable.logo);
            View splash = config.Create();
            SetContentView(splash);
          }
    }
}