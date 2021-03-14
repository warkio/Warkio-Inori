using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Inori.Droid.Services;
using Inori.Services;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace Inori.Droid.Services
{
    public class FileService : IFileService
    {
        Context CurrentContext => CrossCurrentActivity.Current.Activity;
        public async Task SavePictureFromurl(string name, Uri url, string location = "temp")
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, "Inori", location);
            Directory.CreateDirectory(documentsPath);

            string filePath = Path.Combine(documentsPath, name);

            WebClient webClient = new WebClient();

            webClient.DownloadDataCompleted += (s, e) =>
            {
                Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
                string path = System.IO.Path.Combine(storagePath.ToString(), location);
                if(!System.IO.Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePath = System.IO.Path.Combine(path, name);

                System.IO.File.WriteAllBytes(filePath, e.Result);
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filePath)));
                CurrentContext.SendBroadcast(mediaScanIntent);
            };

            await webClient.DownloadDataTaskAsync(url);
        }
    }
}