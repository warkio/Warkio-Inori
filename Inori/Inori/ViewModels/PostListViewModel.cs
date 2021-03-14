using Inori.Models.randomc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Inori.Views;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Diagnostics;
using Inori.Models.Cards;

namespace Inori.ViewModels
{
    public class PostListViewModel : BaseViewModel
    {
        private PostListCardItem _post;
        private int Page { get; set; }
        private int Limit { get; set; }
        private bool AllowInfiniteScroll { get; set; }
        public ObservableCollection<PostListCardItem> Posts { get; }
        public Command LoadPostsCommand { get; }
        public Command<PostListCardItem> PostTapped { get; }
        private bool LoadingPosts { get; set; }
        public PostListViewModel()
        {
            Title = "Recent Posts";
            Posts = new ObservableCollection<PostListCardItem>();
            LoadPostsCommand = new Command(async () => await ExecuteLoadPostsCommand());
            PostTapped = new Command<PostListCardItem>(OnItemSelected);
            Page = 0;
            Limit = 50;
            AllowInfiniteScroll = true;
            LoadingPosts = false;
        }

        async Task ExecuteLoadPostsCommand()
        {
            if(!AllowInfiniteScroll || LoadingPosts)
            {
                return;
            }
            Page++;
            LoadingPosts = true;
            var posts = await RandomCPostService.GetPosts(Page, Limit);
            foreach (var post in posts.Posts)
            {
                var addResult = await this.RandomCDataStore.AddItemAsync(post);
                if(addResult)
                {
                    var images = post.Content.GetImages();
                    this.Posts.Add(
                        new PostListCardItem {
                            Post = post,
                            Viewed = false,
                            CoverImage = images.Count > 0? images[0] : null,
                            TimeAgo = post.TimeAgo(),
                        }
                    );
                }

            }
            // Disable load more items
            if(posts.Posts.Length < Limit)
            {
                AllowInfiniteScroll = false;
            }
            LoadingPosts = false;
            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public PostListCardItem SelectedItem
        {
            get => _post;
            set
            {
                SetProperty(ref _post, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(PostListCardItem post)
        {
            if (post == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(PostDetailsPage)}?{nameof(PostDetailsViewModel.PostId)}={post.Post.Id}");
        }
    }
}