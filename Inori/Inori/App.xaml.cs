using Inori.Services;
using Inori.Services.randomc;
using Inori.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inori
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<RandomCPostDataStore>();
            DependencyService.Register<RandomCService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
