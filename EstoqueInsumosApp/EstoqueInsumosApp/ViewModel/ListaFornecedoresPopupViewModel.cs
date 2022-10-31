using EstoqueInsumosApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EstoqueInsumosApp.ViewModel
{
    class ListaFornecedoresPopupViewModel : BaseViewModel
    {
        public Command SearchCommand
        {
            get
            {
                return new Command(ExecuteSearchCommand,
                CanExecuteSeachCommand);
            }
        }
        private void ExecuteSearchCommand(object parameter)
        {
            Fornecedores = _Fornecedores;
        }
        private string enteredText;

        public string EnteredText
        {
            get { return enteredText; }
            set
            {

                enteredText = value;
                OnPropertyChanged();
                Fornecedores = new List<Fornecedor>(listatemp
                .Where(x => x.Nome.ToLower()
                .Contains(enteredText.ToLower())).ToList());
            }
        }

        private List<Fornecedor> listatemp;
        private List<Fornecedor> _Fornecedores;
        public List<Fornecedor> Fornecedores
        {
            get => _Fornecedores;
            set
            {
                _Fornecedores = value;
                OnPropertyChanged();
            }
        }


        public bool CanExecuteSeachCommand(object parameter)
        {
            return true;
        }

        public ListaFornecedoresPopupViewModel()
        {
            Fornecedores = new List<Fornecedor>();
            listatemp = new List<Fornecedor>();
        }
    }
}
