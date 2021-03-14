using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PanCardView.Droid;
using Android.Support.V4.Content;
using Android;
using Plugin.CurrentActivity;

namespace Inori.Droid
{
    [Activity(Label = "Inori", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public int STORAGE_PERMISSION_CODE = 101;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            CardsViewRenderer.Preserve();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            checkPermission("android.permission.write_external_storage", STORAGE_PERMISSION_CODE);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void checkPermission(String permission, int requestCode)
        {
            var thisActivity = Android.App.Application.Context as Activity;
            // Checking if permission is not granted 
            if (ContextCompat.CheckSelfPermission(
                    Android.App.Application.Context,
                    permission)
                == Android.Content.PM.Permission.Denied)
            {
                RequestPermissions(new String[] { Manifest.Permission.WriteExternalStorage }, requestCode);
            }

        }
    }
}