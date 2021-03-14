using Inori.ViewModels;
using Inori.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Inori
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PostDetailsPage), typeof(PostDetailsPage));
        }

    }
}
