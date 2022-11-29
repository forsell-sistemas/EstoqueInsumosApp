using EstoqueInsumosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EstoqueInsumosApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipalView : ContentPage
    {
        public PaginaPrincipalView()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<PaginaPrincipalViewModel>(this, "HabilitarConfirma", (sender) =>
            {
                BTN_Confirma.IsEnabled = true;
            });
        }

        private void BTN_Confirma_Clicked(object sender, EventArgs e)
        {
            BTN_Confirma.IsEnabled = false;
        }
    }
}