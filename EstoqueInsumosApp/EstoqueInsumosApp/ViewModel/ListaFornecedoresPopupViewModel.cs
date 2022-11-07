using EstoqueInsumosApp.Model;
using Npgsql;
using Rg.Plugins.Popup.Services;
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
        private Fornecedor _SelectedFornecedor;
        public Fornecedor SelectedFornecedor
        {
            get => _SelectedFornecedor;
            set
            {
                _SelectedFornecedor = value;
                OnPropertyChanged();
                if (_SelectedFornecedor != null)
                {
                    MessagingCenter.Send(this, "FornecedorSelecionado", _SelectedFornecedor);
                    PopupNavigation.Instance.PopAllAsync();
                    _SelectedFornecedor = null;
                }
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

            CarregaFornecedores();
        }

        private void CarregaFornecedores()
        {
            using (NpgsqlConnection conn = Connection.Conn())
            {
                conn.Open();
                string strsql = string.Empty;
                strsql += " SELECT";
                strsql += " codigo";
                strsql += " ,nome";
                strsql += " FROM fornecedores";
                strsql += " ORDER BY nome";
                NpgsqlCommand command = new NpgsqlCommand(strsql, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listatemp.Add(new Fornecedor()
                    {
                        Codigo = dr.GetInt32(dr.GetOrdinal("codigo")),
                        Nome = dr.GetString(dr.GetOrdinal("nome"))
                    });
                }
            }
            Fornecedores = listatemp;
        }
    }
}
