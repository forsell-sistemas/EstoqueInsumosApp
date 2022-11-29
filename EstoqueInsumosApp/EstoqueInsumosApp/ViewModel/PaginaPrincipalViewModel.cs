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
            MessagingCenter.Send(this, "DesabilitarConfirma");

            if (FornecedorSelecionado == null)
            {
                App.Current.MainPage.DisplayAlert("ATENÇÃO!", "Selecione um Fornecedor.", "OK");
                MessagingCenter.Send(this, "HabilitarConfirma");
                return;
            }
            else if (FornecedorSelecionado.Codigo == 0)
            {
                App.Current.MainPage.DisplayAlert("ATENÇÃO!", "Selecione um Fornecedor.", "OK");
                MessagingCenter.Send(this, "HabilitarConfirma");
                return;
            }
            if (InsumoSelecionado == null)
            {
                App.Current.MainPage.DisplayAlert("ATENÇÃO!", "Selecione um Insumo.", "OK");
                MessagingCenter.Send(this, "HabilitarConfirma");
                return;
            }
            else if (InsumoSelecionado.Codigo == 0)
            {
                App.Current.MainPage.DisplayAlert("ATENÇÃO!", "Selecione um Insumo.", "OK");
                MessagingCenter.Send(this, "HabilitarConfirma");
                return;
            }
            if (OBS.Trim() == "")
            {
                App.Current.MainPage.DisplayAlert("ATENÇÃO!", "Insira uma Observação.", "OK");
                MessagingCenter.Send(this, "HabilitarConfirma");
                return;
            }
            if (Quantidade == null)
            {
                App.Current.MainPage.DisplayAlert("ATENÇÃO!", "Insira uma Quantidade.", "OK");
                MessagingCenter.Send(this, "HabilitarConfirma");
                return;
            }
            if (Quantidade.Trim() == "")
            {
                App.Current.MainPage.DisplayAlert("ATENÇÃO!", "Insira uma Quantidade.", "OK");
                MessagingCenter.Send(this, "HabilitarConfirma");
                return;
            }
            if (double.Parse(Quantidade.Trim().Replace(",",".")) <= 0)
            {
                App.Current.MainPage.DisplayAlert("ATENÇÃO!", "Insira uma Quantidade acima de 0.", "OK");
                MessagingCenter.Send(this, "HabilitarConfirma");
                return;
            }

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
                strsql += $" WITH preco_insumo AS";
                strsql += $" (";
                strsql += $"     SELECT";
                strsql += $"     CASE";
                strsql += $"         WHEN stk_insumos_estoque.upreco IS TRUE THEN ";
                strsql += $"             COALESCE(stk_insumos_estoque.ultimopreco, 0::numeric)";
                strsql += $"         WHEN stk_insumos_estoque.pmedio IS TRUE THEN ";
                strsql += $"             COALESCE(stk_insumos_estoque.preco_medio, 0::numeric)";
                strsql += $"         ELSE";
                strsql += $"             COALESCE(stk_insumos_estoque.preco, 0::numeric)";
                strsql += $"     END AS preco";
                strsql += $"     FROM stk_insumos_estoque";
                strsql += $"     WHERE codins = {InsumoSelecionado.Codigo}";
                strsql += $" )";
                strsql += $" INSERT INTO";
                strsql += $" stk_estoque_entsai(";
                strsql += $"     tipo, codins, codsub, codpro, codpto, data, quantidade, preco, preco_venda, historico, descricao, codemp,";
                strsql += $"     codlote, lote, codcli, codfor, codvar, codmaq, codfunc, codsetor, setor, lcto_aut, cod_retalho, pedido_insumo_codigo,";
                strsql += $"     pedido_insumo_item, es_entradas_codigo, es_entradas_produtos_item, nfe_codigo, nfe_item, cupom_item_codseq,";
                strsql += $"     regime_fiscal_codigo, log_data_hora, log_usuario_pc, codrequisicao_op, certificado_aprovacao_epi, codlocal,";
                strsql += $"     manutencao_indicador_atual, os_manutencao_codigo, codigo";
                strsql += $" )";
                strsql += $" VALUES (";
                strsql += $"     $tipo${entsai}$tipo$";
                strsql += $"     ,{InsumoSelecionado.Codigo}";
                strsql += $"     ,NULL,NULL";
                strsql += $"     ,26";
                strsql += $"     ,NOW()";
                strsql += $"     ,{Quantidade}";
                strsql += $"     ,(SELECT preco FROM preco_insumo)";
                strsql += $"     ,0";
                strsql += $"     ,$sobs${OBS}$sobs$";
                strsql += $"     ,$descricao${InsumoSelecionado.Nome}$descricao$";
                strsql += $"     ,1";
                strsql += $"     ,NULL,NULL,NULL";
                strsql += $"     ,{FornecedorSelecionado.Codigo}";
                strsql += $"     ,NULL,NULL,NULL,NULL,NULL";
                strsql += $"     ,FALSE";
                strsql += $"     ,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL";
                strsql += $"     ,current_timestamp";
                strsql += $"     ,$log_usuario_pc$INCLUÍDO NO APP DE ESTOQUE INSUMOS$log_usuario_pc$";
                strsql += $"     ,NULL,NULL,NULL,NULL,NULL";
                strsql += $"     ,{prox_codigo}";
                strsql += $" );";

                try
                {
                    comando = new NpgsqlCommand(strsql, conn);
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    App.Current.MainPage.DisplayAlert("ERRO!", "Ocorreu um erro ao cadastrar os dados. (" + ex.Message + ")", "OK");
                    MessagingCenter.Send(this, "HabilitarConfirma");
                    return;
                }

                strsql = string.Empty;
                strsql += $" INSERT INTO";
                strsql += $" estoque_mov(";
                strsql += $"     data, data_ref, codtipo, tipo, codemp, codpto, codpro, codsub, codins, quantidade, codvar, codmovimento,";
                strsql += $"     ignorar, obs, regime_fiscal_codigo, reservado, codcli, codfor, codlocal";
                strsql += $" )";
                strsql += $" SELECT";
                strsql += $" current_date";
                strsql += $" ,COALESCE(stk_estoque_entsai.data, current_date)";
                strsql += $" ,4";
                strsql += $" ,'Movimento Direto'";
                strsql += $" ,1";
                strsql += $" ,26";
                strsql += $" ,NULL, NULL";
                strsql += $" ,{InsumoSelecionado.Codigo}";
                strsql += $" ,CASE";
                strsql += $"     WHEN stk_estoque_entsai.tipo = 'S' THEN";
                strsql += $"         -quantidade";
                strsql += $"     ELSE";
                strsql += $"         quantidade";
                strsql += $" END";
                strsql += $" ,NULL";
                strsql += $" ,{prox_codigo}";
                strsql += $" ,FALSE";
                strsql += $" ,historico";
                strsql += $" ,regime_fiscal_codigo";
                strsql += $" ,FALSE";
                strsql += $" ,stk_estoque_entsai.codcli";
                strsql += $" ,stk_estoque_entsai.codfor";
                strsql += $" ,stk_estoque_entsai.codlocal";
                strsql += $" FROM stk_estoque_entsai";
                strsql += $" WHERE stk_estoque_entsai.codigo = {prox_codigo};";

                try
                {
                    comando = new NpgsqlCommand(strsql, conn);
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    App.Current.MainPage.DisplayAlert("ERRO!", "Ocorreu um erro ao cadastrar os dados. (" + ex.Message + ")", "OK");
                    MessagingCenter.Send(this, "HabilitarConfirma");
                    return;
                }

                App.Current.MainPage.DisplayAlert("AVISO", "Dados cadastrados com sucesso.", "OK");
                MessagingCenter.Send(this, "HabilitarConfirma");
            }

            FornecedorSelecionado = null;
            InsumoSelecionado = null;
            OBS = "";
            Quantidade = "";
        }
    }
}
