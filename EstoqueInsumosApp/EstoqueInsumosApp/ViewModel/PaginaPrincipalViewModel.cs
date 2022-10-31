using EstoqueInsumosApp.View;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EstoqueInsumosApp.ViewModel
{
    class PaginaPrincipalViewModel : BaseViewModel
    {
        public Command FornecedoresPopupCommand { get; set; }
        public Command InsumosPopupCommand { get; set; }

        public PaginaPrincipalViewModel()
        {
            FornecedoresPopupCommand = new Command(FornecedoresPopup);
            InsumosPopupCommand = new Command(InsumosPopup);
        }

        private void FornecedoresPopup()
        {
            PopupNavigation.Instance.PushAsync(new ListaFornecedoresPopupView());
        }

        private void InsumosPopup()
        {
            PopupNavigation.Instance.PushAsync(new ListaInsumosPopupView());
        }
    }
}
