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

            MessagingCenter.Subscribe<PaginaPrincipalViewModel>(this, "DesabilitarConfirma", (sender) =>
            {
                BTN_Confirma.IsEnabled = false;
            });
        }
    }
}