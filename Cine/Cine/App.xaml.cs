using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Cine.Views;

namespace Cine
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PeliculasView());
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
