﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.DB;
using TG_App.Model;
using TG_App.View.Utils;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaPage : ContentPage
    {
        public AgendaPage()
        {
            InitializeComponent();

            DBAgenda DB = new DBAgenda();
            var user = new Validacao().Listagem().SingleOrDefault();
            var dadosData = DB.PesquisarAgenda().Where(c => c.UsuarioID == user.UsuarioID).ToList();


            List<Agenda> dadosDT = new List<Agenda>();
            foreach(var item in dadosData)
            {
                string dia = item.Data.Substring(0, 2);
                string mes = item.Data.Substring(3, 2);
                string ano = item.Data.Substring(6, 4);

                string newDate = mes + "/" + dia + "/" + ano;

                item.Data = newDate;

                dadosDT.Add(item);
            };
            var dados =  dadosDT.OrderBy(c => Convert.ToDateTime(c.Data + " " + c.Horario)).OrderBy(c => c.Status).ToList();

            List<Agenda> lstAgenda = new List<Agenda>();
            foreach(var item in dados)
            {
                string dia = item.Data.Substring(0, 2);
                string mes = item.Data.Substring(3, 2);
                string ano = item.Data.Substring(6, 4);

                item.Data = mes + "/" + dia + "/" + ano;

                Agenda dadosAgenda = new Agenda
                {
                    AgendaID = item.AgendaID,
                    Data = item.Data + " " + item.Horario,
                    Descrição = item.Descrição,
                    Local = item.Local,
                    Observacao = item.Observacao,
                    Horario = item.Horario,
                    Status = item.Status
                };
                lstAgenda.Add(dadosAgenda);
            }

            ListaAgenda.ItemsSource = lstAgenda;
        }

        public void SaveAgenda(object sender, EventArgs args)
        {
            DBAgenda DB = new DBAgenda();
            var user = new Validacao().Listagem().SingleOrDefault();
            Agenda dados = new Agenda
            {
                Descrição = Descricao.Text,
                Local = Local.Text,
                Data = Date.Text,
                Horario = Hours.Time.ToString(),
                Status = 0,
                Observacao = Observacao.Text,
                UsuarioID = user.UsuarioID
            };

            DB.CadastrarAgenda(dados);
            App.Current.MainPage = new Master("AgendaPage");
        }
        // TODO - corrigir
        public void VoltarAction(object sender, EventArgs args)
        {
            App.Current.MainPage = new MainPage();
        }

        public void Pesquisar(object sender, EventArgs args)
        {
            DBAgenda Db = new DBAgenda();
            var user = new Validacao().Listagem().SingleOrDefault();

            var lista = Db.PesquisarAgenda().ToList();

            if (!String.IsNullOrEmpty(DataSearch.Text))
                lista = lista.Where(c => c.Data.Contains(DataSearch.Text)).ToList();

            if (!String.IsNullOrEmpty(NomeEvent.Text))
                lista = lista.Where(c => c.Descrição.Contains(NomeEvent.Text)).ToList();

            ListaAgenda.ItemsSource = lista;
            DataSearch.Text = null;
            NomeEvent.Text = null;
        }

        public void Atualizar(object sender, EventArgs args)
        {
            try
            {
                var user = new Validacao().Listagem().SingleOrDefault();
                DBAgenda DB = new DBAgenda();

                Button btn = (Button)sender;
                Agenda lista = btn.CommandParameter as Agenda;
                var busca = DB.PesquisarAgenda().SingleOrDefault(x => x.UsuarioID == user.UsuarioID && x.AgendaID == lista.AgendaID);

                Agenda dados = new Agenda
                {
                    AgendaID = busca.AgendaID,
                    Descrição = busca.Descrição,
                    Local = busca.Horario,
                    Data = busca.Data,
                    Horario = busca.Horario,
                    Status = busca.Status == 0 ? 1 : 0,
                    Observacao = Observacao.Text,
                    UsuarioID = user.UsuarioID
                };

                DB.UpdateAgenda(dados);
                App.Current.MainPage = new Master("AgendaPage");
            }
            catch
            {
                DisplayAlert("Erro", "Não foi possível alterar os dados!", "Ok");
            }
        }

        public void Excluir(object sender, EventArgs args)
        {
            OnAlertYesNoClicked(sender, args);
        }

        public async void OnAlertYesNoClicked(object sender, EventArgs args)
        {
            bool answer = await DisplayAlert("Excluir Evento", "Deseja Realmente excluir este evento?", "Sim", "Não");

            if (answer)
            {
                DBAgenda DB = new DBAgenda();

                Button btn = (Button)sender;
                var user = new Validacao().Listagem().SingleOrDefault();

                Agenda lista = btn.CommandParameter as Agenda;

                var busca = DB.PesquisarAgenda().SingleOrDefault(x => x.UsuarioID == user.UsuarioID && x.AgendaID == lista.AgendaID);
                DB.DeleteAgenda(busca);
                App.Current.MainPage = new Master("AgendaPage");
            }
            else
                App.Current.MainPage = new Master("AgendaPage");
        }
    }
}