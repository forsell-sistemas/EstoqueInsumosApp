using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using EstoqueInsumosApp.View.Shell;

namespace EstoqueInsumosApp.Droid
{
    [Activity(Label = "EstoqueInsumosApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(23, 166, 118, 255));
            Rg.Plugins.Popup.Popup.Init(this);
            LoadApplication(new App());

            MessagingCenter.Subscribe<MenuLateralView>(this, "SairApp", (sender) =>
            {
                SairApp();
            });
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void SairApp()
        {
            Process.KillProcess(Process.MyPid());
        }
    }
}