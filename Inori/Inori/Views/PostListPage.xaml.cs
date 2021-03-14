using Inori.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inori.Views
{
    
    public partial class PostListView : ContentPage
    {
        PostListViewModel _viewModel;

        public PostListView()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PostListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}