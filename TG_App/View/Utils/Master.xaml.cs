﻿using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Banco;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View.Utils
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public Master()
        {
            InitializeComponent();
            DataBase DB = new DataBase();
        }
        public Master(string tipo)
        {
            InitializeComponent();
            DataBase DB = new DataBase();

            if (tipo == "ExameList")
                Exames();

            if (tipo == "Calcular")
                Calcular();

            if (tipo == "AlimentosPage")
                Alimentos();
        }

        private void GoHome(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new HomePage());
        }

        private void GoRelatorio(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new RelatorioPage());
        }

        private void Configuracoes(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new ConfiguracoesPage());
        }
        public void Configuracoes()
        {
            Detail = new NavigationPage(new ConfiguracoesPage());
        }

        private void Alimentos(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new AlimentosPage());
        }
        public void Alimentos()
        {
            Detail = new NavigationPage(new AlimentosPage());
        }

        private void Exames(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new ExamesListPage());
        }
        public void Exames()
        {
            Detail = new NavigationPage(new ExamesListPage());
        }
        public void Calcular()
        {
            Detail = new NavigationPage(new ExamesPage());
        }
        private void AtFisico(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new AtividadeFisicaPage());
        }
    }
}