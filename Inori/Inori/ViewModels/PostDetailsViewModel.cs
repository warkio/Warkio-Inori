using Inori.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Inori.ViewModels
{
    [QueryProperty(nameof(PostId), nameof(PostId))]
    class PostDetailsViewModel : BaseViewModel
    {
        private string content;
        private string postId;
        private string title;
        private string url;
        public Command DownloadImagesCommand { get; }
        public Command ViewOnBrowserCommand { get; }
        private List<Uri> images;
        private int currentIndex;
        public ObservableCollection<string> imageUrls { get; set; }

        public PostDetailsViewModel()
        {
            IsBusy = false;
            DownloadImagesCommand = new Command(async () => await DownloadImages());
            ViewOnBrowserCommand = new Command(async () => await ViewOnBrowser());
        }

        public string Id { get; set; }
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        public string PostId
        {
            get
            {
                return postId;
            }
            set
            {
                postId = value;
                LoadPostId(Convert.ToInt32(value));
            }
        }

        public string PostTitle
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public List<Uri> Images
        {
            get => images;
            set => SetProperty(ref images, value);
        }

        public string Url
        {
            get => url;
            set => SetProperty(ref url, value);
        }

        public int CurrentIndex
        {
            get => currentIndex;
            set => SetProperty(ref currentIndex, value);
        }

        public async void LoadPostId(int postId)
        {
            IsBusy = true;
            try
            {
                var post = await RandomCDataStore.GetItemAsync(postId);
                Id = post.Id.ToString();
                Content = post.Content.GetContent();
                PostTitle = post.Title.Rendered;
                Url = post.Link.Replace("\\/", "/");
                var postImages = post.Content.GetImages();
                Images = new List<Uri>(postImages);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine("Failed to Load Post");
            }
            IsBusy = false;
        }

        public async Task ViewOnBrowser()
        {

            await Browser.OpenAsync(Url, BrowserLaunchMode.SystemPreferred);
        }

        public async Task DownloadImages()
        {
            var sanitizedFolderName = StringUtils.GenerateSlug(this.PostTitle);
            IsBusy = true;
            for(var i = 0; i < this.images.Count; i++)
            {
                var image = this.images[i];
                var imageExtension = "." + image.ToString().Split('.').Last();
                var imageName = image.ToString().Split('/').Last().Split('.').First();
                var sanitizedImageName = StringUtils.GenerateSlug(imageName) + imageExtension;
                string index = i.ToString();
                // There are, less than 100 images (usually)
                if(i < 10)
                {
                    index = "00" + index;
                }
                else if(i < 100)
                {
                    index = "0" + index;
                }
                await DependencyService.Get<IFileService>().SavePictureFromurl($"{index}_{sanitizedImageName}", image, sanitizedFolderName);
            }
            IsBusy = false;
        }
    }
}
