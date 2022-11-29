using EstoqueInsumosApp.Model;
using EstoqueInsumosApp.View;
using Npgsql;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EstoqueInsumosApp.ViewModel
{
    class PaginaPrincipalViewModel : BaseViewModel
    {
        private Fornecedor _FornecedorSelecionado;
        public Fornecedor FornecedorSelecionado
        {
            get => _FornecedorSelecionado;
            set
            {
                _FornecedorSelecionado = value;
                OnPropertyChanged();
            }
        }
        private Insumo _InsumoSelecionado;
        public Insumo InsumoSelecionado
        {
            get => _InsumoSelecionado;
            set
            {
                _InsumoSelecionado = value;
                OnPropertyChanged();
            }
        }
        private Ponto _PontoSelecionado;
        public Ponto PontoSelecionado
        {
            get => _PontoSelecionado;
            set
            {
                _PontoSelecionado = value;
                OnPropertyChanged();
            }
        }
        private bool _EntradaSelecionada;
        public bool EntradaSelecionada
        {
            get => _EntradaSelecionada;
            set
            {
                _EntradaSelecionada = value;
                OnPropertyChanged();
            }
        }
        private bool _HabilitadoBotaoSaida;
        public bool HabilitadoBotaoSaida
        {
            get => _HabilitadoBotaoSaida;
            set
            {
                _HabilitadoBotaoSaida = value;
                OnPropertyChanged();
            }
        }
        private Color _CorBotaoSaida;
        public Color CorBotaoSaida
        {
            get => _CorBotaoSaida;
            set
            {
                _CorBotaoSaida = value;
                OnPropertyChanged();
            }
        }
        private bool _HabilitadoBotaoEntrada;
        public bool HabilitadoBotaoEntrada
        {
            get => _HabilitadoBotaoEntrada;
            set
            {
                _HabilitadoBotaoEntrada = value;
                OnPropertyChanged();
            }
        }
        private Color _CorBotaoEntrada;
        public Color CorBotaoEntrada
        {
            get => _CorBotaoEntrada;
            set
            {
                _CorBotaoEntrada = value;
                OnPropertyChanged();
            }
        }
        private string _Quantidade;
        public string Quantidade
        {
            get => _Quantidade;
            set
            {
                _Quantidade = value;
                OnPropertyChanged();
            }
        }
        private string _OBS;
        public string OBS
        {
            get => _OBS;
            set
            {
                _OBS = value;
                OnPropertyChanged();
            }
        }

        public Command FornecedoresPopupCommand { get; set; }
        public Command InsumosPopupCommand { get; set; }
        public Command PontoPopupCommand { get; set; }
        public Command EntradaCommand { get; set; }
        public Command SaidaCommand { get; set; }
        public Command ConfirmarCommand { get; set; }

        public PaginaPrincipalViewModel()
        {
            FornecedoresPopupCommand = new Command(FornecedoresPopup);
            InsumosPopupCommand = new Command(InsumosPopup);
            PontoPopupCommand = new Command(PontosPopup);
            EntradaCommand = new Command(EntradaToggle);
            SaidaCommand = new Command(SaidaToggle);
            ConfirmarCommand = new Command(Confirmar);

            EntradaSelecionada = true;
            HabilitadoBotaoEntrada = true;
            CorBotaoEntrada = new Color(0.313, 1, 0.450);
            HabilitadoBotaoSaida = false;
            CorBotaoSaida = new Color(0.8, 0.8, 0.8);

            MessagingCenter.Subscribe<ListaFornecedoresPopupViewModel, Fornecedor>(this, "FornecedorSelecionado", (sender, forn) =>
            {
                FornecedorSelecionado = forn;
            });
            MessagingCenter.Subscribe<ListaInsumosPopupViewModel, Insumo>(this, "InsumoSelecionado", (sender, ins) =>
            {
                InsumoSelecionado = ins;
            });
            MessagingCenter.Subscribe<ListaPontoPopupViewModel, Ponto>(this, "PontoSelecionado", (sender, ponto) =>
            {
                PontoSelecionado = ponto;
            });
        }

        private void FornecedoresPopup()
        {
            PopupNavigation.Instance.PushAsync(new ListaFornecedoresPopupView());
        }

        private void InsumosPopup()
        {
            PopupNavigation.Instance.PushAsync(new ListaInsumosPopupView());
        }
        private void PontosPopup()
        {
            PopupNavigation.Instance.PushAsync(new ListaPontosPopupView());
        }

        private void EntradaToggle()
        {
            EntradaSelecionada = false;
            HabilitadoBotaoEntrada = false;
            CorBotaoEntrada = new Color(0.8, 0.8, 0.8);
            HabilitadoBotaoSaida = true;
            CorBotaoSaida = new Color(0.313, 1, 0.450);
        }

        private void SaidaToggle()
        {
            EntradaSelecionada = true;
            HabilitadoBotaoEntrada = true;
            CorBotaoEntrada = new Color(0.313, 1, 0.450);
            HabilitadoBotaoSaida = false;
            CorBotaoSaida = new Color(0.8, 0.8, 0.8);
        }

        private void Confirmar()
        {
            NpgsqlCommand comando;
            int prox_codigo = 0;
            string entsai = "";

            using (NpgsqlConnection conn = Connection.Conn())
            {
                conn.Open();

                string strsql = string.Empty;
                strsql += " SELECT (MAX(codigo) + 1) AS prox_codigo FROM stk_estoque_entsai";
                NpgsqlCommand command = new NpgsqlCommand(strsql, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    prox_codigo = dr.GetInt32(dr.GetOrdinal("prox_codigo"));
                }
                dr.Close();
                if (prox_codigo == 0)
                {
                    MessagingCenter.Send(this, "HabilitarConfirma");
                    return; 
                }

                if (EntradaSelecionada)
                { entsai = "E"; }
                else
                { entsai = "S"; }

                strsql = string.Empty;
                strsql += $" INSERT INTO stk_estoque_entsai(codigo,tipo,codins,codsub,codpro,codpto,data,quantidade,preco,historico,descricao,codemp,preco_venda,log_data_hora,log_usuario_pc)";
                strsql += $" VALUES({prox_codigo},'{entsai}',{InsumoSelecionado.Codigo},NULL,NULL,{PontoSelecionado.Codigo},NOW(),{Quantidade},0,'{OBS}','{InsumoSelecionado.Nome}',1,0,NOW(),'USUÁRIO APP (Estoque Insumos App)');";

                try
                {
                    comando = new NpgsqlCommand(strsql, conn);
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessagingCenter.Send(this, "HabilitarConfirma");
                }
            }

            FornecedorSelecionado = null;
            InsumoSelecionado = null;
            OBS = "";
            Quantidade = "";
        }
    }
}
