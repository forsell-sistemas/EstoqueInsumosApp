using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EstoqueInsumosApp.View;
using EstoqueInsumosApp.ViewModel;
using System.Threading;

namespace EstoqueInsumosApp.View.Shell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuLateralView : Xamarin.Forms.Shell
    {
        public ICommand SairCommand => new Command(Sair);

        public MenuLateralView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void Sair()
        {
            if (await App.Current.MainPage.DisplayAlert("ATENÇÃO", "Deseja mesmo sair?", "Sim", "Não"))
            {
                Thread.Sleep(1000);
                MessagingCenter.Send(this, "SairApp");
            }
        }
    }
}