using EstoqueInsumosApp.Model;
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

        public Command FornecedoresPopupCommand { get; set; }
        public Command InsumosPopupCommand { get; set; }
        public Command EntradaCommand { get; set; }
        public Command SaidaCommand { get; set; }
        public Command ConfirmarCommand { get; set; }

        public PaginaPrincipalViewModel()
        {
            FornecedoresPopupCommand = new Command(FornecedoresPopup);
            InsumosPopupCommand = new Command(InsumosPopup);
            EntradaCommand = new Command(EntradaToggle);
            SaidaCommand = new Command(SaidaToggle);
            ConfirmarCommand = new Command(Confirmar);

            EntradaSelecionada = true;
            HabilitadoBotaoEntrada = true;
            CorBotaoEntrada = new Color(1, 1, 1);
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
        }

        private void FornecedoresPopup()
        {
            PopupNavigation.Instance.PushAsync(new ListaFornecedoresPopupView());
        }

        private void InsumosPopup()
        {
            PopupNavigation.Instance.PushAsync(new ListaInsumosPopupView());
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
            FornecedorSelecionado = null;
            InsumoSelecionado = null;
            Quantidade = "";
        }
    }
}
