﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Banco;
using TG_App.Model;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ListagemAtividadesPage : ContentPage
  {
    public ListagemAtividadesPage()
    {
      InitializeComponent();

      var user = new Validacao().Listagem().SingleOrDefault();
      DBExercicios DB = new DBExercicios();

      var lista = DB.PesquisarAtividade();

      List<AtividadeFisicaViewModel> listagem = new List<AtividadeFisicaViewModel>();
      foreach(var item in lista)
      {
        AtividadeFisicaViewModel dados = new AtividadeFisicaViewModel
        {
          Data = "Data de Registro: " + item.Data.ToString("dd/MM/yyyy"),
          NomeAtividade = "Nome da Atividade: " + item.NomeAtividade.ToUpper(),
          Fim = "Término: " + item.Fim,
          Inicio = "Início: " + item.Inicio,
          Observacao = "Observação: " + item.Observacao,
          UsuarioID = user.UsuarioID,
          AtividadeFisicaID = item.AtividadeFisicaID
        };
        listagem.Add(dados);
      }

      ListaAtividades.ItemsSource = listagem.OrderBy(c => c.NomeAtividade);
    }
    public void CadastrarAtividadeAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new AtividadeFisicaPage();
    }
    public void ExcluirAction(object sender, EventArgs args)
    {
      Button btn = (Button)sender;
      AtividadeFisicaViewModel lista = btn.CommandParameter as AtividadeFisicaViewModel;
      var user = new Validacao().Listagem().SingleOrDefault();
      DBExercicios DB = new DBExercicios();
      var encontrar = DB.PesquisarAtividade().SingleOrDefault(x => x.UsuarioID == user.UsuarioID && x.AtividadeFisicaID == lista.AtividadeFisicaID);
      DB.DeleteAtividade(encontrar);
      App.Current.MainPage = new ListagemAtividadesPage();
    }
    public void EditAction(object sender, EventArgs args)
    {
      Button btn = (Button)sender;
      AtividadeFisicaViewModel lista = btn.CommandParameter as AtividadeFisicaViewModel;

      DBExercicios DB = new DBExercicios();
      var user = new Validacao().Listagem().SingleOrDefault();
      var dados = DB.PesquisarAtividade().Where(x => x.AtividadeFisicaID == lista.AtividadeFisicaID && x.UsuarioID == user.UsuarioID).SingleOrDefault();

      App.Current.MainPage = new AtividadesEditPage(dados);
    }
  }
}