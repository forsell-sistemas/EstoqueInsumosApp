using EstoqueInsumosApp.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EstoqueInsumosApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PaginaPrincipalView();
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
