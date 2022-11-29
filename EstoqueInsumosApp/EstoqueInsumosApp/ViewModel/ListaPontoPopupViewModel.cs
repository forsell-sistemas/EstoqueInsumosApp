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
    class ListaPontoPopupViewModel : BaseViewModel
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
            Pontos = _Pontos;
        }
        private string enteredText;

        public string EnteredText
        {
            get { return enteredText; }
            set
            {

                enteredText = value;
                OnPropertyChanged();
                Pontos = new List<Ponto>(listatemp
                .Where(x => x.Nome.ToLower()
                .Contains(enteredText.ToLower())).ToList());
            }
        }

        private List<Ponto> listatemp;
        private List<Ponto> _Pontos;
        public List<Ponto> Pontos
        {
            get => _Pontos;
            set
            {
                _Pontos = value;
                OnPropertyChanged();
            }
        }
        private Ponto _SelectedPonto;
        public Ponto SelectedPonto
        {
            get => _SelectedPonto;
            set
            {
                _SelectedPonto = value;
                OnPropertyChanged();
                if (_SelectedPonto != null)
                {
                    MessagingCenter.Send(this, "PontoSelecionado", _SelectedPonto);
                    PopupNavigation.Instance.PopAllAsync();
                    _SelectedPonto = null;
                }
            }
        }


        public bool CanExecuteSeachCommand(object parameter)
        {
            return true;
        }

        public ListaPontoPopupViewModel()
        {
            Pontos = new List<Ponto>();
            listatemp = new List<Ponto>();

            CarregaPontos();
        }

        private void CarregaPontos()
        {
            using (NpgsqlConnection conn = Connection.Conn())
            {
                conn.Open();
                string strsql = string.Empty;
                strsql += " SELECT";
                strsql += " codigo";
                strsql += " ,descricao";
                strsql += " FROM ste_pontos";
                strsql += " ORDER BY descricao";
                NpgsqlCommand command = new NpgsqlCommand(strsql, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listatemp.Add(new Ponto()
                    {
                        Codigo = dr.GetInt32(dr.GetOrdinal("codigo")),
                        Nome = dr.GetString(dr.GetOrdinal("descricao"))
                    });
                }
            }
            Pontos = listatemp;
        }
    }
}
