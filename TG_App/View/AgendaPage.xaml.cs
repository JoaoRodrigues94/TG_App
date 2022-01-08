﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.DB;
using TG_App.Model;
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
            var dados = DB.PesquisarAgenda().Where(c => c.UsuarioID == user.UsuarioID).ToList();

            List<Agenda> lstAgenda = new List<Agenda>();
            foreach(var item in dados)
            {
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
                Status = Statuses.SelectedIndex,
                Observacao = Observacao.Text,
                UsuarioID = user.UsuarioID
            };

            DB.CadastrarAgenda(dados);
            App.Current.MainPage = new AgendaPage();
        }
        // TODO - corrigir
        public void VoltarAction(object sender, EventArgs args)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}