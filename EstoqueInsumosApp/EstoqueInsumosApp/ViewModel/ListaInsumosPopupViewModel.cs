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
    class ListaInsumosPopupViewModel : BaseViewModel
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
            Insumos = _Insumos;
        }
        private string enteredText;

        public string EnteredText
        {
            get { return enteredText; }
            set
            {

                enteredText = value;
                OnPropertyChanged();
                Insumos = new List<Insumo>(listatemp
                .Where(x => x.Nome.ToLower()
                .Contains(enteredText.ToLower())).ToList());
            }
        }

        private List<Insumo> listatemp;
        private List<Insumo> _Insumos;
        public List<Insumo> Insumos
        {
            get => _Insumos;
            set
            {
                _Insumos = value;
                OnPropertyChanged();
            }
        }
        private Insumo _SelectedInsumo;
        public Insumo SelectedInsumo
        {
            get => _SelectedInsumo;
            set
            {
                _SelectedInsumo = value;
                OnPropertyChanged();
                if (_SelectedInsumo != null)
                {
                    MessagingCenter.Send(this, "InsumoSelecionado", _SelectedInsumo);
                    PopupNavigation.Instance.PopAllAsync();
                    _SelectedInsumo = null;
                }
            }
        }


        public bool CanExecuteSeachCommand(object parameter)
        {
            return true;
        }

        public ListaInsumosPopupViewModel()
        {
            Insumos = new List<Insumo>();
            listatemp = new List<Insumo>();

            CarregaInsumos();
        }

        private void CarregaInsumos()
        {
            using (NpgsqlConnection conn = Connection.Conn())
            {
                conn.Open();
                string strsql = string.Empty;
                strsql += " SELECT";
                strsql += " codigo";
                strsql += " ,nome";
                strsql += " FROM insumos";
                strsql += " ORDER BY nome";
                NpgsqlCommand command = new NpgsqlCommand(strsql, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listatemp.Add(new Insumo()
                    {
                        Codigo = dr.GetInt32(dr.GetOrdinal("codigo")),
                        Nome = dr.GetString(dr.GetOrdinal("nome"))
                    });
                }
            }
            Insumos = listatemp;
        }
    }
}